namespace ContosoUniversity.Pages.Departments
{
    using System.Threading.Tasks;

    using ContosoUniversity.Models;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class DeleteModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public DeleteModel(ContosoUniversity.Models.SchoolContext context)
        {
            this._context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            this.Department = await this._context.Departments.FindAsync(id);

            if (this.Department != null)
            {
                this._context.Departments.Remove(this.Department);
                await this._context.SaveChangesAsync();
            }

            return this.RedirectToPage("./Index");
        }
    }
}
