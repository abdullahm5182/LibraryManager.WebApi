using Dapper;

using LibraryManager.Infrastructure.Domain;
using LibraryManager.Infrastructure.Interfaces; 

namespace LibraryManager.Infrastructure.Repository
{
    public class BooksRepository : IBookRepository
    {
        readonly IDbConnectionFactory dbConnectionFactory;
        public BooksRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<BookListResponse> GetBooksSortedByAuthor(PageRequest pageRequest)
        {
            using var connection = dbConnectionFactory.CreateConnection();
            BookListResponse response = new(); 
            connection.Open();
            DynamicParameters parameters = new();
            parameters.Add("@PageSize", pageRequest.PageSize);
            parameters.Add("@PageNumber", pageRequest.PageNumber);
            var result = await connection.QueryMultipleAsync("bsp_BookList_SortedByAuthor",
                parameters, commandType: System.Data.CommandType.StoredProcedure);
            response.List = result.Read<Book>().ToList();
            response.TotalCount = result.ReadFirst<int>();

            return response;

        }

        public async Task<BookListResponse> GetBooksSortedByPublisher(PageRequest pageRequest)
        {
            using var connection = dbConnectionFactory.CreateConnection();
            BookListResponse response = new BookListResponse(); 
            connection.Open();
            DynamicParameters parameters = new();
            parameters.Add("@PageSize", pageRequest.PageSize);
            parameters.Add("@PageNumber", pageRequest.PageNumber);
            var result = await connection.QueryMultipleAsync("bsp_Bookslist_SortedByPublisher",
                parameters, commandType: System.Data.CommandType.StoredProcedure);
            response.List = result.Read<Book>().ToList();
            response.TotalCount = result.ReadFirst<int>();

            return response;
        }

        public async Task<BooksTotalPrice> GetBooksTotalPrice()
        {
            using var connection = dbConnectionFactory.CreateConnection();
            connection.Open();

            return (await connection.QueryFirstOrDefaultAsync<BooksTotalPrice>(
                "bsp_Books_totalPrice",
                commandType: System.Data.CommandType.StoredProcedure))!;

        }

        public async Task<GenericInsertResponse> BooksBulkSave(BookBulkSave request)
        {
            using var connection = dbConnectionFactory.CreateConnection();
            connection.Open();
            DynamicParameters parameters = new();
            parameters.Add("@Books", request.Books.AsTableValuedParameter("udt_BookBulk_save"));
            parameters.Add("ReturnValue",
                dbType: System.Data.DbType.Int32,
                direction: System.Data.ParameterDirection.ReturnValue);

            await connection.ExecuteAsync("usp_BookBulkInsert",
                parameters, commandType: System.Data.CommandType.StoredProcedure);

            return new GenericInsertResponse()
            {
                ReturnValue = parameters.Get<int>("ReturnValue")
            };
        }
    }
}
