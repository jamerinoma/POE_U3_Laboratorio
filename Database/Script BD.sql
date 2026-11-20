-- Autoevaluación: 10.00
-- =========================================
-- Creación de la Base de Datos
-- =========================================
CREATE DATABASE Sistema_Gestion_Venta;
GO

USE Sistema_Gestion_Venta;
GO

-- =========================================
-- 1. Tabla Clientes
-- =========================================
CREATE TABLE Clientes (
    id_cliente INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    telefono NVARCHAR(20),
    email NVARCHAR(100) UNIQUE
);
GO

-- =========================================
-- 2. Tabla Empleados
-- =========================================
CREATE TABLE Empleados (
    id_empleado INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    cargo NVARCHAR(50) NOT NULL
);
GO

-- =========================================
-- 3. Tabla Productos
-- =========================================
CREATE TABLE Productos (
    id_producto INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(255),
    precio DECIMAL(10,2) NOT NULL CHECK (precio > 0),
    stock INT NOT NULL CHECK (stock >= 0)
);
GO

-- =========================================
-- 4. Tabla Ventas
-- =========================================
CREATE TABLE Ventas (
    id_venta INT PRIMARY KEY IDENTITY(1,1),
    fecha DATE NOT NULL,
    id_cliente INT NOT NULL,
    id_empleado INT NOT NULL,
    FOREIGN KEY (id_cliente) REFERENCES Clientes(id_cliente),
    FOREIGN KEY (id_empleado) REFERENCES Empleados(id_empleado)
);
GO

-- =========================================
-- 5. Tabla DetalleVenta
-- =========================================
CREATE TABLE DetalleVenta (
    id_detalle INT PRIMARY KEY IDENTITY(1,1),
    id_venta INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precio_unitario DECIMAL(10,2) NOT NULL CHECK (precio_unitario > 0),
    FOREIGN KEY (id_venta) REFERENCES Ventas(id_venta),
    FOREIGN KEY (id_producto) REFERENCES Productos(id_producto)
);
GO

-- =========================================
-- 6. Extensión de Acceso
-- =========================================

IF OBJECT_ID('dbo.UsuarioAcceso', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.UsuarioAcceso (
        id_usuario INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_UsuarioAcceso PRIMARY KEY,
        id_empleado INT NOT NULL CONSTRAINT FK_UsuarioAcceso_Empleados
            FOREIGN KEY REFERENCES dbo.Empleados(id_empleado),
        nombre_usuario NVARCHAR(50) NOT NULL CONSTRAINT UQ_UsuarioAcceso_Usuario UNIQUE,
        clave_hash VARBINARY(32) NOT NULL,
        clave_salt VARBINARY(16) NOT NULL,
        activo BIT NOT NULL CONSTRAINT DF_UsuarioAcceso_Activo DEFAULT (1)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.UsuarioAcceso WHERE nombre_usuario = N'admin')
BEGIN
    DECLARE @idEmpleado INT;
    DECLARE @sal VARBINARY(16) = CRYPT_GEN_RANDOM(16);
    INSERT dbo.Empleados (nombre, apellido, cargo)
    VALUES (N'Jaime', N'Merino', N'Administrador');
    SET @idEmpleado = CONVERT(INT, SCOPE_IDENTITY());
    INSERT dbo.UsuarioAcceso (id_empleado, nombre_usuario, clave_salt, clave_hash, activo)
    VALUES (@idEmpleado, N'admin', @sal,
        HASHBYTES('SHA2_256', @sal + CONVERT(VARBINARY(MAX), 'Admin123!')), 1);
END;
GO

-- =========================================
-- 7. Verificación
-- =========================================
SELECT e.id_empleado,e.nombre,e.apellido,u.nombre_usuario,u.activo,
       DATALENGTH(u.clave_hash) AS LongitudHash,
       DATALENGTH(u.clave_salt) AS LongitudSalt
FROM dbo.UsuarioAcceso u JOIN dbo.Empleados e ON e.id_empleado=u.id_empleado;

SELECT * FROM dbo.Productos ORDER BY nombre;

SELECT p.nombre, SUM(d.cantidad) AS unidades_vendidas,
       SUM(d.cantidad * d.precio_unitario) AS monto_vendido
FROM dbo.DetalleVenta d JOIN dbo.Productos p ON p.id_producto=d.id_producto
GROUP BY p.nombre ORDER BY monto_vendido DESC;
GO
