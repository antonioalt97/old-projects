
create table "Residencia" ( 

	"Rut" int not null,
	"Direccion" nvarchar(20) not null,
	"IdImpacto" int not null,
	"IdTipoResidencia" int not null,
	"FechaLlegaqada" Datetime,
	"Moradores" int not null)  
	
	

go

alter table "Residencia"
	add constraint "Residencia_PK" primary key ("Rut","Direccion")   


go


alter table "Residencia"
	add constraint "Residencia_FK1" foreign key (
		"Rut")
	 references "Propietario" (
		"Rut") on update no action on delete no action  

go


alter table "Residencia"
	add constraint "Residencia_FK2" foreign key (
		"IdImpacto")
	 references "ClasificacionImpacto" (
		"IdImpacto") on update no action on delete no action  

go


alter table "Residencia"
	add constraint "Residencia_FK3" foreign key (
		"IdTipoResidencia")
	 references "TipoResidencia" (
		"IdTipoResidencia") on update no action on delete no action  

go
