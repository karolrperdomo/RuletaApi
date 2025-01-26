/**

-- ======================================
-- Creación de la base de datos
-- ======================================

-- Crear la base de datos Ruleta
create database Ruleta;

-- Usar la base de datos recién creada
use Ruleta;

-- Tabla: Reglas
-- Descripción: Define las reglas del juego y su estado(Activa)
CREATE TABLE Reglas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Activa BIT NOT NULL DEFAULT 0, --Estado de la regla (0 = Inactiva, 1 = Activa)
    FechaCreacion DATETIME NOT NULL
);

-- Tabla: Apuestas
-- Descripción: Registra las apuestas realizadas por los usuarios.
CREATE TABLE Apuestas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    ReglaId INT NOT NULL,
    TipoApuesta NVARCHAR(50) NOT NULL,
    ValorApuesta NVARCHAR(50) NOT NULL,
    Monto DECIMAL(18, 2) NOT NULL,
    FechaApuesta DATETIME NOT NULL,
    FOREIGN KEY (ReglaId) REFERENCES Reglas(Id)
);

-- Tabla: Resultados
-- Descripción: Almacena los resultados de las reglas jugadas.
CREATE TABLE Resultados (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ReglaId INT NOT NULL,
    NumeroGanador INT NOT NULL,
    ColorGanador NVARCHAR(20) NOT NULL,
    MontoGanado DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (ReglaId) REFERENCES Reglas(Id)
);

-- Tabla: Usuarios
-- Descripción: Contiene la información de los usuarios y su crédito disponible.
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    Nombre NVARCHAR(100) NOT NULL,   
    Credito DECIMAL(10, 2) NOT NULL  
);

-- ======================================
-- Inserción de datos iniciales
-- ======================================

-- Insertar datos iniciales en la tabla Reglas
INSERT INTO Reglas (Activa, FechaCreacion)
VALUES (0, GETDATE());

-- Insertar datos iniciales en la tabla Apuestas para pruebas
INSERT INTO Apuestas (UsuarioId, ReglaId, TipoApuesta, ValorApuesta, Monto, FechaApuesta)
VALUES (1, 1, 'Numero', '17', 100.00, GETDATE()); -- Usuario 1 apuesta 100 al número 17

-- Insertar datos iniciales en la tabla Resultados para pruebas
INSERT INTO Resultados (ReglaId, NumeroGanador, ColorGanador, MontoGanado)
VALUES (1, 17, 'Rojo', 500.00);

-- Insertar datos iniciales en la tabla Usuarios
INSERT INTO Usuarios (Nombre, Credito) VALUES
('Usuario1', 5000.00),
('Usuario2', 10000.00),
('Usuario3', 7500.00);

-- ======================================
-- Consultas de prueba
-- ======================================

-- Consultar todas las apuestas relacionadas con la primera regla
SELECT * FROM Apuestas WHERE ReglaId = 1;

-- Consultar los resultados asociados a la primera regla
SELECT * FROM Resultados WHERE ReglaId = 1;

-- Consultar todos los usuarios registrados en el sistema
SELECT * FROM Usuarios;*/
