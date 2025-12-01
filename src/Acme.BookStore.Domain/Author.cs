using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore
{
    public class Author : AggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public Guid BookId { get; set; }
    }
}
