using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JobInsight.Data;
using JobInsight.Model;

namespace JobInsight.Pages.Applications
{
    public class IndexModel : PageModel
    {
        private readonly JobInsight.Data.JobInsightContext _context;

        public IndexModel(JobInsight.Data.JobInsightContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }

        public IList<Application> Application { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            if (_context.Application != null)
            {
                Application = await _context.Application.ToListAsync();
            }

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";

            IQueryable<Application> applications = from a in _context.Application select a;

            switch (sortOrder)
            {
                case "name_desc": 
                    applications = applications.OrderByDescending(c => c.Company);
                    break;
                case "date_desc":
                    applications = applications.OrderByDescending(c => c.DateApplied);
                    break;
                default:
                    break;
            }

            Application = await applications.AsNoTracking().ToListAsync();

        }
    }
}
