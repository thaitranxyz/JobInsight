using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JobInsight.Data;
using JobInsight.Model;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Logging.Abstractions;
using NuGet.Versioning;

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
        public string Ghosted { get; set; }
        public string NotReady { get; set; }
        public string JobDescription { get; set; }

        public IList<Application> Application { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string filter)
        {
            
            if (_context.Application != null)
            {
                Application = await _context.Application.ToListAsync();
            }

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            Ghosted = String.IsNullOrEmpty(filter) ? "ghosted" : "";
            NotReady = String.IsNullOrEmpty(filter) ? "not_ready" : "";

            IQueryable<Application> applications = from a in _context.Application select a;


            switch (filter)
            {
                case "ghosted":
                    applications = applications.Where(x => x.Result == "3");
                    break;
                case "not_ready":
                    applications = applications.Where(x => x.Result == "0");
                    break;
                default: 
                    break;
            }

            switch (sortOrder)
            {
                case "name_desc": 
                    applications = applications.OrderByDescending( c => c.Company);
                    break;
                case "date_desc":
                    applications = applications.OrderByDescending(c => c.DateApplied);
                    break;
                default:
                    break;
            }

            foreach (var app in applications)
            {
                app.JobDescription = app.JobDescription.Replace(@"\n", "<br>");
            }

            Application = await applications.AsNoTracking().ToListAsync();

        }

        public async Task<IActionResult> OnPostDestroy()
        {
            await Console.Out.WriteLineAsync("DESTROY EVERYTHING!!!");
            //var applications = _context.Application.ToList();

            //foreach (var application in applications)
            //{
            //    _context.Remove(application);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }

        public string GetJobDescription(int? id)
        {
            var application = this.Application.FirstOrDefault(x => x.Id == id); 


            if (application == null)
            {
                return "Not Found";
            }
            Console.WriteLine("Clicked");
            Console.WriteLine("JOB DESC ID: " + id);
            JobDescription = application.JobDescription;
            return application.JobDescription;
        }
    }
}
