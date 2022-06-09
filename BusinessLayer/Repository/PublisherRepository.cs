using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class PublisherRepository
    {
        public readonly DbSet<Publisher> _dbSet;
        public readonly BookDbContext _dbContext;
        public PublisherRepository(BookDbContext dbContext)
        {
            _dbSet = dbContext.Set<Publisher>();
            _dbContext = dbContext;
        }

        public DbSet<Publisher> Get()
        {
            return _dbSet;
        }

        public Publisher GetById(int id)
        {
            var data = _dbSet.Find(id);
            return data;
        }

        public void Add(Publisher entity)
        {
             _dbSet.Add(entity);
        }

        public void Update(Publisher entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _dbSet.Remove(entity);
        }
    }
}
