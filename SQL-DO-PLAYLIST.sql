-- -- Crear la base de datos
-- CREATE DATABASE MusicDB;

-- -- Conectarse a la base de datos
-- \c MusicDB;


DROP TABLE IF EXISTS Cancion CASCADE;
DROP TABLE IF EXISTS PlayList CASCADE;
DROP TABLE IF EXISTS PlayListCanciones CASCADE;

-- Crear la tabla "Cancion"
CREATE TABLE Cancion (
    CancionId SERIAL PRIMARY KEY,
    Titulo VARCHAR(100) NOT NULL,
    Artista VARCHAR(100) NOT NULL,
    Album VARCHAR(100),
    Genero VARCHAR(50),
    Duracion TIME
);

-- Insertar datos de prueba en la tabla "Canciones"
-- Insertar datos de canciones reales
INSERT INTO Cancion (Titulo, Artista, Album, Genero, Duracion) VALUES
('Bohemian Rhapsody', 'Queen', 'A Night at the Opera', 'Rock', '00:05:55'),
('Shape of You', 'Ed Sheeran', '÷ (Divide)', 'Pop', '00:03:53'),
('Despacito', 'Luis Fonsi ft. Daddy Yankee', 'Vida', 'Reggaeton', '00:03:48'),
('Hotel California', 'The Eagles', 'Hotel California', 'Rock', '00:06:30'),
('Billie Jean', 'Michael Jackson', 'Thriller', 'Pop', '00:04:54'),
('Stairway to Heaven', 'Led Zeppelin', 'Led Zeppelin IV', 'Rock', '00:08:02'),
('Thinking Out Loud', 'Ed Sheeran', 'x', 'Pop', '00:04:41'),
('Thunderstruck', 'AC/DC', 'The Razors Edge', 'Rock', '00:04:52'),
('Shape of My Heart', 'Sting', 'Ten Summoners Tales', 'Pop', '00:04:39'),
('Smells Like Teen Spirit', 'Nirvana', 'Nevermind', 'Grunge', '00:05:01'),
('Hey Jude', 'The Beatles', 'Hey Jude', 'Rock', '00:07:08'),
('Rolling in the Deep', 'Adele', '21', 'Pop', '00:03:48'),
('Blinding Lights', 'The Weeknd', 'After Hours', 'Synthpop', '00:03:20'),
('Lose Yourself', 'Eminem', '8 Mile', 'Hip-Hop', '00:05:26'),
('Wonderwall', 'Oasis', '(Whats the Story) Morning Glory?', 'Rock', '00:04:18'),
('Someone lik You', 'Adele', '21', 'Pop', '00:04:45'),
('Sweet Child o Mine', 'Guns N Roses', 'Appetite for Destruction', 'Rock', '00:05:56'),
('Rolling in the Deep', 'Adele', '21', 'Pop', '00:03:48'),
('Uptown Funk', 'Mark Ronson ft. Bruno Mars', 'Uptown Special', 'Funk', '00:04:30'),
('Perfect', 'Ed Sheeran', '÷ (Divide)', 'Pop', '00:04:23');




-- Crear la tabla "PlayList"
CREATE TABLE PlayList (
    PlayListId SERIAL PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion TEXT
);

INSERT INTO PlayList (Nombre, Descripcion) VALUES
('Mi Lista de Música', 'Una lista personalizada de canciones'),
('Favoritos', 'Mis canciones favoritas'),
('Clásicos del Rock', 'Grandes éxitos del rock de todos los tiempos'),
('Verano 2024', 'Canciones para disfrutar del verano'),
('Éxitos Pop', 'Las mejores canciones pop del momento'),
('Canciones para Entrenar', 'Lista energizante para el gimnasio'),
('Relax y Meditación', 'Música suave para relajarse y meditar'),
('Latin Party', 'Éxitos latinos para una fiesta increíble'),
('Bandas Sonoras Épicas', 'Música de películas y series'),
('Románticas', 'Canciones románticas para dedicar');



-- Crear la tabla intermedia "PlayListCanciones" para la relación many-to-many
CREATE TABLE PlayListCanciones (
    PlayListId INT REFERENCES PlayList(PlayListId),
    CancionId INT REFERENCES Cancion(CancionId),
    PRIMARY KEY (PlayListId, CancionId)
);

-- Insertar datos de prueba en la tabla "PlayListCanciones"
INSERT INTO PlayListCanciones (PlayListId, CancionId) VALUES
(1, 1), -- Agregar la canción 1 a la lista de reproducción 1
(1, 2), -- Agregar la canción 2 a la lista de reproducción 1
(1, 3), -- Agregar la canción 3 a la lista de reproducción 1
(2, 2), -- Agregar la canción 2 a la lista de reproducción 2
(2, 3); -- Agregar la canción 3 a la lista de reproducción 2


SELECT c.*
FROM PlayList p
INNER JOIN PlayListCanciones pc ON p.PlayListId = pc.PlayListId
INNER JOIN Cancion c ON pc.CancionId = c.CancionId
WHERE p.PlayListId = 1;
SELECT * FROM Playlist;