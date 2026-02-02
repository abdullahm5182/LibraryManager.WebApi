namespace LibraryManager.Application.DTOs.Response
{
    public class BookResponseDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? AuthorFirstName { get; set; }
        public string? AuthorLastName { get; set; }
        public string? Publisher { get; set; }
        public int YearPublished { get; set; }
        public string? ISBN { get; set; }
        public decimal Price { get; set; }
        public string? MlaCitation { get; set; } 
        public string? ChicagoCitation { get; set; } 
    }
    public class BookListResponseDTO
    {
        public List<BookResponseDTO> List { get; set; } = new();
        public int TotalCount { get; set; }
    }
    public class BooksTotalPriceResponseDTO
    {
        public decimal TotalPrice { get; set; }
    }   

}
