using LibraryManager.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        Task<BookListResponse> GetBooksSortedByAuthor(PageRequest pageRequest);
        Task<BookListResponse> GetBooksSortedByPublisher(PageRequest pageRequest);
        Task<BooksTotalPrice> GetBooksTotalPrice();
        Task<GenericInsertResponse> BooksBulkSave(BookBulkSave request);
    }
}
