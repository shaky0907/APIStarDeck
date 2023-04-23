create database StarDeck

--tablas
Use StarDeck

CREATE TABLE Carta (
    Id VARCHAR(15) NOT NULL,
    N_personaje VARCHAR(30) NOT NULL,
    Energia INT NOT NULL,
    C_batalla INT NOT NULL,
    Imagen VARCHAR(MAX),
    Raza INT NOT NULL,
    Activa BIT NOT NULL,
    Descripcion VARCHAR(1000) NOT NULL,
    Tipo INT NOT NULL,
    CONSTRAINT PK_Carta PRIMARY KEY (Id)
);


CREATE TABLE Raza (
	Id INT NOT NULL,
	Nombre VARCHAR(20) NOT NULL
	CONSTRAINT PK_Raza PRIMARY KEY (Id)
);

CREATE TABLE Tipo (
	Id INT NOT NULL,
	Nombre VARCHAR(20) NOT NULL
	CONSTRAINT PK_Tipo PRIMARY KEY (Id)
);


CREATE TABLE Usuario (
	Id VARCHAR(15) NOT NULL,
	Administrador BIT NOT NULL,
	Nombre VARCHAR(30) NOT NULL,
	Username VARCHAR(30) NOT NULL,
	Contrasena VARCHAR(50) NOT NULL,
	Correo VARCHAR(50) NOT NULL,
	Nacionalidad int NOT NULL,
	Estado BIT NOT NULL,
	Avatar int NOT NULL,
	Ranking int NOT NULL,
	Monedas int NOT NULL,
	CONSTRAINT PK_Usuario PRIMARY KEY (Id)
);

CREATE TABLE Paises(
	Id INT NOT NULL,
	Nombre VARCHAR(20),
	CONSTRAINT PK_Paises PRIMARY KEY (Id)
);


CREATE TABLE Avatar(
	Id INT NOT NULL,
	Imagen VARCHAR(MAX) NOT NULL,
	CONSTRAINT PK_Avatar PRIMARY KEY (Id)
);



--Constraints
ALTER TABLE Carta 
ADD CONSTRAINT FK_Carta_Tipo 
FOREIGN KEY (Tipo) REFERENCES Tipo (Id);

ALTER TABLE Carta 
ADD CONSTRAINT FK_Carta_Raza 
FOREIGN KEY (Raza) REFERENCES Raza (Id);

ALTER TABLE Usuario
ADD CONSTRAINT FK_Usuario_Nacionalidad
FOREIGN KEY (Nacionalidad) REFERENCES Paises (Id);

ALTER TABLE Usuario
ADD CONSTRAINT FK_Usuario_Avatar
FOREIGN KEY (Avatar) REFERENCES Avatar (Id);


use StarDeck

drop table Carta
drop table Raza
drop table Tipo
drop table Paises
drop table Usuario
drop table Avatar