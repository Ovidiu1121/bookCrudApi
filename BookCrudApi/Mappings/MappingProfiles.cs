using AutoMapper;
using BookCrudApi.Books.Model;
using BookCrudApi.Dto;

namespace BookCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<CreateBookRequest, Book>();
            CreateMap<UpdateBookRequest, Book>();
            CreateMap<BookDto, Book>().ReverseMap();
        }

    }
}
