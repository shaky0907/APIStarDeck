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

CREATE TABLE CartaXUsuario (

	Id_usuario VARCHAR(15) NOT NULL,
	Id_carta VARCHAR(15) NOT NULL
	CONSTRAINT PK_Ids_cxus PRIMARY KEY (Id_usuario, Id_carta)

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


CREATE TABLE TurnoXUsuario(
	
	Id VARCHAR(15) NOT NULL,
	Id_Partida VARCHAR(15) NOT NULL,
	Numero_turno INT NOT NULL,
	Id_Usuario VARCHAR(15) NOT NULL,
	Energia INT,
	Terminado BIT
	CONSTRAINT PK_Ids_txu PRIMARY KEY (Id)
	
);

CREATE TABLE CartasXTurnoXPlanetaXUsuario(

	Id_Carta VARCHAR(15),
	Id_Turno VARCHAR(15),
	Id_Planeta VARCHAR(15),
	Id_Usuario VARCHAR(15),
	CONSTRAINT PK_Ids_cxtxp PRIMARY KEY (Id_Turno, Id_Planeta, Id_Usuario, Id_Carta)
);



CREATE TABLE CartasXTurnoXDeckXUsuario(

	Id_Carta VARCHAR(15),
	Id_Turno VARCHAR(15),
	Id_Usuario VARCHAR(15),
	Posicion INT
	CONSTRAINT PK_Ids_cxtxdxu PRIMARY KEY (Id_Turno, Id_Carta, Id_Usuario)
);

CREATE TABLE CartasXTurnoXManoXUsuario(

	Id_Carta VARCHAR(15),
	Id_Turno VARCHAR(15),
	Id_Usuario VARCHAR(15),
	Posicion INT
	CONSTRAINT PK_Ids_cxtxmxu PRIMARY KEY (Id_Turno, Id_Carta, Id_Usuario)
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

CREATE TABLE Parametros(
	Id Int,
	Tiempo_turno INT,
	Turnos_totales INT,
	Cartas_Mano_Inicial int,
	Energia_Inicial INT
	CONSTRAINT PK_Params PRIMARY KEY (Id)
);


--Constraints
ALTER TABLE Carta 
ADD CONSTRAINT FK_Carta_Tipo 
FOREIGN KEY (Tipo) REFERENCES Tipo (Id);

ALTER TABLE Carta 
ADD CONSTRAINT FK_Carta_Raza 
FOREIGN KEY (Raza) REFERENCES Raza (Id);


ALTER TABLE Usuario
ADD CONSTRAINT FK_Usuario_Actividad
FOREIGN KEY (Id_actividad) REFERENCES Actividad (Id);

ALTER TABLE CartaXUsuario
ADD CONSTRAINT FK_Carta_User
FOREIGN KEY (Id_carta) REFERENCES Carta;

ALTER TABLE CartaXUsuario
ADD CONSTRAINT FK_Usuario_Cards
FOREIGN KEY (Id_usuario) REFERENCES Usuario;

ALTER TABLE Usuario
ADD CONSTRAINT FK_Usuario_Nacionalidad
FOREIGN KEY (Nacionalidad) REFERENCES Paises (Id);

ALTER TABLE Usuario
ADD CONSTRAINT FK_Usuario_Avatar
FOREIGN KEY (Avatar) REFERENCES Avatar (Id);

ALTER TABLE CartasXDeck
ADD CONSTRAINT FK_Carta_D
FOREIGN KEY (Id_Carta) REFERENCES Carta (Id);

ALTER TABLE CartasXDeck
ADD CONSTRAINT FK_Deck_C
FOREIGN KEY (Id_Deck) REFERENCES Deck (Id);

ALTER TABLE Planeta
ADD CONSTRAINT FK_Tipo_planeta
FOREIGN KEY (Tipo) REFERENCES Tipo_Planeta (Id);

ALTER TABLE Partida
ADD CONSTRAINT FK_Estado_partida
FOREIGN KEY (Estado) REFERENCES Estado_Partida (Id);

ALTER TABLE TurnoXUsuario
ADD CONSTRAINT FK_Partida_Turno
FOREIGN KEY (Id_Partida) REFERENCES Partida (Id);


ALTER TABLE TurnoXUsuario
ADD CONSTRAINT FK_Turno_Usuario_U
FOREIGN KEY (Id_Usuario) REFERENCES Usuario (Id);


ALTER TABLE CartasXTurnoXPlanetaXUsuario
ADD CONSTRAINT FK_Carta_T_P
FOREIGN KEY (Id_Carta) REFERENCES Carta (Id);

ALTER TABLE CartasXTurnoXPlanetaXUsuario
ADD CONSTRAINT FK_Turnos_C_P
FOREIGN KEY (Id_Turno) REFERENCES TurnoXUsuario (Id);

ALTER TABLE CartasXTurnoXPlanetaXUsuario
ADD CONSTRAINT FK_Planeta_C_T
FOREIGN KEY (Id_Planeta) REFERENCES Planeta (Id);

ALTER TABLE CartasXTurnoXPlanetaXUsuario
ADD CONSTRAINT FK_Planeta_C_U
FOREIGN KEY (Id_Usuario) REFERENCES Usuario (Id);


ALTER TABLE CartasXTurnoXDeckXUsuario
ADD CONSTRAINT FK_Carta_C_x
FOREIGN KEY (Id_Carta) REFERENCES Carta (Id);

ALTER TABLE CartasXTurnoXDeckXUsuario
ADD CONSTRAINT FK_Turnos_T_x
FOREIGN KEY (Id_Turno) REFERENCES TurnoXUsuario (Id);

ALTER TABLE CartasXTurnoXDeckXUsuario
ADD CONSTRAINT FK_Planeta_U_x
FOREIGN KEY (Id_Usuario) REFERENCES Usuario (Id);


ALTER TABLE CartasXTurnoXManoXUsuario
ADD CONSTRAINT FK_Carta_C_x_m
FOREIGN KEY (Id_Carta) REFERENCES Carta (Id);

ALTER TABLE CartasXTurnoXManoXUsuario
ADD CONSTRAINT FK_Turnos_T_x_m
FOREIGN KEY (Id_Turno) REFERENCES TurnoXUsuario (Id);

ALTER TABLE CartasXTurnoXManoXUsuario
ADD CONSTRAINT FK_Planeta_U_x_m
FOREIGN KEY (Id_Usuario) REFERENCES Usuario (Id);


ALTER TABLE UsuarioXPartida
ADD CONSTRAINT FK_Usuario
FOREIGN KEY (Id_Usuario) REFERENCES Usuario (Id);


ALTER TABLE UsuarioXPartida
ADD CONSTRAINT FK_Partida_U
FOREIGN KEY (Id_Partida) REFERENCES Partida (Id);


ALTER TABLE PlanetasXPartida
ADD CONSTRAINT FK_Planetas_P
FOREIGN KEY (Id_Planeta) REFERENCES Planeta (Id);


ALTER TABLE PlanetasXPartida
ADD CONSTRAINT FK_Partida_P
FOREIGN KEY (Id_Partida) REFERENCES Partida (Id);


