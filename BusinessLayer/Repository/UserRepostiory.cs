using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTO;

namespace BusinessLayer.Repository
{
    public class UserRepostiory
    {
        public readonly DbSet<User> _dbSet;
        public readonly BookDbContext _dbContext;
        public UserRepostiory(BookDbContext dbContext)
        {
            _dbSet = dbContext.Set<User>();
            _dbContext = dbContext;
        }

        public DbSet<User> Get()
        {
            return _dbSet;
        }

        public User GetById(int id)
        {
            var data = _dbSet.Find(id);
            return data;
        }

        public void Add(User entity)
        {
             _dbSet.Add(entity);
        }

        public void Update(User entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _dbSet.Remove(entity);
        }

        public User Login(LoginCreateModel model)
        {
            var user = _dbSet.FirstOrDefault(u => u.EmailAddress == model.Email
                                                  && u.Password == model.Password);
            return user;
        }
    }
}
