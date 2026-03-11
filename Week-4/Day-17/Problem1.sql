use day17;
-- Products table
CREATE TABLE Products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    product_name VARCHAR(100),
    stock_quantity INT NOT NULL CHECK (stock_quantity >= 0)
);

CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT NOT NULL,
    order_date DATETIME NOT NULL,
    status INT NOT NULL
);
CREATE TABLE Order_Items (
    order_item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),
    FOREIGN KEY (product_id) REFERENCES Products(product_id)
);
INSERT INTO Products
VALUES ('Car Battery', 10),
       ('Brake Pads', 20),
       ('Headlights', 15);

--Level-2 Problem 1: Transactions and Trigger Implementation

BEGIN TRANSACTION;
DECLARE @OrderID INT;
DECLARE @ProductID INT = 1;
DECLARE @Quantity INT = 3;
INSERT INTO Orders (customer_id, order_date, status)
VALUES (101, GETDATE(), 1);
SET @OrderID = SCOPE_IDENTITY();
IF EXISTS (
    SELECT 1 FROM Products WHERE product_id = @ProductID AND stock_quantity >= @Quantity
)
BEGIN
    INSERT INTO Order_Items (order_id, product_id, quantity)
    VALUES (@OrderID, @ProductID, @Quantity);
    COMMIT TRANSACTION;
    PRINT 'Order placed successfully.';
END
ELSE
BEGIN
    ROLLBACK TRANSACTION;
    PRINT 'Insufficient stock. Transaction rolled back.';
END


CREATE TRIGGER trg_reduce_stock
ON Order_Items
AFTER INSERT
AS
BEGIN
    UPDATE p
    SET p.stock_quantity = p.stock_quantity - i.quantity
    FROM Products p
    INNER JOIN inserted i ON p.product_id = i.product_id;
    IF EXISTS (
        SELECT 1 FROM Products p
        INNER JOIN inserted i ON p.product_id = i.product_id
        WHERE p.stock_quantity < 0
    )
    BEGIN
        RAISERROR ('Stock cannot be negative. Rolling back.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;

SELECT * FROM Orders ;
SELECT * FROM Order_Items ;
SELECT * FROM Products WHERE product_id = 1;

--Level-2 Problem 2: Atomic Order Cancellation with SAVEPOINT

BEGIN TRANSACTION;
BEGIN TRY
    DECLARE @OrderID INT = 2; 
    SAVE TRANSACTION CancelPoint;
    UPDATE p
    SET p.stock_quantity = p.stock_quantity + oi.quantity
    FROM Products p
    INNER JOIN Order_Items oi ON p.product_id = oi.product_id
    WHERE oi.order_id = @OrderID;
    UPDATE Orders
    SET status = 3
    WHERE order_id = @OrderID;
    COMMIT TRANSACTION;
    PRINT 'Order cancelled successfully. Stock restored and status updated.';
END TRY
BEGIN CATCH
    PRINT 'Error occurred during cancellation. Rolling back to SAVEPOINT.';
    ROLLBACK TRANSACTION CancelPoint;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    PRINT @ErrorMessage;
    ROLLBACK TRANSACTION;
END CATCH;