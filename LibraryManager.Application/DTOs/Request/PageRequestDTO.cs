using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.Request
{
    public class PageRequestDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }   
    }
}
