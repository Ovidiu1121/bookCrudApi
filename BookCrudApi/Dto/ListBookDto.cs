namespace BookCrudApi.Dto;

public class ListBookDto
{
    public ListBookDto()
    {
        bookList = new List<BookDto>();
    }
    
    public List<BookDto> bookList { get; set; }
}