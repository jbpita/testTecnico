USE TestDB;
GO

IF OBJECT_ID(N'[dbo].[GetPersonasPaginated]', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE [dbo].[GetPersonasPaginated];
END;
GO

CREATE PROCEDURE GetPersonasPaginated
    @PageNumber INT = 1,                -- Número de página
    @PageSize INT = 10,                 -- Tamaño de página
    @Nombre NVARCHAR(50) = NULL,        -- Filtro por nombre
    @Apellido NVARCHAR(50) = NULL,      -- Filtro por apellido
    @NumeroIdentificacion NVARCHAR(20) = NULL -- Filtro por número de identificación
AS
BEGIN
    SET NOCOUNT ON;

    -- Validaciones
    IF @PageNumber <= 0 OR @PageSize <= 0
    BEGIN
        RAISERROR('Los valores de PageNumber y PageSize deben ser mayores a 0.', 16, 1);
        RETURN;
    END;

    IF @Nombre IS NOT NULL AND LEN(@Nombre) > 50
    BEGIN
        RAISERROR('El valor de @Nombre excede la longitud máxima permitida.', 16, 1);
        RETURN;
    END;

    IF @Apellido IS NOT NULL AND LEN(@Apellido) > 50
    BEGIN
        RAISERROR('El valor de @Apellido excede la longitud máxima permitida.', 16, 1);
        RETURN;
    END;

    IF @NumeroIdentificacion IS NOT NULL AND LEN(@NumeroIdentificacion) > 20
    BEGIN
        RAISERROR('El valor de @NumeroIdentificacion excede la longitud máxima permitida.', 16, 1);
        RETURN;
    END;

    -- Filtro para caracteres inválidos
    IF @Nombre LIKE '%[%]%' OR @Apellido LIKE '%[%]%' OR @NumeroIdentificacion LIKE '%[%]%'
    BEGIN
        RAISERROR('Los valores de búsqueda no deben contener caracteres inválidos.', 16, 1);
        RETURN;
    END;

    -- Declaración de variables para la paginación
    DECLARE @StartRow INT = (@PageNumber - 1) * @PageSize + 1;
    DECLARE @EndRow INT = @PageNumber * @PageSize;

    -- Common Table Expression (CTE) para la paginación
    WITH PersonasCTE AS (
        SELECT 
            ROW_NUMBER() OVER (ORDER BY FechaCreacion DESC) AS RowNum,
            Identificador,
            Nombres,
            Apellidos,
            NumeroIdentificacion,
            TipoIdentificacion,
            Email,
            FechaCreacion,
            NumeroIdentificacionCompleto,
            NombreCompleto
        FROM Personas
        WHERE 
            (@Nombre IS NULL OR Nombres LIKE '%' + @Nombre + '%') AND
            (@Apellido IS NULL OR Apellidos LIKE '%' + @Apellido + '%') AND
            (@NumeroIdentificacion IS NULL OR NumeroIdentificacion LIKE '%' + @NumeroIdentificacion + '%')
    )
    SELECT 
        Identificador,
        Nombres,
        Apellidos,
        NumeroIdentificacion,
        TipoIdentificacion,
        Email,
        FechaCreacion,
        NumeroIdentificacionCompleto,
        NombreCompleto
    FROM PersonasCTE
    WHERE RowNum BETWEEN @StartRow AND @EndRow;

    -- Total de registros para la paginación
    SELECT COUNT(*) AS TotalCount
    FROM Personas
    WHERE 
        (@Nombre IS NULL OR Nombres LIKE '%' + @Nombre + '%') AND
        (@Apellido IS NULL OR Apellidos LIKE '%' + @Apellido + '%') AND
        (@NumeroIdentificacion IS NULL OR NumeroIdentificacion LIKE '%' + @NumeroIdentificacion + '%');
END;
GO
