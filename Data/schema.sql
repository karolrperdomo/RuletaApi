/**create database Ruleta;

use Ruleta;

CREATE TABLE Reglas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Activa BIT NOT NULL DEFAULT 0,
    FechaCreacion DATETIME NOT NULL
);

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

CREATE TABLE Resultados (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ReglaId INT NOT NULL,
    NumeroGanador INT NOT NULL,
    ColorGanador NVARCHAR(20) NOT NULL,
    MontoGanado DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (ReglaId) REFERENCES Reglas(Id)
);

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Identificador único del usuario
    Nombre NVARCHAR(100) NOT NULL,   -- Nombre del usuario (opcional para esta aplicación)
    Credito DECIMAL(10, 2) NOT NULL  -- Crédito disponible para realizar apuestas
);


INSERT INTO Reglas (Activa, FechaCreacion)
VALUES (0, GETDATE());

INSERT INTO Apuestas (UsuarioId, ReglaId, TipoApuesta, ValorApuesta, Monto, FechaApuesta)
VALUES (1, 1, 'Numero', '17', 100.00, GETDATE());

INSERT INTO Resultados (ReglaId, NumeroGanador, ColorGanador, MontoGanado)
VALUES (1, 17, 'Rojo', 500.00);

SELECT * FROM Apuestas WHERE ReglaId = 1;
SELECT * FROM Resultados WHERE ReglaId = 1;

INSERT INTO Usuarios (Nombre, Credito) VALUES
('Usuario1', 5000.00),
('Usuario2', 10000.00),
('Usuario3', 7500.00);

SELECT * FROM Usuarios;*/
