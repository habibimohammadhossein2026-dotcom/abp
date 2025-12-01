using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Books
{
    public class BookCreatedEvent
    {
        public Guid BookId { get; set; }
        public string TempAuthorName { get; set; }
        public string TempAuthorFamily { get; set; }
    }

}
