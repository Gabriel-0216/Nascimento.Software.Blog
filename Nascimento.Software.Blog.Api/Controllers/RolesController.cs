using Domain.Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;

namespace Nascimento.Software.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRepository<Role> _roleRepo;
        private readonly IRepository<UserRole> _userRoleRepo;
        public RolesController(IRepository<Role> roleRepo, IRepository<UserRole> userRepo)
        {
            _userRoleRepo = userRepo;
            _roleRepo = roleRepo;
        }
        [HttpGet]
        [Route("get-roles")]
        public async Task<ActionResult> GetRolesAsync()
        {
            return Ok(await _roleRepo.GetAllAsync());
        }
        [HttpPost]
        [Route("insert-role")]
        public async Task<ActionResult> InsertRole([FromBody] Role role)
        {
            var inserted = await _roleRepo.Create(role);
            if (inserted) return Ok();

            return BadRequest();
        }
        [HttpPost]
        [Route("insert-role-to-user")]
        public async Task<ActionResult> InsertRoleToUser([FromBody] UserRole role)
        {
            var inserted = await _userRoleRepo.Create(role);
            if (inserted) return Ok();

            return BadRequest();
        }


    }
}
