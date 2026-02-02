using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Infrastructure.Domain
{
    public class PageRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
