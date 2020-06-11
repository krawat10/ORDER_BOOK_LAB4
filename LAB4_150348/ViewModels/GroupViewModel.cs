using System.Collections.Generic;
using LAB4_150348.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB4_150348.ViewModels
{
    public class GroupViewModel
    {
        public int BooksAmount { get; set; }
        public decimal AveragePrice { get; set; }
        public IEnumerable<string> UniqueAuthors { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}