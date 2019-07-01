using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Working.Models;
using Working.ViewModels;

namespace Working.Controllers
{
    public class AuthController: Controller
    {
        private SignInManager<WorkingUser> _signInManager;
        private UserManager<WorkingUser> _userManager;
        private IConfigurationRoot _config;
        private object token = null;

        public AuthController(SignInManager<WorkingUser> signInManager, UserManager<WorkingUser> userManager, IConfigurationRoot config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                
                return RedirectToAction("Trips", "App");
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(
                    vm.UserName, vm.Password, true, false);
                if (signInResult.Succeeded)
                {
                  //  var appUser= _userManager.Users.SingleOrDefault(r => r.UserName == vm.UserName);

                   // token = GenerateJwtToken(vm.UserName, appUser);

                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Trips", "App");

                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                   // return RedirectToAction("Trips", "App");

                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            token = null;
            return RedirectToAction("Index", "App");
        }
    }
}
