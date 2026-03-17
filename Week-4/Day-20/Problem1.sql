CREATE DATABASE BookMart;

USE BookMart;

CREATE TABLE Books (
    BookID  INT           IDENTITY(1,1) PRIMARY KEY,
    Title   NVARCHAR(150) NOT NULL,
    Stock   INT           NOT NULL CHECK (Stock >= 0),
    Price   DECIMAL(10,2) NOT NULL
);

CREATE TABLE Orders (
    OrderID   INT       IDENTITY(1,1) PRIMARY KEY,
    BookID    INT       NOT NULL,
    Quantity  INT       NOT NULL CHECK (Quantity > 0),
    OrderDate DATETIME2 DEFAULT SYSDATETIME(),
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);


--Task_1

CREATE OR ALTER PROCEDURE sp_AddNewBook
    @Title  NVARCHAR(150),
    @Stock  INT,
    @Price  DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;                        -- Suppress "rows affected" noise

    BEGIN TRY

        -- Validate stock before inserting
        IF @Stock < 0
            RAISERROR('Stock cannot be negative.', 16, 1);

        -- Validate price before inserting
        IF @Price <= 0
            RAISERROR('Price must be greater than zero.', 16, 1);

        INSERT INTO Books (Title, Stock, Price)
        VALUES (@Title, @Stock, @Price);

        PRINT '✔ Book added successfully: ' + @Title;

    END TRY
    BEGIN CATCH

        PRINT '✘ Error ' + CAST(ERROR_NUMBER() AS VARCHAR) +
              ': '        + ERROR_MESSAGE();

    END CATCH
END;

--Task_2

CREATE OR ALTER PROCEDURE sp_PlaceOrder
    @BookID   INT,
    @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Forces automatic rollback if session is killed mid-transaction
    SET XACT_ABORT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

            -- ── Stock & existence check ──────────────────────
            DECLARE @AvailableStock INT;

            SELECT @AvailableStock = Stock
            FROM   Books
            WHERE  BookID = @BookID;

            -- Book not found → @AvailableStock will be NULL
            IF @AvailableStock IS NULL OR @AvailableStock < @Quantity
                RAISERROR('Not enough stock or book not found.', 16, 1);

            -- ── Deduct stock ─────────────────────────────────
            UPDATE Books
            SET    Stock = Stock - @Quantity
            WHERE  BookID = @BookID;

            -- ── Record the order ─────────────────────────────
            INSERT INTO Orders (BookID, Quantity)
            VALUES (@BookID, @Quantity);

        COMMIT TRANSACTION;

        PRINT '✔ Order placed successfully.';

    END TRY
    BEGIN CATCH

        -- Roll back only if a transaction is still open
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        PRINT '✘ Error ' + CAST(ERROR_NUMBER() AS VARCHAR) +
              ': '        + ERROR_MESSAGE();

    END CATCH
END;


EXEC sp_AddNewBook 'Clean Code',                  50,  499.00;
EXEC sp_AddNewBook 'The Pragmatic Programmer',    30,  699.00;
EXEC sp_AddNewBook 'Introduction to Algorithms',  10, 1299.00;
EXEC sp_AddNewBook 'Design Patterns',             20,  899.00;
EXEC sp_AddNewBook 'You Don''t Know JS',           5,  349.00;

-- Verify inserted books
SELECT * FROM Books;
GO