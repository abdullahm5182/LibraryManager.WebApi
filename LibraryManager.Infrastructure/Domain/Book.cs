using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? AuthorFirstName { get; set; }
        public string? AuthorLastName { get; set; }
        public string? Publisher { get; set; }
        public int YearPublished { get; set; }
        public string? ISBN { get; set; }
        public decimal Price { get; set; }
    }
    public class BookBulkSave
    {
        public DataTable Books { get; set; }

    }
    public class BooksTotalPrice
    {
        public decimal TotalPrice { get; set; }
    }

    public class BookListResponse
    {
        public List<Book> List { get; set; }
        public int TotalCount { get; set; }
    }
}
