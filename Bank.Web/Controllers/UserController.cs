using AutoMapper;
using Bank.Infrastructure.Identity;
using Bank.Web.Services;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var users = _userService.GetAll();

            var model = new UserListViewModel
            {
                Users = _mapper.Map<List<UserViewModel>>(users)
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserProfile(string id)
        {
            var user = await _userService.Get(id);

            var model = _mapper.Map<UserProfileViewModel>(user);
            model.Role = await _userService.GetUserRole(user);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {
            var model = new CreateUserViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _mapper.Map<ApplicationUser>(model);
            var succeeded = await _userService.CreateUser(user, model.Password, model.Role);
            if (succeeded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(string id)
        {
            var user = _userService.Get(id);
            var model = _mapper.Map<EditUserViewModel>(user);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _mapper.Map<ApplicationUser>(model);
            var succeeded = await _userService.EditUser(user, model.Password, model.Role);
            if (succeeded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.Get(id);

            var succeeded = await _userService.DeleteUser(user);
            if (succeeded) return RedirectToAction(nameof(Index));

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userService.GetByEmail(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Incorrect username and/or password.");
                return View(model);
            }

            var result = await _userService.SignInUser(user.UserName, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect username and/or password.");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutUser();
            return RedirectToAction(nameof(Login));
        }
    }
}