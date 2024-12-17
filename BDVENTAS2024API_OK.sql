-- SCRIPT  DE  IMPLANTACION  DE  BD. MS SQL SERVER  
-- CREACION DE BASE DE DATOS  (DATA y LOG)
USE master
GO

IF db_id('BDVENTAS2024API') is not null
begin
	ALTER DATABASE BDVENTAS2024API
	SET SINGLE_USER WITH ROLLBACK IMMEDIATE

	DROP DATABASE BDVENTAS2024API
end
go

CREATE DATABASE BDVENTAS2024API
COLLATE Modern_Spanish_CI_AI
GO

SET LANGUAGE SPANISH
SET NOCOUNT ON
GO

USE BDVENTAS2024API
GO

-- Creacion de las Tablas
CREATE TABLE tb_articulos (
	cod_art char (5) NOT NULL 
	    Constraint pk_tb_articulos Primary key,
	nom_art varchar (50) NULL ,
	uni_med varchar (10) NULL ,
	pre_art decimal(7,2) NULL ,
	stk_art int NULL,
	dado_de_baja char(2) Default('No'))
GO

CREATE TABLE tb_articulos_baja (
	cod_art char (5) NOT NULL,
	fecha_baja date default(getdate()) NOT NULL,
	constraint pk_tb_articulos_baja 
	   Primary key (cod_art, fecha_baja) ,
    constraint fk_tb_art_baja_cod_art Foreign Key(cod_art)
	   References tb_articulos(cod_art)
)
GO

-- unidades_liquidar, es el stock > 0 del articulo dado de baja
-- precio_liquidar, es el precio del articulo dado de baja
CREATE TABLE tb_articulos_liquidacion (
	num_reg int identity(1,1) NOT NULL, 
	cod_art char (5) NOT NULL,
	unidades_liquidar int NOT NULL , 
	precio_liquidar decimal(7,2) NOT NULL,
	    constraint pk_tb_art_liquidacion Primary Key(num_reg),
		constraint fk_tb_art_liqui_cod_art Foreign Key(cod_art)
			References tb_articulos(cod_art) 

)
GO

-- Ingreso de Data a las Tablas
set dateformat dmy
set language spanish
go
 
INSERT INTO tb_articulos VALUES ('A0001','MOUSE GENIOUS','UNIDAD', 25,235, 'No')
INSERT INTO tb_articulos VALUES ('A0002','PENTIUM III 600','UNIDAD',150,0, 'No')
INSERT INTO tb_articulos VALUES ('A0003','PENTIUM IV 2.5 GB','UNIDAD',150,220, 'No')
INSERT INTO tb_articulos VALUES ('A0004','FUNDAS NAYLON','METROS', 40, 35, 'No')
INSERT INTO tb_articulos VALUES ('A0005','MEMORIA ZIP 32','PAQUETE', 60, 0, 'No')
INSERT INTO tb_articulos VALUES ('A0006','TINTA BJC21 B/N','CAJA', 20, 0, 'No')
INSERT INTO tb_articulos VALUES ('A0007','IMPRESORA EPSON 1234','PAQUETE',355,120, 'No')
INSERT INTO tb_articulos VALUES ('A0008','MONITOR SYNMASTER 3N','UNIDAD',0, 33, 'No')
INSERT INTO tb_articulos VALUES ('A0009','MONITOR VIEWSONIC 17','UNIDAD',450, 92, 'No')
INSERT INTO tb_articulos VALUES ('A0010','PENTIUM MMX 260','UNIDAD',120, 97, 'No')
INSERT INTO tb_articulos VALUES ('A0011','MOUSE MICROSOFT','UNIDAD', 50,0, 'No')
INSERT INTO tb_articulos VALUES ('A0012','MEMORIA 2 GB','PAQUETE', 60, 25, 'No')
INSERT INTO tb_articulos VALUES ('A0013','MEMORIA 4 GB','PAQUETE', 92, 25, 'No')
INSERT INTO tb_articulos VALUES ('A0014','IMPRESORA CANON 1000','UNIDAD',205,0, 'No')
INSERT INTO tb_articulos VALUES ('A0015','IMPRESORA Samsung Laser','UNIDAD',1805,200, 'No')
INSERT INTO tb_articulos VALUES ('A0016','TINTA BJC21 COLOR','CAJA', 20,0, 'No')
INSERT INTO tb_articulos VALUES ('A0017','TINTA B/n  484','CAJA', 20,120, 'No')
INSERT INTO tb_articulos VALUES ('A0018','TINTA Color 624','CAJA', 20,0, 'No')
INSERT INTO tb_articulos VALUES ('A0019','TECLADO EPSON 102','UNIDAD', 75,122, 'No')
INSERT INTO tb_articulos VALUES ('A0020','MOUSE TECH','UNIDAD', 30,0, 'No')
INSERT INTO tb_articulos VALUES ('A0021','USB KINGSTON 2GB','UNIDAD',60, 97, 'No')
INSERT INTO tb_articulos VALUES ('A0022','USB KINGSTON 4GB','UNIDAD',90, 0, 'No')
INSERT INTO tb_articulos VALUES ('A0023','USB KINGSTON 8GB','UNIDAD',120, 25, 'No')
INSERT INTO tb_articulos VALUES ('A0024','AMPLIFICADOR TRINITON','UNIDAD',100,20, 'No')
INSERT INTO tb_articulos VALUES ('A0025','PARLANTES DE 50 watss','UNIDAD', 70,12, 'No')
INSERT INTO tb_articulos VALUES ('A0026','TECLADO EPSON 102','UNIDAD', 75,122, 'No')
INSERT INTO tb_articulos VALUES ('A0027','MOUSE TECH INALAMBRICO','UNIDAD', 80,190, 'No')
GO

INSERT INTO tb_articulos 
VALUES('A0028', 'CD PRINCO X 50', 'CONO', 25, 0, 'No'),
      ('A0029', 'DVD PRINCO X 50', 'CONO', 30, 20, 'No'),
      ('A0030', 'SCANNER CODIGO DE BARRAS', 'UNI', 190, 10, 'No'),
      ('A0031', 'CD PRINCO X 100', 'CONO', 45, 10, 'No'),
      ('A0032', 'DVD PRINCO X 100', 'CONO', 55, 20, 'No'),
      ('A0033', 'LECTOR-QUEMADOR CD LG', 'UNI', 45, 10, 'No'),
      ('A0034', 'LECTOR-QUEMADOR DVD LG', 'UNI', 75, 0, 'No')
GO

SET NOCOUNT OFF
SELECT MENSAJE='BASE DE DATOS BDVENTAS2024API CREADA CORRECTAMENTE'
GO



