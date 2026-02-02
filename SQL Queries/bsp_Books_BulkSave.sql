CREATE PROCEDURE usp_BookBulkInsert
    @Books udt_BookBulk_save READONLY
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO tbl_Books
            (Title, AuthorFirstName, AuthorLastName, Publisher, YearPublished, ISBN, Price)
        SELECT 
            Title,
            AuthorFirstName,
            AuthorLastName,
            Publisher,
            YearPublished,
            ISBN,
            Price
        FROM @Books;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        RETURN 1;
    END CATCH
END;
 
