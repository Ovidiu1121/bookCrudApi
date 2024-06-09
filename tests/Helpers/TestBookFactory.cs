using BookCrudApi.Books.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Helpers
{
    public class TestBookFactory
    {
        public static Book CreateBook(int id)
        {

            return new Book
            {
                Id = id,
                Title="The freak"+id,
                Author="Batman"+id,
                Genre="Turkish"+id
            };

        }

        public static List<Book> CreateBooks(int count)
        {

            List<Book> doctors = new List<Book>();

            for (int i = 0; i<count; i++)
            {
                doctors.Add(CreateBook(i));
            }
            return doctors;

        }

    }
}
