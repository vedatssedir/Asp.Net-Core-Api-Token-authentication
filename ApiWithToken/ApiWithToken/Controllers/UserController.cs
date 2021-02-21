using ApiWithToken.Domain.Entities;
using ApiWithToken.Domain.Services;
using ApiWithToken.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ApiWithToken.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            var claims = User.Claims;
            var userId = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).First();
            var userResponse = _userService.FindById(int.Parse(userId));
            if (userResponse.Success)
            {
                return Ok(userResponse.User);
            }
            return BadRequest(userResponse.Message);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser(UserResource userResource)
        {
            var user = _mapper.Map<UserResource, User>(userResource);
            var userResponse = _userService.Add(user);
            if (userResponse.Success)
            {
                return Ok(userResponse.User);
            }
            return BadRequest(userResponse.Message);
        }
    }
}