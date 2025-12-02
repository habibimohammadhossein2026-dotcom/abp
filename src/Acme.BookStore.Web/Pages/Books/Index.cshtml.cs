using Acme.BookStore.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;

namespace Acme.BookStore.Web.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookAppService _bookAppService;
        private readonly IDistributedEventBus _eventBus;

        public IndexModel(BookAppService bookAppService, IDistributedEventBus eventBus)
        {
            _bookAppService = bookAppService;
            _eventBus = eventBus;
        }

        public List<BookDto> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _bookAppService.GetListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _bookAppService.DeleteAsync(id);

            return RedirectToPage();
        }
    }
}
