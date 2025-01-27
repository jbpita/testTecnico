IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TestDB')
BEGIN
    CREATE DATABASE TestDB;
    PRINT 'DATABASE WAS CREATED SUCCESSFULLY';
END;
GO


USE TestDB;
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Personas]') AND type in (N'U'))
BEGIN
CREATE TABLE Personas (
    Identificador INT IDENTITY PRIMARY KEY,
    Nombres NVARCHAR(50) NOT NULL,
    Apellidos NVARCHAR(50) NOT NULL,
    NumeroIdentificacion NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    TipoIdentificacion NVARCHAR(20) NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    NumeroIdentificacionCompleto AS (TipoIdentificacion + '-' + NumeroIdentificacion),
    NombreCompleto AS (Nombres + ' ' + Apellidos)
);
END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
BEGIN
CREATE TABLE Usuarios (
    Identificador INT IDENTITY PRIMARY KEY,
    Persona INT NOT NULL,
    Usuario NVARCHAR(50) NOT NULL,
    Pass NVARCHAR(25) NOT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE()
);
END;
GO
