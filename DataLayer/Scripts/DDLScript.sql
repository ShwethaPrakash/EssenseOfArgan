DROP TABLE IF EXISTS TestOrders
DROP TABLE IF EXISTS TestProducts
DROP TABLE IF EXISTS TestCategories
DROP TABLE IF EXISTS TestProductCategories
DROP TABLE IF EXISTS TestOrderProducts

DECLARE @OrderId int
CREATE TABLE TestOrders(id int IDENTITY, FirstName varchar(255) , LastName varchar(255), Address varchar(255), City varchar(255), State varchar(255), Country varchar(255))
CREATE TABLE TestProducts (id int IDENTITY, Name varchar(255), SKU varchar(255), Description varchar(255))
CREATE TABLE TestCategories (id int IDENTITY, Name varchar(255))
CREATE TABLE TestProductCategories(id int IDENTITY, ProductId int, CategoryId int)
CREATE TABLE TestOrderProducts(id int IDENTITY, OrderId int, ProductId int, Quantity int, Price money, Total money)

INSERT INTO TestOrders(FirstName, LastName, Address, City, State, Country)
SELECT 'Jeff', 'Cheung', '150 Golf Course Rd', 'South Burlington', 'VT', 'USA'
SET @OrderId = @@IDENTITY

INSERT INTO TestOrderProducts(OrderId, ProductId, Quantity, Price, Total)
VALUES(@OrderId, 1, 1, 9.99, 9.99)

INSERT INTO TestOrderProducts(OrderId, ProductId, Quantity, Price, Total)
VALUES(@OrderId, 3, 1, 4.99, 9.99)

INSERT INTO TestOrders(FirstName, LastName, Address, City, State, Country)
SELECT 'Jeff', 'Cheung', '150 Golf Course Rd', 'South Burlington', 'VT', 'USA'

SET @OrderId = @@IDENTITY

INSERT INTO TestOrderProducts(OrderId, ProductId, Quantity, Price, Total)
VALUES(@OrderId, 2, 1, 9.99, 9.99)

INSERT INTO TestOrders(FirstName, LastName, Address, City, State, Country)
SELECT 'Jeff', 'Cheung', '150 Dorset St Ste 245-236', 'South Burlington', 'VT', 'USA'
SET @OrderId = @@IDENTITY

INSERT INTO TestOrderProducts(OrderId, ProductId, Quantity, Price, Total)
VALUES(@OrderId, 1, 1, 9.99, 9.99)

INSERT INTO TestOrderProducts(OrderId, ProductId, Quantity, Price, Total)
VALUES(@OrderId, 3, 1, 4.99, 9.99)

INSERT INTO TestOrderProducts(OrderId, ProductId, Quantity, Price, Total)
VALUES(@OrderId, 2, 1, 9.99, 9.99)

INSERT INTO TestCategories(Name)
SELECT ('Category 1')
UNION
SELECT ('Category 2')

INSERT INTO TestProducts(Name, SKU, Description)
SELECT 'Argan Product', 'Argan', 'Argan Test Product'
UNION
SELECT 'Argan Product 2', 'Argan2', 'Argan Test Product #2'
UNION
SELECT 'Idro Product', 'Idro', 'Idro Test Product'

INSERT INTO TestProductCategories(ProductId, CategoryId)
SELECT 1, 1
UNION
SELECT 2, 1
UNION
SELECT 3, 2

/****** Object:  StoredProcedure [dbo].[GetOrders]    Script Date: 6/9/2020 10:44:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetOrders]
AS
BEGIN
select orders.id, FirstName, LastName, Address, City, State,Country, Price, tp.Name as ProductName, ordersProds.quantity, tp.SKU as ProductSKU, tc.Name as Category  from 
TestOrders orders join 
TestOrderProducts ordersProds on orders.id = ordersProds.OrderId join
TestProducts tp on ordersProds.ProductId = tp.id join
TestProductcategories tpc on tpc.ProductId = tp.id  join
TestCategories tc on tc.id = tpc.CategoryId
END
GO

