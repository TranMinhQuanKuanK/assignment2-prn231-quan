using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class AuthorRepostiory
    {
        public readonly DbSet<Author> _dbSet;
        public readonly BookDbContext _dbContext;
        public AuthorRepostiory(BookDbContext dbContext)
        {
            _dbSet = dbContext.Set<Author>();
            _dbContext = dbContext;
        }

        public DbSet<Author> Get()
        {
            return _dbSet;
        }

        public Author GetById(int id)
        {
            var data = _dbSet.Find(id);
            return data;
        }

        public void Add(Author entity)
        {
             _dbSet.Add(entity);
        }

        public void Update(Author entity)
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
