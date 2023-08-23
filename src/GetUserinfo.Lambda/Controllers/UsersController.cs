using GetUserinfo.Lambda.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GetUserinfo.Lambda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PoliciesContext _policiesContext;

        public UsersController(PoliciesContext policiesContext)
        {
            _policiesContext = policiesContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<User> users = await _policiesContext.Users
                                        .Include(u => u.Permissions)
                                        .ToListAsync();

            return Ok(users);
        }
    }
}
