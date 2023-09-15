using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobInsight.Model;

namespace JobInsight.Data
{
    public class JobInsightContext : DbContext
    {
        public JobInsightContext (DbContextOptions<JobInsightContext> options)
            : base(options)
        {
        }

        public DbSet<JobInsight.Model.Application> Application { get; set; } = default!;
    }
}
