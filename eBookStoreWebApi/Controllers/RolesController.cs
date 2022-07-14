using BusinessLayer.Repository;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eBookStoreWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ODataController
    {
        private BookDbContext _dbContext;
        private RoleRepository _repo; 

        public RolesController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
            _repo = new RoleRepository(_dbContext);
        }
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.Get());
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_repo.GetById(id));
        }

        [EnableQuery]
        [HttpPost]
        public void Post([FromBody] Role role)
        {
            _repo.Get();
            _repo.Add(role);
            _dbContext.SaveChanges();
        }

        [HttpPut("{id}")]
        [EnableQuery]
        public void Put(int id, [FromBody] Role role)
        {
            _repo.Update(role);
            _dbContext.SaveChanges();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
            _dbContext.SaveChanges();
        }
    }
}
