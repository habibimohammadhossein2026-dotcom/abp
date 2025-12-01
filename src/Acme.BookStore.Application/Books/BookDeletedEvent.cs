using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Books
{
    public class BookDeletedEvent
    {
        public Guid BookId { get; set; }
    }

}
