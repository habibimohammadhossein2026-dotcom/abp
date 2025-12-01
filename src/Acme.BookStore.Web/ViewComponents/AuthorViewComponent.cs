using Acme.BookStore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

public class AuthorViewComponent : ViewComponent, ITransientDependency
{
    private readonly IRepository<Author, Guid> _authorRepository;

    public AuthorViewComponent(IRepository<Author, Guid> repo)
    {
        _authorRepository = repo;
    }

    public async Task<IViewComponentResult> InvokeAsync(Guid? bookId)
    {
        Author model = null;

        if (bookId.HasValue)
        {
            model = await _authorRepository.FirstOrDefaultAsync(x => x.BookId == bookId.Value);
        }

        return View("~/Pages/Authors/_AuthorPartial.cshtml", model);
    }
}
