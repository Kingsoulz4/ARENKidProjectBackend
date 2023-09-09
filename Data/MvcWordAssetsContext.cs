using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectBackend.Models;

    public class MvcWordAssetsContext : DbContext
    {
        public MvcWordAssetsContext (DbContextOptions<MvcWordAssetsContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectBackend.Models.WordAssets> WordAssets { get; set; } = default!;
    }
