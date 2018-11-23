namespace ContosoUniversity.Pages.Departments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ContosoUniversity.Models;

    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public IndexModel(ContosoUniversity.Models.SchoolContext context)
        {
            this._context = context;
        }

        public IList<Department> Department { get;set; }

        public async Task OnGetAsync()
        {
            this.Department = await this._context.Departments
                .Include(d => d.Administrator).ToListAsync();
        }
    }
}
