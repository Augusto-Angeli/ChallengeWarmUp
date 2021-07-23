using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChallengeNivelatorio.Models;

    public class ChallengeBlogContext : DbContext
    {
        public ChallengeBlogContext (DbContextOptions<ChallengeBlogContext> options)
            : base(options)
        {
        }

        public DbSet<ChallengeNivelatorio.Models.Blog> Blog { get; set; }
    }
