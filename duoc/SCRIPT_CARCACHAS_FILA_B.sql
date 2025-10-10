CREATE DATABASE CARCAHAS;
GO
USE CARCAHAS ;
GO
-- -----------------------------------------------------
-- Table Usuarios
-- -----------------------------------------------------
CREATE  TABLE  USUARIOS(
  USER_NAME VARCHAR(45) NOT NULL ,
  PASSWORD VARCHAR(45) NULL ,
  NOMBRE VARCHAR(45) NULL   
  PRIMARY KEY (USER_NAME) )
GO


-- -----------------------------------------------------
-- Table Marcas
-- -----------------------------------------------------
CREATE  TABLE  MARCAS (
  idMarcas INT NOT NULL ,
  Marca VARCHAR(45) NULL ,
  PRIMARY KEY (idMarcas) )
GO


-- -----------------------------------------------------
-- Table Modelo
-- -----------------------------------------------------
CREATE  TABLE  MODELO(
  idModelo INT NOT NULL ,
  Modelo VARCHAR(45) NULL ,
  PRIMARY KEY (idModelo) )
GO


-- -----------------------------------------------------
-- Table Automovil
-- -----------------------------------------------------
CREATE  TABLE  AUTOMOVIL (
  idAutomovil INT NOT NULL ,
  Patente VARCHAR(45) NULL ,
  Puertas VARCHAR(45) NULL ,
  Color VARCHAR(45) NULL ,
  Precio DECIMAL(10,0)  NULL ,
  Modelo_idModelo INT NOT NULL ,
  Marcas_idMarcas INT NOT NULL ,
  PRIMARY KEY (idAutomovil) )
GO

ALTER TABLE AUTOMOVIL ADD CONSTRAINT FK_AUTO_MODELO
FOREIGN KEY (Modelo_idModelo) REFERENCES MODELO(idModelo)
GO

ALTER TABLE AUTOMOVIL ADD CONSTRAINT FK_AUTO_MARCA
FOREIGN KEY (Marcas_idMarcas) REFERENCES MARCAS(idMarcas)
GO

INSERT INTO MARCAS VALUES(1,'RENAULT');
INSERT INTO MARCAS VALUES(2,'FORD');
INSERT INTO MARCAS VALUES(3,'NISSAN');
GO

INSERT INTO MODELO VALUES(1,'CLASSIC');
INSERT INTO MODELO VALUES(2,'STATION');
INSERT INTO MODELO VALUES(3,'4X4');
INSERT INTO MODELO VALUES(4,'SUB');
GO
INSERT INTO USUARIOS VALUES('JP','1234','JUAN SILVA');
INSERT INTO USUARIOS VALUES('JF','HOLA','JUANA SOLIS');
INSERT INTO USUARIOS VALUES('JT','CASA','JUAN PABLO ROMERO');
GO
