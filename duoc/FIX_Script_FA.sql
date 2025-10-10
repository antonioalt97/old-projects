
create table "Alojamiento" ( 

	"Rut" int not null,
	"NombreMascota" nvarchar(20) not null,
	"IdTipoMascota" int not null,
	"IdTipoAlojamiento" int not null,
	"FechaIngreso" Datetime,
	"Dias" int not null)  
	
 

go

alter table "Alojamiento"
	add constraint "Alojamiento_PK" primary key ("Rut","NombreMascota")   


go


alter table "Alojamiento"
	add constraint "Alojamiento_FK1" foreign key (
		"Rut")
	 references "Propietario" (
		"Rut") on update no action on delete no action  

go


alter table "Alojamiento"
	add constraint "Alojamiento_FK2" foreign key (
		"IdTipoMascota")
	 references "TipoMascota" (
		"IdTipoMascota") on update no action on delete no action  

go


alter table "Alojamiento"
	add constraint "Alojamiento_FK3" foreign key (
		"IdTipoAlojamiento")
	 references "TipoAlojamiento" (
		"IdTipoAlojamiento") on update no action on delete no action  

go