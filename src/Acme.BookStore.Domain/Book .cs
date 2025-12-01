using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Books
{
    public class Book : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }

        public Book() { }

        public Book(Guid id, string title)
            : base(id)
        {
            Title = title;
        }
    }
}
