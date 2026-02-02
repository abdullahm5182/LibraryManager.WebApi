using LibraryManager.Application.DTOs.Request;
using LibraryManager.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookListResponseDTO> GetBooksSortedByAuthor(PageRequestDTO pageRequest);
        Task<BookListResponseDTO> GetBooksSortedByPublisher(PageRequestDTO pageRequest);
        Task<BooksTotalPriceResponseDTO> GetBooksTotalPrice();
        Task<GenericInsertResponseDTO> BooksBulkSave(BookBulkSaveRequestDTO request);

    }
}
