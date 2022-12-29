using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Dtos;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : BaseApiController
    {
        public UserManager<AppUser> _userManager { get; }
        private readonly IMapper _mapper;
        public UserController(UserManager<AppUser> userManager,IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<UserToReturnDto>>> GetAllUsersAsync()
        {
            var list = _userManager.Users.ToList();
            var returnList = new List<UserToReturnDto> ();

            foreach (var user in list)
            {
                var role =await _userManager.GetRolesAsync(user);
                var newUser = new UserToReturnDto 
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Role = role.FirstOrDefault()
                };
                returnList.Add(newUser);
            }

            return returnList;
        }
    }
}