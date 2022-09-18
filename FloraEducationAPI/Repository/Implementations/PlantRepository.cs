using FloraEducationAPI.Context;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Repository.Implementations
{
    public class PlantRepository : IPlantRepository
    {
        private readonly FloraEducationDbContext context;
        private readonly DbSet<Plant> entites;

        public PlantRepository(FloraEducationDbContext context)
        {
            this.context = context;
            this.entites = context.Set<Plant>();
        }

        public Plant FetchPlantById(Guid plantId)
        {
            return entites
                .Include(e => e.Comments)
                .Include("Comments.Author")
                .SingleOrDefault(e => e.Id == plantId);
        }
    }
}
