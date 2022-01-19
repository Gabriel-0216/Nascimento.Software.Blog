using Domain.Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;

namespace Nascimento.Software.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDAL _userDAL;
        public UsersController(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        [HttpGet]
        [Route("get-all-users")]
        public async Task<ActionResult> GetAllUsersAsync()
        {
            return Ok(await _userDAL.GetUsersAsync());
        }

        [HttpPost]
        [Route("insert-user")]
        public async Task<ActionResult> InsertUser([FromBody] User user)
        {
            var insert = await _userDAL.CreateUser(user);
            if (insert) return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet]
        [Route("get-user-by-id")]
        public async Task<ActionResult> GetUserByIdAsync([FromHeader] int Id)
        {
            return Ok(await _userDAL.GetUserAsync(Id));
        }

        [HttpPut]
        [Route("update-user")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] User user)
        {
            var updated = await _userDAL.UpdateUser(user);
            if (updated) return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [Route("delete-user-by-id")]
        public async Task<ActionResult> DeleteUserAsync([FromHeader] int Id)
        {
            var userExists = await _userDAL.GetUserAsync(Id);
            if (userExists == null) return BadRequest();

            var deleted = await _userDAL.RemoveUser(userExists);
            if (deleted) return Ok();

            return BadRequest();
        }

        [HttpGet]
        [Route("get-users-with-roles")]
        public async Task<ActionResult> GetUsersWithRolesAsync()
        {
            return Ok(await _userDAL.GetUsersWithRoles());
        }
    }
}
