using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobInsight.Data;
using JobInsight.Model;

namespace JobInsight.Pages.Applications
{
    public class EditModel : PageModel
    {
        private readonly JobInsight.Data.JobInsightContext _context;

        public EditModel(JobInsight.Data.JobInsightContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Application Application { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application =  await _context.Application.FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            Application = application;

            Application.JobDescription = Application.JobDescription.Replace(@"\n", "\r\n");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            await Console.Out.WriteLineAsync(Application.JobDescription);
            _context.Attach(Application).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(Application.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ApplicationExists(int id)
        {
          return (_context.Application?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
