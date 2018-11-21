namespace ContosoUniversity.Pages.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ContosoUniversity.Models;

    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get; set; }

        public async Task OnGetAsync()
        {
            Course = await _context.Courses
                         .Include(c => c.Department)
                         .AsNoTracking()
                         .ToListAsync();
        }
    }
}