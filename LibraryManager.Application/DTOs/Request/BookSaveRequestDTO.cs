using System.ComponentModel.DataAnnotations; 

namespace LibraryManager.Application.DTOs.Request
{
    public class BookSaveRequestDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 255 characters.")] 
        public string? Title { get; set; }
        [Required(ErrorMessage = "Author First Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Author First Name must be between 2 and 100 characters.")] 
        public string? AuthorFirstName { get; set; }
        [Required(ErrorMessage = "Author Last Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Author Last Name must be between 2 and 100 characters.")] 
        public string? AuthorLastName { get; set; }
        [Required(ErrorMessage = "Publisher is required.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Publisher must be between 2 and 255 characters.")]
        public string? Publisher { get; set; }
        [Required(ErrorMessage = "Year Published is required.")]
        public int YearPublished { get; set; }
        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 20 characters.")] 
        public string? ISBN { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(1.00, double.MaxValue, ErrorMessage = "Price must be at least 1.00.")] 
        public decimal Price { get; set; }
    }
    public class BookBulkSaveRequestDTO
    {
        public List<BookSaveRequestDTO> Books { get; set; } = new();
    }
}
