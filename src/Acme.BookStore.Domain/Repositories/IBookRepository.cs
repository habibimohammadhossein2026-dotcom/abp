using Acme.BookStore.Books;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Repositories
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        Task<List<Book>> GetBooksBeforeYearAsync(int year);
    }
}
