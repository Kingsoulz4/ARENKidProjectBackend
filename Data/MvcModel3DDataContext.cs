using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectBackend.Models;

    public class MvcModel3DDataContext : DbContext
    {
        public MvcModel3DDataContext (DbContextOptions<MvcModel3DDataContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectBackend.Models.Model3DData> Model3DData { get; set; } = default!;
    }
