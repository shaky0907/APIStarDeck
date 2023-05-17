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

<<<<<<< Updated upstream
=======
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
	Id_actividad INT NOT NULL,
	CONSTRAINT PK_Usuario PRIMARY KEY (Id)
);


CREATE TABLE Actividad (
	
	Id int NOT NULL,
	Nombre_act VARCHAR(20) NOT NULL,
	CONSTRAINT PK_Actividad PRIMARY KEY (Id)

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



CREATE TABLE Deck(
	
	Id VARCHAR(15) NOT NULL,
	Nombre VARCHAR(20) NOT NULL,
	Estado BIT NOT NULL,
	Id_usuario VARCHAR(15) NOT NULL
	CONSTRAINT PK_Deck PRIMARY KEY (Id)

);

CREATE TABLE CartasXDeck(

	Id_Deck VARCHAR(15) NOT NULL,
	Id_Carta VARCHAR(15) NOT NULL
	CONSTRAINT PK_Ids_cxd PRIMARY KEY (Id_Deck, Id_Carta)
);

CREATE TABLE Planeta(
	
	Id VARCHAR(15) NOT NULL,
	Nombre VARCHAR(20) NOT NULL,
	Tipo INT NOT NULL,
	Descripcion VARCHAR(50) NOT NULL,
	Estado BIT NOT NULL,
	Imagen VARCHAR(MAX) NOT NULL
	CONSTRAINT PK_Planeta PRIMARY KEY (Id)

);


CREATE TABLE Tipo_planeta(
	
	Id INT NOT NULL,
	Nombre VARCHAR(15) NOT NULL,
	Probabilidad INT NOT NULL,
	CONSTRAINT PK_Tipo_planeta PRIMARY KEY (Id)

);

CREATE TABLE Partida(
	
	Id VARCHAR(15) NOT NULL,
	Estado INT NOT NULL,
	Fecha_hora DATETIME NOT NULL,
	CONSTRAINT PK_Partida PRIMARY KEY (Id)

);


CREATE TABLE Turno(

	Id VARCHAR(15),
	Id_Partida VARCHAR(15),
	Numero_turno INT
	CONSTRAINT PK_Turno PRIMARY KEY (Id)
)

CREATE TABLE TurnoXUsuario(
	
	Id_turno VARCHAR(15),
	Id_Usuario VARCHAR(15),
	Energia_inicial INT,
	Energia_gastada INT,
	Revela_primero BIT
	CONSTRAINT PK_Ids_txu PRIMARY KEY (Id_turno, Id_Usuario)
	
);

CREATE TABLE CartasXTurnoXPlaneta(

	Id_Carta VARCHAR(15),
	Id_Turno VARCHAR(15),
	Id_Planeta VARCHAR(15)
	CONSTRAINT PK_Ids_cxtxp PRIMARY KEY (Id_Carta, Id_Turno, Id_Planeta)
);



CREATE TABLE Estado_Partida(
	
	Id INT NOT NULL,
	Nombre VARCHAR(20)
	CONSTRAINT PK_Estado_Partida PRIMARY KEY (Id)

);

CREATE TABLE UsuarioXPartida(

	Id_Usuario VARCHAR(15),
	Id_Partida VARCHAR(15),
	Id_Master BIT,
	Ganador BIT,
	Monedas_ingreso INT,
	Monedas_apuesta INT
	CONSTRAINT PK_Ids_uxp PRIMARY KEY (Id_Usuario, Id_Partida)

);


CREATE TABLE PlanetasXPartida(

	Id_Planeta VARCHAR(15),
	Id_Partida VARCHAR(15)
	CONSTRAINT PK_Ids_pxp PRIMARY KEY (Id_Planeta, Id_Partida)

);


>>>>>>> Stashed changes
--Constraints
ALTER TABLE Carta 
ADD CONSTRAINT FK_Carta_Tipo 
FOREIGN KEY (Tipo) REFERENCES Tipo (Id);

ALTER TABLE Carta 
ADD CONSTRAINT FK_Carta_Raza 
FOREIGN KEY (Raza) REFERENCES Raza (Id);


drop table Carta
drop table Raza
drop table Tipo