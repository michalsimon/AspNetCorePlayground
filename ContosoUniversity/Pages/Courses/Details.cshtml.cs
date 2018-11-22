﻿namespace ContosoUniversity.Pages.Courses
{
    using System.Threading.Tasks;

    using ContosoUniversity.Models;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class DetailsModel : PageModel
    {
        private readonly SchoolContext _context;

        public DetailsModel(SchoolContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Courses
                         .AsNoTracking()
                         .Include(c => c.Department)
                         .FirstOrDefaultAsync(m => m.CourseID == id);

            if (Course == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}