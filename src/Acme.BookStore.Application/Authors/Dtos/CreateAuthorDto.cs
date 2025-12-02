using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Authors
{
    public class CreateAuthorDto
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public Guid BookId { get; set; }
    }

}
