-- =============================================
-- BASE DE DATOS: SistemaAutobuses
-- Sistema de Control de Autobuses
-- =============================================

CREATE DATABASE SistemaAutobuses;
GO

USE SistemaAutobuses;
GO

-- =============================================
-- TABLAS
-- =============================================

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(256) NOT NULL,
    TipoUsuario NVARCHAR(20) NOT NULL CHECK (TipoUsuario IN ('Administrador', 'Usuario'))
);

CREATE TABLE Choferes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Cedula NVARCHAR(20) NOT NULL UNIQUE
);

CREATE TABLE Autobuses (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Marca NVARCHAR(50) NOT NULL,
    Modelo NVARCHAR(50) NOT NULL,
    Placa NVARCHAR(20) NOT NULL UNIQUE,
    Color NVARCHAR(30) NOT NULL,
    Anio INT NOT NULL
);

CREATE TABLE Rutas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreRuta NVARCHAR(100) NOT NULL UNIQUE,
    Descripcion NVARCHAR(250) NULL
);

CREATE TABLE Asignaciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ChoferId INT NOT NULL,
    AutobusId INT NOT NULL,
    RutaId INT NOT NULL,
    FechaAsignacion DATETIME NOT NULL DEFAULT GETDATE(),
    Activa BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Asignaciones_Choferes FOREIGN KEY (ChoferId) REFERENCES Choferes(Id),
    CONSTRAINT FK_Asignaciones_Autobuses FOREIGN KEY (AutobusId) REFERENCES Autobuses(Id),
    CONSTRAINT FK_Asignaciones_Rutas FOREIGN KEY (RutaId) REFERENCES Rutas(Id)
);
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS - USUARIOS
-- =============================================

CREATE PROCEDURE sp_ValidarUsuario
    @NombreUsuario NVARCHAR(50),
    @Contrasena NVARCHAR(256)
AS
BEGIN
    SELECT Id, NombreUsuario, TipoUsuario
    FROM Usuarios
    WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contrasena;
END
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS - CHOFERES
-- =============================================

CREATE PROCEDURE sp_ObtenerChoferes
AS
BEGIN
    SELECT Id, Nombre, Apellido, FechaNacimiento, Cedula
    FROM Choferes
    ORDER BY Apellido, Nombre;
END
GO

CREATE PROCEDURE sp_ObtenerChoferPorId
    @Id INT
AS
BEGIN
    SELECT Id, Nombre, Apellido, FechaNacimiento, Cedula
    FROM Choferes
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_InsertarChofer
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @FechaNacimiento DATE,
    @Cedula NVARCHAR(20)
AS
BEGIN
    INSERT INTO Choferes (Nombre, Apellido, FechaNacimiento, Cedula)
    VALUES (@Nombre, @Apellido, @FechaNacimiento, @Cedula);
END
GO

CREATE PROCEDURE sp_ActualizarChofer
    @Id INT,
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @FechaNacimiento DATE,
    @Cedula NVARCHAR(20)
AS
BEGIN
    UPDATE Choferes
    SET Nombre = @Nombre, Apellido = @Apellido, 
        FechaNacimiento = @FechaNacimiento, Cedula = @Cedula
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_EliminarChofer
    @Id INT
AS
BEGIN
    DELETE FROM Choferes WHERE Id = @Id;
END
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS - AUTOBUSES
-- =============================================

CREATE PROCEDURE sp_ObtenerAutobuses
AS
BEGIN
    SELECT Id, Marca, Modelo, Placa, Color, Anio
    FROM Autobuses
    ORDER BY Marca, Modelo;
END
GO

CREATE PROCEDURE sp_ObtenerAutobusPorId
    @Id INT
AS
BEGIN
    SELECT Id, Marca, Modelo, Placa, Color, Anio
    FROM Autobuses
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_InsertarAutobus
    @Marca NVARCHAR(50),
    @Modelo NVARCHAR(50),
    @Placa NVARCHAR(20),
    @Color NVARCHAR(30),
    @Anio INT
AS
BEGIN
    INSERT INTO Autobuses (Marca, Modelo, Placa, Color, Anio)
    VALUES (@Marca, @Modelo, @Placa, @Color, @Anio);
END
GO

CREATE PROCEDURE sp_ActualizarAutobus
    @Id INT,
    @Marca NVARCHAR(50),
    @Modelo NVARCHAR(50),
    @Placa NVARCHAR(20),
    @Color NVARCHAR(30),
    @Anio INT
AS
BEGIN
    UPDATE Autobuses
    SET Marca = @Marca, Modelo = @Modelo, Placa = @Placa, 
        Color = @Color, Anio = @Anio
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_EliminarAutobus
    @Id INT
