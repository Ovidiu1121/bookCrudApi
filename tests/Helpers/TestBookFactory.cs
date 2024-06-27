using BookCrudApi.Books.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookCrudApi.Dto;

namespace tests.Helpers
{
    public class TestBookFactory
    {
        public static BookDto CreateBook(int id)
        {

            return new BookDto
            {
                Id = id,
                Title="The freak"+id,
                Author="Batman"+id,
                Genre="Turkish"+id
            };

        }

        public static ListBookDto CreateBooks(int count)
        {

            ListBookDto doctors = new ListBookDto();

            for (int i = 0; i<count; i++)
            {
                doctors.bookList.Add(CreateBook(i));
            }
            return doctors;

        }

    }
}
