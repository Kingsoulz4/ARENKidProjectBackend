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

        public DbSet<ProjectBackend.Models.Model3DData>? Model3DData { get; set; }

        public DbSet<ProjectBackend.Models.Mode3DBehaviorData>? Mode3DBehaviorData { get; set; }

        public DbSet<ProjectBackend.Models.VideoData>? VideoData { get; set; }

        public DbSet<ProjectBackend.Models.AudioData>? AudioData { get; set; }

        public DbSet<ProjectBackend.Models.WordAssetData>? WordAssetData { get; set; }
    }
