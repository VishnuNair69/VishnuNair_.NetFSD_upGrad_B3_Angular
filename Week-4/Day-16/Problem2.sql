CREATE TABLE stocks(
    product_id INT PRIMARY KEY,
    quantity INT,
    FOREIGN KEY(product_id) REFERENCES products(product_id)
);

INSERT INTO stocks VALUES
(101,10),
(102,20),
(103,50);





--Level-2 Problem 2: Stock Auto-Update Trigger
ALTER TRIGGER trg_UpdateStock
ON order_items
AFTER INSERT
AS
BEGIN
BEGIN TRY
IF EXISTS(
SELECT 1
FROM inserted i
JOIN stocks s
ON i.product_id = s.product_id
WHERE s.quantity < i.quantity
)
BEGIN
RAISERROR('Insufficient Stock',16,1)
ROLLBACK TRANSACTION
RETURN
END
UPDATE s
SET s.quantity = s.quantity - i.quantity
FROM stocks s
JOIN inserted i
ON s.product_id = i.product_id
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION
THROW
END CATCH
END;

INSERT INTO order_items VALUES (4,1,101,5,50000,0);

---Level-2 Problem 3: Order Status Validation Trigger
ALTER TRIGGER trg_OrderStatusValidation
ON orders
AFTER UPDATE
AS
BEGIN
BEGIN TRY

IF EXISTS(
    SELECT 1
    FROM inserted
    WHERE order_status = 4
    AND shipped_date IS NULL
)
BEGIN
    RAISERROR('Shipped date cannot be NULL when order is completed',16,1);
    RETURN;
END
END TRY
BEGIN CATCH
    THROW;
END CATCH
END;

UPDATE orders
SET order_status = 4, shipped_date = NULL
WHERE order_id = 1;


---Level-2 Problem 3: Cursor-Based Revenue Calculation
CREATE PROCEDURE usp_CursorRevenueCalculation
AS
BEGIN
BEGIN TRY
    BEGIN TRANSACTION
    DECLARE @order_id INT
    DECLARE @store_id INT
    DECLARE @revenue DECIMAL(10,2)
    -- Temporary table to store results
    CREATE TABLE #TempRevenue
    (
        store_id INT,
        order_id INT,
        revenue DECIMAL(10,2)
    )
    -- Cursor to fetch completed orders
    DECLARE order_cursor CURSOR FOR
    SELECT order_id, store_id
    FROM orders
    WHERE order_status = 4
    OPEN order_cursor
    FETCH NEXT FROM order_cursor INTO @order_id, @store_id
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Calculate revenue per order
        SELECT @revenue =
        SUM(quantity * list_price * (1 - discount))
        FROM order_items
        WHERE order_id = @order_id
        -- Store result
        INSERT INTO #TempRevenue
        VALUES(@store_id, @order_id, @revenue)
        FETCH NEXT FROM order_cursor INTO @order_id, @store_id
    END
    CLOSE order_cursor
    DEALLOCATE order_cursor
    -- Store wise revenue summary
    SELECT 
        store_id,
        SUM(revenue) AS total_revenue
    FROM #TempRevenue
    GROUP BY store_id
    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION
    PRINT ERROR_MESSAGE()
END CATCH
END;