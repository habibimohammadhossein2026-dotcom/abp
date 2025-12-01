using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Books
{
    public class CreateBookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
