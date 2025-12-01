using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Web.Pages.Shared.Components.AuthorName
{
    public class AuthorNameViewComponent : ViewComponent, ITransientDependency
    {
        private readonly IRepository<Author, Guid> _authorRepo;

        public AuthorNameViewComponent(IRepository<Author, Guid> repo)
        {
            _authorRepo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid bookId)
        {
            var author = await _authorRepo.FirstOrDefaultAsync(x => x.BookId == bookId);

            var display = author == null
                ? "-"
                : $"{author.Name} {author.Family}";

            return Content(display);
        }
    }
}
