using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class BookRepository
    {
        public readonly DbSet<Book> _dbSet;
        public readonly BookDbContext _dbContext;
        public BookRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Book>();
        }

        public DbSet<Book> Get()
        {
            return _dbSet;
        }

        public Book GetById(int id)
        {
            var data = _dbSet.Find(id);
            return data;
        }

        public void Add(Book entity)
        {
             _dbSet.Add(entity);
            
        }

        public void Update(Book entity)
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
