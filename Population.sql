Use StarDeck

INSERT INTO Tipo (Id, Nombre)
VALUES (1, 'Ultra-Rara'),
       (2, 'Muy Rara'),
       (3, 'Rara'),
       (4, 'Normal'),
       (5, 'Básica');



INSERT INTO Raza (Id, Nombre)
VALUES (1, 'Raza 1'),
       (2, 'Raza 2'),
       (3, 'Raza 3'),
       (4, 'Raza 4'),
       (5, 'Raza 5');
	   
INSERT INTO Paises (Id, Nombre)
VALUES (1, 'Estados Unidos'),
       (2, 'México'),
       (3, 'Costa Rica');


INSERT INTO ACTIVIDAD(Id, Nombre_act)
VALUES (1, 'No busca partida'),	
		(2, 'Buscando partida'),
		(3, 'En partida');


INSERT INTO TIPO_PLANETA(Id, Nombre, Probabilidad)
VALUES (1, 'Popular', 50),
		(2, 'Basico', 35),
		(3, 'Raro', 15);


INSERT INTO Estado_Partida(Id, Nombre)
VALUES (1, 'Creada'),
		(2, 'Emaparejada'),
		(3, 'En curso'),
		(4, 'Terminada');



INSERT INTO Parametros(Id, Tiempo_turno, Turnos_totales, Cartas_Mano_Inicial, Energia_Inicial)
Values (1, 10, 18, 5, 100);