AS
BEGIN
    DELETE FROM Autobuses WHERE Id = @Id;
END
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS - RUTAS
-- =============================================

CREATE PROCEDURE sp_ObtenerRutas
AS
BEGIN
    SELECT Id, NombreRuta, Descripcion
    FROM Rutas
    ORDER BY NombreRuta;
END
GO

CREATE PROCEDURE sp_ObtenerRutaPorId
    @Id INT
AS
BEGIN
    SELECT Id, NombreRuta, Descripcion
    FROM Rutas
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_InsertarRuta
    @NombreRuta NVARCHAR(100),
    @Descripcion NVARCHAR(250)
AS
BEGIN
    INSERT INTO Rutas (NombreRuta, Descripcion)
    VALUES (@NombreRuta, @Descripcion);
END
GO

CREATE PROCEDURE sp_ActualizarRuta
    @Id INT,
    @NombreRuta NVARCHAR(100),
    @Descripcion NVARCHAR(250)
AS
BEGIN
    UPDATE Rutas
    SET NombreRuta = @NombreRuta, Descripcion = @Descripcion
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_EliminarRuta
    @Id INT
AS
BEGIN
    DELETE FROM Rutas WHERE Id = @Id;
END
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS - ASIGNACIONES
-- =============================================

CREATE PROCEDURE sp_ObtenerAsignaciones
AS
BEGIN
    SELECT a.Id, a.ChoferId, a.AutobusId, a.RutaId, a.FechaAsignacion, a.Activa,
           c.Nombre + ' ' + c.Apellido AS NombreChofer,
           b.Marca + ' ' + b.Modelo + ' (' + b.Placa + ')' AS InfoAutobus,
           r.NombreRuta
    FROM Asignaciones a
    INNER JOIN Choferes c ON a.ChoferId = c.Id
    INNER JOIN Autobuses b ON a.AutobusId = b.Id
    INNER JOIN Rutas r ON a.RutaId = r.Id
    WHERE a.Activa = 1
    ORDER BY a.FechaAsignacion DESC;
END
GO

CREATE PROCEDURE sp_InsertarAsignacion
    @ChoferId INT,
    @AutobusId INT,
    @RutaId INT
AS
BEGIN
    INSERT INTO Asignaciones (ChoferId, AutobusId, RutaId, FechaAsignacion, Activa)
    VALUES (@ChoferId, @AutobusId, @RutaId, GETDATE(), 1);
END
GO

CREATE PROCEDURE sp_EliminarAsignacion
    @Id INT
AS
BEGIN
    UPDATE Asignaciones SET Activa = 0 WHERE Id = @Id;
END
GO

-- =============================================
-- PROCEDIMIENTOS DE DISPONIBILIDAD
-- =============================================

CREATE PROCEDURE sp_ObtenerChoferesDisponibles
AS
BEGIN
    SELECT c.Id, c.Nombre, c.Apellido, c.FechaNacimiento, c.Cedula
    FROM Choferes c
    WHERE c.Id NOT IN (
        SELECT ChoferId FROM Asignaciones WHERE Activa = 1
    )
    ORDER BY c.Apellido, c.Nombre;
END
GO

CREATE PROCEDURE sp_ObtenerAutobusesDisponibles
AS
BEGIN
    SELECT a.Id, a.Marca, a.Modelo, a.Placa, a.Color, a.Anio
    FROM Autobuses a
    WHERE a.Id NOT IN (
        SELECT AutobusId FROM Asignaciones WHERE Activa = 1
    )
    ORDER BY a.Marca, a.Modelo;
END
GO

CREATE PROCEDURE sp_ObtenerRutasDisponibles
AS
BEGIN
    SELECT r.Id, r.NombreRuta, r.Descripcion
    FROM Rutas r
    WHERE r.Id NOT IN (
        SELECT RutaId FROM Asignaciones WHERE Activa = 1
    )
    ORDER BY r.NombreRuta;
END
GO

-- =============================================
-- DATOS INICIALES
-- =============================================

INSERT INTO Usuarios (NombreUsuario, Contrasena, TipoUsuario) VALUES
('admin', 'admin123', 'Administrador'),
('usuario', 'usuario123', 'Usuario');

INSERT INTO Rutas (NombreRuta, Descripcion) VALUES
('Villa Mella', 'Ruta hacia Villa Mella'),
('Sabana', 'Ruta hacia la Sabana'),
('La Charles', 'Ruta hacia La Charles de Gaulle'),
('La Churchill', 'Ruta hacia la Av. Winston Churchill'),
('Puente Juan Carlos', 'Ruta hacia Puente Juan Carlos');
GO
