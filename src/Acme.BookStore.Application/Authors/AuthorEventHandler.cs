using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace Acme.BookStore.Books
{
    public class AuthorEventHandler :
        IDistributedEventHandler<BookCreatedEvent>,
        IDistributedEventHandler<BookUpdatedEvent>,
        IDistributedEventHandler<BookDeletedEvent>,
        ITransientDependency
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository<Author, Guid> _authorRepository;

        public AuthorEventHandler(
            IHttpContextAccessor contextAccessor,
            IRepository<Author, Guid> authorRepository)
        {
            _contextAccessor = contextAccessor;
            _authorRepository = authorRepository;
        }

        public async Task HandleEventAsync(BookCreatedEvent eventData)
        {
            var form = _contextAccessor.HttpContext?.Request?.Form;
            if (form == null)
                return;

            string name = form["Author.Name"];
            string family = form["Author.Family"];

            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(family))
                return;

            await _authorRepository.InsertAsync(new Author
            {
                BookId = eventData.BookId,
                Name = name,
                Family = family
            }, autoSave: true);
        }

        public async Task HandleEventAsync(BookUpdatedEvent eventData)
        {
            var form = _contextAccessor.HttpContext?.Request?.Form;
            if (form == null)
                return;

            string name = form["Author.Name"];
            string family = form["Author.Family"];

            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(family))
                return;

            var author = await _authorRepository.FirstOrDefaultAsync(x => x.BookId == eventData.BookId);

            if (author == null)
            {
                author = new Author
                {
                    BookId = eventData.BookId,
                    Name = name,
                    Family = family
                };

                await _authorRepository.InsertAsync(author, autoSave: true);
            }
            else
            {
                author.Name = name;
                author.Family = family;

                await _authorRepository.UpdateAsync(author, autoSave: true);
            }
        }

        public async Task HandleEventAsync(BookDeletedEvent eventData)
        {
            var author = await _authorRepository.FirstOrDefaultAsync(x => x.BookId == eventData.BookId);
            await _authorRepository.DeleteAsync(author, autoSave: true);

        }
    }
}
