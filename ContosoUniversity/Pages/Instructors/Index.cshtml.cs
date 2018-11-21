﻿namespace ContosoUniversity.Pages.Instructors
{
    using System.Linq;
    using System.Threading.Tasks;

    using ContosoUniversity.Models;
    using ContosoUniversity.Models.SchoolViewModels;

    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;

    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        public InstructorIndexData Instructor { get; set; }

        public int InstructorID { get; set; }

        public int CourseID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            Instructor = new InstructorIndexData();
            Instructor.Instructors = await _context.Instructors
                                         .Include(i => i.OfficeAssignment)
                                         .Include(i => i.CourseAssignments)
                                         .ThenInclude(i => i.Course)
                                         .ThenInclude(i => i.Department)

                                         // .Include(i => i.CourseAssignments)
                                         // .ThenInclude(i => i.Course)
                                         // .ThenInclude(i => i.Enrollments)
                                         // .ThenInclude(i => i.Student)
                                         // .AsNoTracking()
                                         .OrderBy(i => i.LastName)
                                         .ToListAsync();

            if (id != null)
            {
                InstructorID = id.Value;
                Instructor instructor = Instructor.Instructors.Where(
                    i => i.ID == id.Value).Single();
                Instructor.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)
            {
                CourseID = courseID.Value;
                var selectedCourse = Instructor.Courses.Where(x => x.CourseID == courseID).Single();
                await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }

                Instructor.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}