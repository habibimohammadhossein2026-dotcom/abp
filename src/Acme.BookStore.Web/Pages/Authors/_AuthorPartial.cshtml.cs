using Acme.BookStore.Web.Helpers.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Acme.BookStore.Web.Pages.Authors
{
    [PartialFor(new[] { "CreateBook", "EditBook" }, "Author")]
    public class _AuthorPartialModel : PageModel
    {
    }

}
