using Acme.BookStore.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;

namespace Acme.BookStore.Web.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly BookAppService _bookAppService;
        private readonly IDistributedEventBus _eventBus;

        public EditModel(
            BookAppService bookAppService,
            IDistributedEventBus eventBus)
        {
            _bookAppService = bookAppService;
            _eventBus = eventBus;
        }

        // BookId از Route گرفته می‌شود
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        // DTO مخصوص کتاب (بدون Author)
        [BindProperty]
        public UpdateBookDto Input { get; set; }

        public async Task OnGetAsync()
        {
            var book = await _bookAppService.GetAsync(Id);

            Input = new UpdateBookDto
            {
                Title = book.Title
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _bookAppService.UpdateAsync(Id, Input);

            await _eventBus.PublishAsync(new BookUpdatedEvent
            {
                BookId = Id
            });

            return RedirectToPage("Index");
        }
    }
}
