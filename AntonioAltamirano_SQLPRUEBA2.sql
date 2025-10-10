-- EVALUACION 2 --
/*

Ejercicio 1: (20pts)
Crear procedimiento sp_empleado, el cual se entrega por parametro el nombre y apellido 
del empleado, mostrando el nombre de la editorial en los cual participo en la 
publicacion y la cantidad de libros publicados.
formato: el empleado xx publico xx libros en la editorial xx.
*/


CREATE PROCEDURE sp_empleado @nombre VARCHAR(20), @apellido VARCHAR(20)
AS 
BEGIN

 DECLARE @nombre_aut varchar(20),
 @apellido_aut varchar(20),
 @nombre_libro varchar(20),
 @cantidadd int 

 SELECT @nombre_aut= au_fname , @apellido_aut=au_lname, @nombre_libro = title , @cantidadd =qty
 FROM pubs.dbo.authors 
 join pubs.dbo.titleauthor on (authors.au_id = titleauthor.au_id)
  join pubs.dbo.titles on (titleauthor.title_id = titles.title_id)
  join pubs.dbo.sales on (titles.title_id = sales.title_id) WHERE authors.au_fname=@nombre AND authors.au_lname=@apellido;

  PRINT 'El autor : '+@nombre_aut +' '+@apellido_aut +' escribio el libro '+@nombre_libro+' y vende la cantidad de :'+CONVERT(varchar(20),@cantidadd)+' libros'
END


EXEC sp_empleado 'Johnson','White'

GO




/*

Ejercicio 2: (20pts)
Crear un procedimiento que obtenga la siguiente informacion:
Entregando por parametro van a entregar el min_lvl y el max_lvl del empleado
que se necesita mostrar el nombre y van a obtener la ciudad de la editorial en la que
trabaja. Reemplazar en la ciudad las letras "o" por 0, las letras "a" por 4
y las letras "e" por un 3. el empleado xx trabaja en la ciudad



*/



CREATE PROCEDURE sp_ejercicio2 @lvlmin int, @lvlmax int
AS
BEGIN
 DECLARE @nombre varchar(25), @apellido varchar(25),@ciudad varchar(25)

DECLARE CursorEmp  CURSOR SCROLL 
FOR SELECT employee.fname ,employee.lname, REPLACE(stores.city,'a','4') FROM pubs.dbo.employee join pubs.dbo.publishers on employee.pub_id=publishers.pub_id join pubs.dbo.titles on publishers.pub_id=titles.pub_id join pubs.dbo.sales on titles.title_id=sales.title_id join pubs.dbo.stores on sales.stor_id=stores.stor_id

OPEN CursorEmp
FETCH FIRST FROM CursorEmp INTO @nombre , @apellido, @ciudad
WHILE (@@FETCH_STATUS =0) -- SI ESTA EN 0 PUEDO CONTINUAR
 BEGIN

 PRINT 'el empleado '+@nombre+ ' '+@apellido + ' trabaja en la ciudad '+@ciudad+''
 FETCH NEXT FROM CursorEmp INTO @nombre , @apellido, @ciudad
 END

 CLOSE CursorEmp
 DEALLOCATE CursorEmp
 

END

exec sp_ejercicio2 25,100


GO





/*
Ejercicio 3: (60pts)
Crear una tabla para hacer un historial de las tiendas llamada STORE_HISTORIAL.
Parametros de la tabla: id de la tienda, nombre de la tienda, fecha del sistema,
y descripcion de la ejecucion.
-Deben crear un trigger para cada accion:
- Si se inserta en la tabla store, se insertara en la 
tabla historial por ej: 1 - DUOC STORE - 12121-121-12 - 'Se inserto en la tabla store'
- Si se actualiza en la tabla store, se insertara en la tabla historial por ej:
 2 - DUOC STORE - 12121-121-12 - 'Se actualizo en la tabla store'.
- Si se elimina en la tabla store, se insertara en la tabla historial por ej:
 2 - DUOC STORE - 12121-121-12 - 'Se elimino en la tabla store'.
 */




 SELECT * INTO copia_store FROM pubs.dbo.stores;

 CREATE TABLE store_historial (
 id_tienda int PRIMARY KEY ,
 nombre varchar(20),
 fecha date,
 descrp varchar(30) 
 
 )

 select *  from copia_store;


 CREATE TRIGGER tr_ejercicio3insertar
 ON copia_store
 FOR INSERT 
 AS
 BEGIN
 INSERT store_historial SELECT stor_id, stor_name ,GETDATE(), 'SE INSERTO EN LA TABLA STORE' FROM inserted 
 END
 GO


 INSERT INTO copia_store values (88,'Juan','123 mi casa', 'santiago', '88','1234');

 
 CREATE TRIGGER tr_ejercicio3actualizar
 ON copia_store
 FOR UPDATE
 AS
 BEGIN
 INSERT store_historial SELECT stor_id, stor_name,GETDATE(), 'SE ACTUALIZO EN LA TABLA STORE' FROM inserted
 END
 GO

 UPDATE copia_store set stor_name='juana' where stor_id=7066


 CREATE TRIGGER tr_ejercicio3eliminar
 ON copia_store
 FOR DELETE
 AS
 BEGIN
 INSERT store_historial SELECT stor_id, stor_name,GETDATE(), 'SE ELIMINO EN LA TABLA STORE' FROM deleted
 END
 GO


 delete copia_store where stor_id='6380'

 Select * from store_historial;
  select * from copia_store;


  drop trigger tr_ejercicio3eliminar