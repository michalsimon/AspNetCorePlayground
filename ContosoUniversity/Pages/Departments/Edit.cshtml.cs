namespace ContosoUniversity.Pages.Departments
{
    using System.Linq;
    using System.Threading.Tasks;

    using ContosoUniversity.Models;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class EditModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public EditModel(ContosoUniversity.Models.SchoolContext context)
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
           this.ViewData["InstructorID"] = new SelectList(this._context.Instructors, "ID", "FirstMidName");
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this._context.Attach(this.Department).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.DepartmentExists(this.Department.DepartmentID))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.RedirectToPage("./Index");
        }

        private bool DepartmentExists(int id)
        {
            return this._context.Departments.Any(e => e.DepartmentID == id);
        }
    }
}
