using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class RoleRepository
    {
        public readonly DbSet<Role> _dbSet;
        public readonly BookDbContext _dbContext;
        public RoleRepository(BookDbContext dbContext)
        {
            _dbSet = dbContext.Set<Role>();
            _dbContext = dbContext;
        }

        public DbSet<Role> Get()
        {
            return _dbSet;
        }

        public Role GetById(int id)
        {
            var data = _dbSet.Find(id);
            return data;
        }

        public void Add(Role entity)
        {
             _dbSet.Add(entity);
        }

        public void Update(Role entity)
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
