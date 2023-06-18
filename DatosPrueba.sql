
INSERT INTO Avatar (Id, Imagen)
	VALUES (1, 'AVATAR1');

INSERT INTO Usuario (Id, Administrador, Nombre, Username,
	Contrasena, Correo, Nacionalidad, Estado, Avatar, Ranking, Monedas, Id_actividad)
	VALUES('123124', 0, 'David', 'Shaky', '*/*', 'shaky09', 1, 1, 1, 100, 100, 1);

INSERT INTO Usuario (Id, Administrador, Nombre, Username,
	Contrasena, Correo, Nacionalidad, Estado, Avatar, Ranking, Monedas, Id_actividad)
	VALUES('123125', 0, 'Jenni', 'LaWapa', '*/*', 'jennialv', 1, 1, 1, 100, 100, 2);

INSERT INTO Usuario (Id, Administrador, Nombre, Username,
	Contrasena, Correo, Nacionalidad, Estado, Avatar, Ranking, Monedas, Id_actividad)
	VALUES('123126', 0, 'Richard', 'Rixard', '*/*', 'richardgmail', 1, 1, 1, 100, 100, 2);

INSERT INTO Carta(Id, N_personaje, Energia, C_batalla, Imagen, Raza, Activa, Descripcion, Tipo)
	VALUES(1, 'Carta1', 20, 5, 'imagen1', 1, 1, 'Es la primera carta', 5),
		(2, 'Carta2', 30, 4, 'imagen2', 1, 1, 'Es la segunda carta', 5),
		(3, 'Carta3', 50, 7, 'imagen3', 1, 1, 'Es la tercera carta', 5),
		(4, 'Carta4', 20, 5, 'imagen1', 1, 1, 'Es la primera carta', 5),
		(5, 'Carta5', 30, 4, 'imagen2', 1, 1, 'Es la segunda carta', 5),
		(6, 'Carta6', 50, 7, 'imagen3', 1, 1, 'Es la tercera carta', 5),
		(7, 'Carta7', 20, 5, 'imagen1', 1, 1, 'Es la primera carta', 5),
		(8, 'Carta8', 30, 4, 'imagen2', 1, 1, 'Es la segunda carta', 5),
		(9, 'Carta9', 50, 7, 'imagen3', 1, 1, 'Es la tercera carta', 5),
		(10, 'Carta10', 20, 5, 'imagen1', 1, 1, 'Es la primera carta', 5),
		(11, 'Carta11', 30, 4, 'imagen2', 1, 1, 'Es la segunda carta', 5),
		(12, 'Carta12', 50, 7, 'imagen3', 1, 1, 'Es la tercera carta', 5),
		(13, 'Carta13', 20, 5, 'imagen1', 1, 1, 'Es la primera carta', 5),
		(14, 'Carta14', 30, 4, 'imagen2', 1, 1, 'Es la segunda carta', 5),
		(15, 'Carta15', 50, 7, 'imagen3', 1, 1, 'Es la tercera carta', 5),
		(16, 'Carta16', 20, 5, 'imagen1', 1, 1, 'Es la primera carta', 4),
		(17, 'Carta17', 30, 4, 'imagen2', 1, 1, 'Es la segunda carta', 3),
		(18, 'Carta18', 50, 7, 'imagen3', 1, 1, 'Es la tercera carta', 4),
		(19, 'Carta19', 20, 5, 'imagen1', 1, 1, 'Es la primera carta', 3),
		(20, 'Carta20', 30, 4, 'imagen2', 1, 1, 'Es la segunda carta', 3),
		(21, 'Carta21', 50, 7, 'imagen3', 1, 1, 'Es la tercera carta', 4),
		(22, 'Carta22', 20, 5, 'imagen1', 1, 1, 'Es la primera carta', 4),
		(23, 'Carta23', 30, 4, 'imagen2', 1, 1, 'Es la segunda carta', 3),
		(24, 'Carta24', 50, 7, 'imagen3', 1, 1, 'Es la tercera carta', 3),
		(25, 'Carta25', 20, 5, 'imagen1', 1, 1, 'Es la primera carta', 4),
		(26, 'Carta26', 30, 4, 'imagen2', 1, 1, 'Es la segunda carta', 4),
		(27, 'Carta27', 50, 7, 'imagen3', 1, 1, 'Es la tercera carta', 5);

INSERT INTO Planeta(Id, Nombre, Tipo, Descripcion, Estado, Imagen)
	VALUES ('1', 'Namek', 1,'Donde vive el senor picolo', 1, 'picolo'),
			('2', 'Yadrat', 2,'Donde goku aprende el tp', 1, 'tp'),
			('3', 'Planeta Dioses', 3,'goku eta vaina e seria', 1, 'dioses');

SELECT * FROM Partida

SELECT * FROM UsuarioXPartida

SELECT * FROM PlanetasXPartida

SELECT * FROM Usuario

DELETE FROM UsuarioXPartida

DELETE FROM PlanetasXPartida

DELETE FROM Partida

DELETE FROM Usuario WHERE Id = 123126

DELETE FROM Usuario WHERE Id = 123125

SELECT * FROM TURNOXUSUARIO