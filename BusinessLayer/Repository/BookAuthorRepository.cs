using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class BookAuthorRepository
    {
        public readonly DbSet<BookAuthor> _dbSet;
        public readonly BookDbContext _dbContext;
        public BookAuthorRepository(BookDbContext dbContext)
        {
            _dbSet = dbContext.Set<BookAuthor>();
            _dbContext = dbContext;
        }

        public DbSet<BookAuthor> Get()
        {
            return _dbSet;
        }

        public BookAuthor GetById(int id)
        {
            var data = _dbSet.Find(id);
            return data;
        }

        public void Add(BookAuthor entity)
        {
             _dbSet.Add(entity);
        }

        public void Update(BookAuthor entity)
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
