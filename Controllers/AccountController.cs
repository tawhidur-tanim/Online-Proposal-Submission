using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Resources;
using ProjectFinal101.Persistance;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Bulk()
        {
            var hasher = new PasswordHasher<string>();

            var list = new List<ApplicationUser>();

            //  var background = new Thread(InsertUser);

            //   background.Start();
            for (var i = 0; i < 500; i++)
            {
                var user = new ApplicationUser
                {
                    UserName = "j-" + i,
                    FullName = "Jonny Test",
                    //ConcurrencyStamp = Guid.NewGuid().ToString(),
                    //SecurityStamp = Guid.NewGuid().ToString(),
                    //PasswordHash = hasher.HashPassword("Jonny Test", "tanim123")
                };
                //list.Add(user);
                await _userManager.CreateAsync(user, "tanim123");

                await _userManager.AddToRoleAsync(user, "Student");
            }

            //_context.BulkInsert(list);

            return Ok();
        }


        private void InsertUser()
        {
            for (var i = 0; i < 1000; i++)
            {
                var user = new ApplicationUser
                {
                    UserName = "j-" + i,
                    FullName = "Jonny Test",
                    //ConcurrencyStamp = Guid.NewGuid().ToString(),
                    //SecurityStamp = Guid.NewGuid().ToString(),
                    //PasswordHash = hasher.HashPassword("Jonny Test", "tanim123")
                };
                //list.Add(user);
                _userManager.CreateAsync(user, "tanim123");

                _userManager.AddToRoleAsync(user, "Student");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RoleSeed()
        {
            var admin = await _roleManager.FindByNameAsync("Admin");

            if (admin == null)
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

            var student = await _roleManager.FindByNameAsync("Student");

            if (student == null)
                await _roleManager.CreateAsync(new IdentityRole { Name = "Student" });

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterResource model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var user = new ApplicationUser
                {
                    UserName = model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return Ok();
                }
                AddErrors(result);

            }
            catch (Exception e)
            {
                return BadRequest("Somwthing Gone Wrong");
            }

            return BadRequest();

        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginResource model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (!result.Succeeded) return BadRequest("Wrong username or password");

                var user = _userManager.Users.SingleOrDefault(x => x.UserName == model.UserName);
                var roles = await _userManager.GetRolesAsync(user);

                if (user == null) return BadRequest("Something gone wrong");

                var authData = new AuthDataResource
                {
                    Token = GenerateJwtToken(user, roles),
                    Id = user.Id,
                    ExpireTime = DateTime.Now.AddHours(Convert.ToDouble(_configuration["JwtExpireDays"])),
                    Roles = roles,

                    Name = user.FullName + " [" + user.UserName + "]"
                };

                return Ok(authData);
            }
            catch (Exception e)
            {
                // ignored
            }

            return BadRequest("Something gone wrong");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PasswordChange(RegisterResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

                var result = await _userManager.ChangePasswordAsync(user, resource.Password, resource.ConfirmPassword);

                if (result.Succeeded)
                    return Ok();

                return BadRequest(result.Errors);

            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetMenu()
        {
            var admin = new List<Menu>
            {
                new Menu { Route = "semesters", DisplayName = "Semesters", Icon = "fa fa-calendar"},
                new Menu { Route = "proposals", DisplayName = "Proposals", Icon = "fa fa-lightbulb-o"},
                new Menu { Route = "manageProposal", DisplayName = "Manage Proposal", Icon = "fa fa-clock-o"},
                new Menu { Route = "supervisor", DisplayName = "Supervised Student", Icon = "fa fa-sitemap"},
                new Menu { Route = "reviewer", DisplayName = "Review Student", Icon = "fa fa-sitemap"},
                new Menu { Route = "settings", DisplayName = "System Settings", Icon = "fa fa-cog"},
                new Menu { Route = "gpa", DisplayName = "Course GPA", Icon = "fa fa-keyboard-o"},

            };

            var students = new List<Menu>
            {
                new Menu { Route = "proposals", DisplayName = "Proposals", Icon = "fa fa-lightbulb-o"},
                new Menu { Route = "gpa", DisplayName = "Course GPA", Icon = "fa fa-keyboard-o"},

            };

            var teachers = new List<Menu>
            {
                new Menu { Route = "manageProposal", DisplayName = "Manage Proposal", Icon = "fa fa-clock-o"},
                new Menu { Route = "supervisor", DisplayName = "Supervised Student", Icon = "fa fa-sitemap"},
                new Menu { Route = "reviewer", DisplayName = "Review Student", Icon = "fa fa-sitemap"}

            };

            if (User.IsInRole(RoleReference.Student))
            {
                return Ok(students);
            }

            if (User.IsInRole(RoleReference.Admin))
            {
                return Ok(admin);
            }

            if (User.IsInRole(RoleReference.Teacher))
            {
                return Ok(teachers);
            }

            return Ok(new List<Menu>());
        }


        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName ?? "")
            };

            // var roles = await _userManager.GetRolesAsync(user);

            if (roles.Any())
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
