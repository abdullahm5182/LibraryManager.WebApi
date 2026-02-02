CREATE PROCEDURE bsp_Books_totalPrice
AS
BEGIN
    SET NOCOUNT ON;

    SELECT SUM(Price) AS TotalPrice FROM tbl_Books 
END