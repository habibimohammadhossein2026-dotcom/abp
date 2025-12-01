using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Books
{
    public class BookUpdatedEvent
    {
        public Guid BookId { get; set; }
    }

}
