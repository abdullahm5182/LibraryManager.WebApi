CREATE PROCEDURE bsp_BookList_SortedByAuthor
@PageSize INT NULL,
@PageNumber INT NULL
AS
BEGIN
	SET NOCOUNT ON;

    SET @PageSize = ISNULL(@PageSize,100)   
    SET @PageNumber = ISNULL(@PageNumber,1)

    SELECT * FROM tbl_Books
    ORDER BY AuthorLastName, AuthorFirstName, Title
    OFFSET (@PageNumber - 1) ROWS FETCH NEXT (@PageSize) ROWS ONLY;   
 
 	SELECT COUNT(Id) AS TotalCount FROM tbl_Books

END;  