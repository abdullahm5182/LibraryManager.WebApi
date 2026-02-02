using System.Data;

using MapsterMapper;

using LibraryManager.Application.DTOs.Request;
using LibraryManager.Application.DTOs.Response;
using LibraryManager.Application.Interfaces;
using LibraryManager.Infrastructure.Domain;
using LibraryManager.Infrastructure.Interfaces;



namespace LibraryManager.Application.Services;

public class BookService : IBookService
{
    readonly IBookRepository bookRepository;
    readonly IMapper mapper;
    public BookService(IBookRepository bookRepository, IMapper mapper)
    {
        this.bookRepository= bookRepository;
        this.mapper= mapper;
    }

    public async Task<GenericInsertResponseDTO> BooksBulkSave(BookBulkSaveRequestDTO request)
    {
        DataTable books = new();
        books.Columns.Add("Title", typeof(string));
        books.Columns.Add("AuthorFirstName", typeof(string));
        books.Columns.Add("AuthorLastName", typeof(string));
        books.Columns.Add("Publisher", typeof(string));
        books.Columns.Add("YearPublished", typeof(int));
        books.Columns.Add("ISBN", typeof(string));
        books.Columns.Add("Price", typeof(decimal));

        foreach (var book in request.Books)
        {
            // Basic validation: skip invalid rows or throw exception
            if (string.IsNullOrWhiteSpace(book.Title) ||
                string.IsNullOrWhiteSpace(book.AuthorFirstName) ||
                string.IsNullOrWhiteSpace(book.AuthorLastName) ||
                string.IsNullOrWhiteSpace(book.Publisher) ||
                string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new ArgumentException("One or more required fields are missing.");
            }

            if (book.Price < 1.00m)
            {
                throw new ArgumentException("Price must be at least 1.00.");
            }

            var row = books.NewRow();
            row["Title"] = book.Title.Trim();
            row["AuthorFirstName"] = book.AuthorFirstName.Trim();
            row["AuthorLastName"] = book.AuthorLastName.Trim();
            row["Publisher"] = book.Publisher.Trim();
            row["YearPublished"] = book.YearPublished;
            row["ISBN"] = book.ISBN.Trim();
            row["Price"] = book.Price;

            books.Rows.Add(row);
        }

        var response = await bookRepository.BooksBulkSave(new BookBulkSave()
        {
            Books = books
        });

        var dto = mapper.Map<GenericInsertResponseDTO>(response);

        return dto;
    }


    public async Task<BookListResponseDTO> GetBooksSortedByAuthor(PageRequestDTO pageRequest)
    {
        var req = mapper.Map<PageRequest>(pageRequest);
        var res = await bookRepository.GetBooksSortedByAuthor(req);
        var dto = mapper.Map<BookListResponseDTO>(res);

        return dto;
    }

    public async Task<BookListResponseDTO> GetBooksSortedByPublisher(PageRequestDTO pageRequest)
    {
        var req = mapper.Map<PageRequest>(pageRequest);
        var res = await bookRepository.GetBooksSortedByPublisher(req);
        var dto = mapper.Map<BookListResponseDTO>(res);

        return dto;
    }

    public async Task<BooksTotalPriceResponseDTO> GetBooksTotalPrice()
    {
        var res = await bookRepository.GetBooksTotalPrice();
        var dto = mapper.Map<BooksTotalPriceResponseDTO>(res);

        return dto;
    }
}

