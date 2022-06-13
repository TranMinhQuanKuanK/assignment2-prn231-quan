using BusinessLayer.Repository;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DTO;



namespace eBookStoreWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ODataController
    {
        private BookDbContext _dbContext;
        private UserRepostiory _repo; 
        public LoginController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
            _repo = new UserRepostiory(_dbContext);
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginCreateModel model)
        {
            var user = _repo.Login(model);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }
    }
}
