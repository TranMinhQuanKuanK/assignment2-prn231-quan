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

namespace Assignment2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ODataController
    {
        private BookDbContext _dbContext;
        public BooksController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<BooksController>
        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            BookRepository repo = new BookRepository(_dbContext);
            return Ok(repo.Get());
        }

        // GET api/<BooksController>/5
        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            BookRepository repo = new BookRepository(_dbContext);
            return Ok(repo.GetById(id));
        }

        // POST api/<BooksController>
        [EnableQuery]
        [HttpPost]
        public void Post([FromBody] Book book)
        {
            BookRepository repo = new BookRepository(_dbContext);
            repo.Get();
            repo.Add(book);
            _dbContext.SaveChanges();
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        [EnableQuery]
        public void Put(int id, [FromBody] Book book)
        {
            BookRepository repo = new BookRepository(_dbContext);
            repo.Update(book);
            _dbContext.SaveChanges();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
