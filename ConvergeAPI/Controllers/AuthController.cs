
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Response;
using BusinessLogicLayer.Service;
using BusinessObjectsLayer.Entities;
using DataAccessLayer.Repositories;
using ErrorLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ConvergeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] BusinessObjectsLayer.Entities.LoginRequest loginRequest)
        {
            try
            {
                var token = await _userService.Authenticate(loginRequest.UserId, loginRequest.Password);
                if (token == null)
                {
                    return Ok(ResponseHelper.GetFailureResponse(ResponseMessage.InvalidCredentials));
                }
                return Ok(ResponseHelper.GetSuccessResponse(token));
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(Authenticate), ErrorLogger.ConvertObjectToDictionary(loginRequest));
                ErrorLogger.LogError("AuthController", "Authenticate", request, ex);
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }



        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var resp = await _userService.ChangePassword(changePassword);
                return Ok(ResponseHelper.GetSuccessResponse(resp));
            }
            catch (Exception ex)
            {
                string request = ErrorLogger.LogMethodParameters(nameof(Authenticate), ErrorLogger.ConvertObjectToDictionary(changePassword));
                ErrorLogger.LogError("AuthController", "ChangePassword", request, ex);
                return Ok(ResponseHelper.GetFailureResponse());
            }
        }
    }
}
