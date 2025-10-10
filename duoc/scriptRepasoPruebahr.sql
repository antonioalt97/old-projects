SELECT CONCAT(CONCAT(TO_CHAR(NUMRUT_CLI,'09G999G999'),'-'),DVRUT_CLI) "RUT CLIENTE",
concat(concat(concat(concat(appaterno_cli, ' '),SUBSTR(apmaterno_cli,1,1))
,'. '),nombre_cli) "NOMBRE CLIENTE", to_char(renta_cli,'$99,999,999')"RENTA",
fonofijo_cli "FONO FIJO", nvl(to_char(celular_cli),'NO REGISTRA') "CELULAR"
FROM CLIENTE join estado_civil
using (id_estcivil) where desc_estcivil = 'Soltero'
order by numrut_cli asc;

select concat(nombre_emp, concat( ' ',concat(appaterno_emp,concat(' ' , apmaterno_emp)))) "NOMBRE COMPLETO" , sueldo_emp "SUELDO" , 
to_char(fecing_emp,'DD/MM/YYYY') "FECHA INGRESO",(round(months_between(sysdate,fecing_emp)/12))"AÑOS TRABAJANDO", round(sueldo_emp*(((months_between(sysdate,fecing_emp)/12))*1.09)) "BONO"
from empleado where fecing_emp between '01011978' and '01011989' 
order by BONO desc;

select concat(to_char(NUMRUT_PROP,'99G999G999') ,concat('-',DVRUT_PROP)) " RUT PROPIETARIO" from propietario