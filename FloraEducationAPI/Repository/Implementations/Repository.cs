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
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly FloraEducationDbContext context;
        private readonly DbSet<T> entities;

        public Repository(FloraEducationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Delete: Entity is null");
            }
            var e = entities.Remove(entity).Entity;
            context.SaveChanges();
            return e;

        }

        public T FetchById(Guid? id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<T> FetchAll()
        {
            return entities.AsEnumerable();
        }

        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Insert: Entity is null");
            }
            var e = entities.Add(entity).Entity;
            context.SaveChanges();
            return e;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Update: Entity is null");
            }
            var e = entities.Update(entity).Entity;
            context.SaveChanges();
            return e;
        }
    }
}
