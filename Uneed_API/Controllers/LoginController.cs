﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uneed_API.Services;

namespace Uneed_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IServiceLogin _serviceLogin;
        public LoginController(IServiceLogin serviceLogin)
        {
            _serviceLogin = serviceLogin;
        }
        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult> Login(Models.Login login)
        {
            var user = await _serviceLogin.Login(login);
            if(user == null)
            {
                return Unauthorized();
            }
            return Ok(new
            {
                token = _serviceLogin.generateToken(user),
                userId = user.Id,
                userEmail = user.Email,
            });
        }
       
    }
       
    }
