using AutoMapper;
using Bank.Application.Exceptions;
using Bank.Application.Services;
using Bank.Infrastructure.Identity;
using Bank.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(
            IMapper mapper, 
            IUserService userService,
            SignInManager<ApplicationUser> signInManager)
        {
            _mapper = mapper;
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("users")]
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
        [Route("users/{id}")]
        public async Task<IActionResult> UserProfile(string id)
        {
            var user = await _userService.Get(id);

            var model = _mapper.Map<UserProfileViewModel>(user);
            model.Role = await _userService.GetUserRole(user);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("users/create")]
        public IActionResult CreateUser()
        {
            var model = new CreateUserViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("users/create")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var user = _mapper.Map<ApplicationUser>(model);
                var succeeded = await _userService.CreateUser(user, model.Password, model.Role);
                if (succeeded) return RedirectToAction(nameof(Index));
            }
            catch (UserExistsException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("users/edit/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.Get(id);
            var model = _mapper.Map<EditUserViewModel>(user);
            model.CurrentRole = await _userService.GetUserRole(user);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("users/edit/{id}")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var succeeded = await _userService.EditUser(model.UserId, model.Email, model.Password, model.CurrentRole, model.NewRole);
                if (succeeded) return RedirectToAction(nameof(Index));
            }
            catch (MatchingRolesException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("users/delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.Get(id);

            var succeeded = await _userService.DeleteUser(user);
            if (succeeded) return RedirectToAction(nameof(Index));

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var user = await _userService.GetByEmail(model.Email);
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded == false)
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return View(model);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (EmailNotFoundException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}