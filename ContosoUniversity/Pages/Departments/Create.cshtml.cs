namespace ContosoUniversity.Pages.Departments
{
    using System.Threading.Tasks;

    using ContosoUniversity.Models;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public CreateModel(ContosoUniversity.Models.SchoolContext context)
        {
            this._context = context;
        }

        public IActionResult OnGet()
        {
        this.ViewData["InstructorID"] = new SelectList(this._context.Instructors, "ID", "FirstMidName");
            return this.Page();
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this._context.Departments.Add(this.Department);
            await this._context.SaveChangesAsync();

            return this.RedirectToPage("./Index");
        }
    }
}