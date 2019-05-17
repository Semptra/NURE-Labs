using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab5.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    [ApiController]
    public class EntitiesController : ControllerBase
    {
        private readonly MyDbContext context;

        public EntitiesController(MyDbContext context)
        {
            this.context = context;
        }

        [HttpGet("api/users")]
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        [HttpGet("api/users/{id}")]
        public async Task<User> GetUserAsync(Guid id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
