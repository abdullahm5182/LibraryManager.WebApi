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
    public class PageMapper : IRegister
    {
        public void Register(TypeAdapterConfig typeConfig)
        {
            typeConfig.NewConfig<PageRequestDTO, PageRequest>()
                .Map(dest => dest.PageNumber, src => src.PageNumber ==0 ? 1 : src.PageNumber)
                .Map(dest => dest.PageSize, src => src.PageSize ==0 ? 100 : src.PageSize);

            typeConfig.NewConfig<GenericInsertResponse, GenericInsertResponseDTO>();


        }
    }
}
