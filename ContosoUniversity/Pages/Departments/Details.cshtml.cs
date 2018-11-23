namespace ContosoUniversity.Pages.Departments
{
    using System.Threading.Tasks;

    using ContosoUniversity.Models;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public DetailsModel(ContosoUniversity.Models.SchoolContext context)
        {
            this._context = context;
        }

        public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Department = await this._context.Departments
                .Include(d => d.Administrator).FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (this.Department == null)
            {
                return this.NotFound();
            }
            return this.Page();
        }
    }
}
