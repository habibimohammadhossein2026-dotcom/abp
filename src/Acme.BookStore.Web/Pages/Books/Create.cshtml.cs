using Acme.BookStore.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Acme.BookStore.Web.Pages.Books
{
    [RenderComponent("Author",null,null)]
    public class CreateModel : PageModel
    {
        private readonly BookAppService _bookAppService;

        public CreateModel(BookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        [BindProperty]
        public CreateBookDto Input { get; set; }

        // فیلدهای مستقل نویسنده
        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _bookAppService.CreateAsync(
                Input
            );

            return RedirectToPage("Index");
        }
    }
}
