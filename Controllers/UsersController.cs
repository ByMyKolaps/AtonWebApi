using AtonWebApi.DAL;
using AtonWebApi.DAL.DTO;
using AtonWebApi.DAL.Repository.Interfaces;
using AtonWebApi.DTO;
using AtonWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AtonWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetActiveUsers")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<User> GetActiveUsers()
        {
            return _userRepository.GetActiveUsers();
        }

        [HttpGet("GetUserByLogin")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByLogin(string userLogin)
        {
            var result = _userRepository.GetUserByLogin(userLogin);
            return result.IsSuccess ? Ok(new PartialUserDTO(result.Value)) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("GetUserByLoginAndPassword")]
        [Authorize(Roles = "User")]
        public IActionResult GetByLoginAndPassword(string password)
        {
            var userLogin = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var result = _userRepository.GetUserByLoginAndPassword(userLogin, password);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("GetUsersOlderThan")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<User> GetOlderThan(int age)
        {
            return _userRepository.GetUsersOlderThan(age);
        }


        [HttpPost("CreateUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(UserCreateDTO user)
        {
            if (ModelState.IsValid)
            {
                var userLogin = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                var result = _userRepository.TryCreate(userLogin, user);
                return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
            }
            else
                return BadRequest(ModelState);

        }

        [HttpPost("UpdateUser")]
        public IActionResult Update(UserUpdateDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var userLogin = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                Maybe result;
                if (HttpContext.User.IsInRole("Admin"))
                    result = _userRepository.TryUpdate(userLogin, userDTO.UserLoginToUpdate, userDTO);
                else
                    result = _userRepository.TryUpdate(userLogin, userLogin, userDTO);
                return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
            }
            else
                return BadRequest(ModelState);
        }


        [HttpPost("ChangeUserPassword")]
        public IActionResult ChangePassword(ChangeUserPasswordDTO passwordDTO)
        {
            if (ModelState.IsValid)
            {
                var userLogin = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                Maybe result;
                result = _userRepository.ChangePassword(userLogin, passwordDTO);
                return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPost("RestoreUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult Restore(string userLogin)
        {
            var result = _userRepository.RestoreUser(userLogin);
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }

        [HttpPost("ChangeUserLogin")]
        public IActionResult ChangeLogin(ChangeUserLoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var userLogin = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                Maybe result;
                result = _userRepository.ChangeLogin(userLogin, loginDTO);
                return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(DeleteUserDTO deleteUserDTO)
        {
            if (ModelState.IsValid)
            {
                var userLogin = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                Maybe result;
                if (deleteUserDTO.IsCompletely)
                    result = _userRepository.DeleteUserCompletely(deleteUserDTO.UserLogin);
                else
                    result = _userRepository.DeleteUserSoftly(userLogin, deleteUserDTO.UserLogin);
                return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
            }
            return BadRequest(ModelState);
            
        }
    }
}
    