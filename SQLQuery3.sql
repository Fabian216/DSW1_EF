USE BDVENTAS2024API
GO

CREATE PROCEDURE sp_ListarArticulosActivos
AS
BEGIN
    -- Seleccionar los art�culos que no est�n en tb_articulos_baja
    SELECT 
        a.cod_art,
        a.nom_art,
        a.uni_med,
        a.pre_art,
        a.stk_art,
        a.dado_de_baja
    FROM 
        tb_articulos a
    WHERE 
        a.cod_art NOT IN (
            SELECT b.cod_art
            FROM tb_articulos_baja b
        )
END;
GO

EXEC sp_ListarArticulosActivos;


CREATE PROCEDURE sp_FiltrarArticuloActivoPorID
    @CodigoArticulo CHAR(5) 
AS
BEGIN
    -- Seleccionar el art�culo que no est� en tb_articulos_baja y que coincide con el ID proporcionado
    SELECT 
        cod_art,
        nom_art,
        uni_med,
        pre_art,
        stk_art,
        dado_de_baja
    FROM 
        tb_articulos
    WHERE 
        cod_art = @CodigoArticulo
        AND cod_art NOT IN (
            SELECT cod_art
            FROM tb_articulos_baja
        )
END;
GO

EXEC sp_FiltrarArticuloActivoPorID @CodigoArticulo = 'A0001';



CREATE PROCEDURE sp_FiltrarArticulosActivosPorIniciales
    @Iniciales NVARCHAR(50) -- Par�metro para las iniciales
AS
BEGIN
    -- Seleccionar los art�culos que no est�n en tb_articulos_baja y que coinciden con las iniciales
    SELECT 
        a.cod_art,
        a.nom_art,
        a.uni_med,
        a.pre_art,
        a.stk_art,
        a.dado_de_baja
    FROM 
        tb_articulos a
    WHERE 
        a.cod_art NOT IN (
            SELECT b.cod_art
            FROM tb_articulos_baja b
        )
        AND a.nom_art LIKE @Iniciales + '%' -- Filtrar por iniciales    
END;
GO

EXEC sp_FiltrarArticulosActivosPorIniciales @Iniciales = 'PENTIUM';



EXEC sp_FiltrarArticulosActivosPorCodigo @Codigo = 'A0003';


CREATE PROCEDURE sp_DarDeBajaArticulo
    @Codigo CHAR(5) -- C�digo del art�culo a dar de baja
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION; -- Iniciar una transacci�n

        -- Verificar si el art�culo existe y a�n no ha sido dado de baja
        IF EXISTS (
            SELECT 1 
            FROM tb_articulos 
            WHERE cod_art = @Codigo AND dado_de_baja = 'No'
        )
        BEGIN
            -- Insertar registro en tb_articulos_baja
            INSERT INTO tb_articulos_baja (cod_art, fecha_baja)
            VALUES (@Codigo, GETDATE());

            -- Verificar si el art�culo tiene stock
            DECLARE @Stock INT, @Precio DECIMAL(7,2);
            SELECT @Stock = stk_art, @Precio = pre_art
            FROM tb_articulos
            WHERE cod_art = @Codigo;

            IF @Stock > 0
            BEGIN
                -- Insertar registro en tb_articulos_liquidacion
                INSERT INTO tb_articulos_liquidacion (cod_art, unidades_liquidar, precio_liquidar)
                VALUES (@Codigo, @Stock, @Precio);
            END

            -- Actualizar el estado del art�culo a "Si" (dado de baja)
            UPDATE tb_articulos
            SET dado_de_baja = 'Si'
            WHERE cod_art = @Codigo;

            -- Confirmar transacci�n
            COMMIT TRANSACTION;
            PRINT 'Art�culo dado de baja exitosamente.';
        END
        ELSE
        BEGIN
            -- Si el art�culo no existe o ya est� dado de baja
            RAISERROR('El art�culo no existe o ya est� dado de baja.', 16, 1);
        END
    END TRY
    BEGIN CATCH
        -- Manejo de errores y reversi�n de la transacci�n
        ROLLBACK TRANSACTION;
        PRINT ERROR_MESSAGE();
    END CATCH
END;
GO



--DAR DE BAJA AL CODIGO :
EXEC sp_DarDeBajaArticulo @Codigo = 'A0034';

DROP PROCEDURE sp_FiltrarArticulosActivosPorCodigo;

select * from tb_articulos
go

select * from tb_articulos_baja
go

select *from tb_articulos_liquidacion
go
