using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;

namespace Acme.BookStore.Books
{
    public class BookAppService : ApplicationService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IDistributedEventBus _eventBus;

        public BookAppService(
            IRepository<Book, Guid> bookRepository,
            IDistributedEventBus eventBus)
        {
            _bookRepository = bookRepository;
            _eventBus = eventBus;
        }

        public async Task<Guid> CreateAsync(CreateBookDto input)
        {
            var book = new Book { Title = input.Title };

            await _bookRepository.InsertAsync(book, autoSave: true);

            await _eventBus.PublishAsync(new BookCreatedEvent
            {
                BookId = book.Id
            });

            return book.Id;
        }

        public async Task<BookDto> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title
            };
        }
        public async Task UpdateAsync(Guid id, UpdateBookDto input)
        {
            var book = await _bookRepository.GetAsync(id);

            book.Title = input.Title;

            await _bookRepository.UpdateAsync(book, autoSave: true);

            await _eventBus.PublishAsync(new BookUpdatedEvent
            {
                BookId = id
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);

            await _eventBus.PublishAsync(new BookDeletedEvent
            {
                BookId = id
            });
        }

        public async Task<List<BookDto>> GetListAsync()
        {
            var items = await _bookRepository.GetListAsync();

            return items
                .Select(x => new BookDto
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .ToList();
        }
    }
}
