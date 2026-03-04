CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);

CREATE TABLE brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50)
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100),
    brand_id INT,
    category_id INT,
    model_year INT,
    list_price DECIMAL(10,2),
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);

INSERT INTO categories VALUES
(1, 'Electronics'),
(2, 'Sports'),
(3, 'Furniture');

INSERT INTO brands VALUES
(1, 'Nike'),
(2, 'Samsung'),
(3, 'Ikea');

INSERT INTO products VALUES
(101, 'Running Shoes', 1, 2, 2025, 600),
(102, 'Smart TV', 2, 1, 2024, 450),
(103, 'Sofa Set', 3, 3, 2023, 800),
(104, 'Basketball', 1, 2, 2025, 300),
(105, 'Laptop', 2, 1, 2025, 750);

SELECT 
    p.product_name,
    b.brand_name,
    c.category_name,
    p.model_year,
    p.list_price
FROM products p
INNER JOIN brands b
    ON p.brand_id = b.brand_id
INNER JOIN categories c
    ON p.category_id = c.category_id
WHERE p.list_price > 500
ORDER BY p.list_price ASC;

