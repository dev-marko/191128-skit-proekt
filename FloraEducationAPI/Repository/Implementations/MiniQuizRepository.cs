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
    public class MiniQuizRepository : IMiniQuizRepository
    {
        private readonly FloraEducationDbContext context;
        private readonly DbSet<MiniQuiz> entities;

        public MiniQuizRepository(FloraEducationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<MiniQuiz>();
        }

        public MiniQuiz FetchMiniQuizByPlantId(Guid plantId)
        {
            return entities
                .Include(e => e.Plant)
                .Include(e => e.Questions)
                .SingleOrDefault(e => e.PlantId == plantId);
        }
    }
}
