using LibraryManager.Application.DTOs.Request;
using LibraryManager.Application.DTOs.Response;
using LibraryManager.Infrastructure.Domain;
using Mapster;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Mapper
{
    public class BooksMapper : IRegister
    { 
        public void Register(TypeAdapterConfig typeConfig)
        { 
            typeConfig.NewConfig<Book, BookResponseDTO>()
         .Map(dest => dest.MlaCitation, src => $"{src.AuthorLastName}, {src.AuthorFirstName}. {src.Title}. {src.Publisher}, {src.YearPublished}.")
         .Map(dest => dest.ChicagoCitation, src => $"{src.AuthorLastName}, {src.AuthorFirstName}. {src.Title}. {src.Publisher}, {src.YearPublished}.");


            typeConfig.NewConfig<BooksTotalPrice, BooksTotalPriceResponseDTO>();
            typeConfig.NewConfig<BookListResponse, BookListResponseDTO>();

        }
    }
}
