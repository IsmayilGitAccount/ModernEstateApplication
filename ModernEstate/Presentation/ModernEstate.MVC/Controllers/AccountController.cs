using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Abstractions.Services;
using ModernEstate.Application.ViewModels;
using ModernEstate.Application.ViewModels.Account;
using ModernEstate.Domain.Entities.Account;
using ModernEstate.Domain.Enums;

namespace ModernEstate.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private static Dictionary<string, string> _verificationCodes = new Dictionary<string, string>();

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            if (!userVM.Email.Contains("@"))
            {
                ModelState.AddModelError("Email", "Email must contain '@'!");
            }
            else
            {
                string email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(userVM.Email, email))
                {
                    ModelState.AddModelError("Email", "Email format is not correct, try again!");
                }
                else if (userVM.Email.Any(char.IsUpper))
                {
                    ModelState.AddModelError("Email", "Email cannot contain uppercase letters!");
                }
            }

            if (userVM.UserName.Any(char.IsUpper))
            {
                ModelState.AddModelError("UserName", "Username cannot contain uppercase letters!");
            }

            if (userVM.Name.Any(char.IsDigit))
            {
                ModelState.AddModelError("Name", "Name cannot contain numbers!");
            }

            if (userVM.Surname.Any(char.IsDigit))
            {
                ModelState.AddModelError("Surname", "Surname cannot contain numbers!");
            }

            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            AppUser user = new AppUser()
            {
                Name = userVM.Name,
                Surname = userVM.Surname,
                Email = userVM.Email,
                UserName = userVM.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(user, userVM.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(userVM);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            TempData["SuccessMessage"] = "You have successfully registered!";

            await _emailService.SendMailAsync(user.Email, "You have successfully registered!", $"Dear {user.Name}, welcome to our family!", false);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            if (string.IsNullOrEmpty(userVM.UserNameorEmail))
            {
                ModelState.AddModelError("UserNameorEmail", "Username or email cannot be empty!");
                return View(userVM);
            }

            if (userVM.UserNameorEmail.Any(char.IsUpper))
            {
                ModelState.AddModelError("UserNameorEmail", "Username or email cannot contain uppercase letters!");
                return View(userVM);
            }


            AppUser user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName.Trim() == userVM.UserNameorEmail.Trim() || u.Email == userVM.UserNameorEmail);

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Username, password, or email is wrong!");
                return View(userVM);
            }

            var result = await _signInManager.PasswordSignInAsync(user, userVM.Password, false, true);

            if(result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your account has locked, please try later!");
                return View(userVM);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username, password, or email is wrong!");
                return View(userVM);
            }

            TempData["SuccessMessage"] = "You have successfully logged in!";

            await _emailService.SendMailAsync(user.Email, "You have successfully logged in!", $"Dear {user.Name}, nice to see you again!", false); 

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> CreateRoles()
        {
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email not found!");
                return View(model);
            }

            string verificationCode = GenerateVerificationCode();
            _verificationCodes[user.Email] = verificationCode;

            await _emailService.SendMailAsync(user.Email, "Verification Code", $"Your verification code is: {verificationCode}", false);

            return RedirectToAction("ConfirmVerification", new { email = user.Email });
        }

        [HttpGet]
        public IActionResult ConfirmVerification(string email)
        {
            if (string.IsNullOrEmpty(email) || !_verificationCodes.ContainsKey(email))
            {
                return RedirectToAction("VerifyEmail");
            }

            return View(new ConfirmVerificationVM { Email = email });
        }

        [HttpPost]
        public IActionResult ConfirmVerification(ConfirmVerificationVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_verificationCodes.TryGetValue(model.Email, out var code) && code == model.VerificationCode)
            {
                _verificationCodes.Remove(model.Email);
                return RedirectToAction("ChangePassword", new { email = model.Email });
            }

            ModelState.AddModelError(string.Empty, "Verification code is not correct!");
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("VerifyEmail");
            }

            return View(new ChangePasswordVM { Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email not found!");
                return View(model);
            }

            var result = await _userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            int value = random.Next(1000000, 9999999); 
            return value.ToString();
        }

        public IActionResult MyProfile()
        {
            var user = _userManager.GetUserAsync(User).Result;

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new UserVM
            {
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
            };

            return View(model);
        }

    }

}

