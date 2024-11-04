ALTER PROCEDURE SP_GET_ALL_PRODUCTS
@Id NVARCHAR(max) = NULL,
@ProductName NVARCHAR(max) = NULL,
@ProductDescription NVARCHAR(max) = NULL,
@ProductColor NVARCHAR(max) = NULL,
@ProductSize NVARCHAR(max) = NULL,
@ProductPrice MONEY = NULL,
@ProductSalePrice MONEY = NULL

AS 
BEGIN
    SELECT 
        Products.Id, 
        Products.ProductName, 
        Products.ProductDescription, 
        Products.ProductCategoryId, 
        ProductColors.ColorName AS ProductColor, 
        ProductSizes.ProductSizeName AS ProductSize, 
        Products.ProductPrice, 
        Products.ProductSalePrice
    FROM Products
        INNER JOIN ProductColors ON Products.ProductColorId = ProductColors.Id
        INNER JOIN ProductSizes ON Products.ProductSizeId = ProductSizes.Id
    WHERE 
        (@Id IS NULL OR Products.Id LIKE '%' + @Id + '%') AND
        (@ProductName IS NULL OR Products.ProductName LIKE '%' + @ProductName + '%') AND
        (@ProductDescription IS NULL OR Products.ProductDescription LIKE '%' + @ProductDescription + '%') AND
        (@ProductColor IS NULL OR ProductColors.ColorName LIKE '%' + @ProductColor + '%') AND
        (@ProductSize IS NULL OR ProductSizes.ProductSizeName LIKE '%' + @ProductSize + '%') AND
        (@ProductPrice IS NULL OR Products.ProductPrice = @ProductPrice) AND
        (@ProductSalePrice IS NULL OR Products.ProductSalePrice = @ProductSalePrice);
END