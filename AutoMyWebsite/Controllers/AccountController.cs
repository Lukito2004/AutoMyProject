using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMy.Database;
using AutoMy.DomainModels;
using AutoMy.DomainModels.Exstensions;
using AutoMy.Interfaces;
using AutoMy.ServiceModels;
using AutoMyWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMyWebsite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string hey = "wtf";
        private readonly string hey1 = "hi wtff";

        private readonly IMapper _mapper;

        public AccountController(UserManager<Account> userManager, 
            SignInManager<Account> signInManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public IActionResult Register() => View();

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                Account RealAccount = new Account()
                {
                    Email = account.Email,
                    DateOfBirth = account.DateOfBirth,
                    UserName = account.Username,
                    ImageUrl = account.ImageUrl,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    PhoneNumber = "+995" + account.PhoneNumber.ToString()
                };

                var result = await _userManager.CreateAsync(RealAccount, account.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(RealAccount, true);
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(account);
        }

        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl) => View();

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, true, false);
                
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", "Please check your username and password");
                }
            }

            return View(signInModel);
        }

        [AllowAnonymous]
        public IActionResult Index(string username)
        {
            Account account = _userManager.Users.Where(o => o.UserName == username).Include(p => p.Posts).FirstOrDefault();
            return View(_mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(account)));
        }

        public async Task<IActionResult> ToMyPage()
        {
            Account account = await _userManager.GetUserAsync(HttpContext.User);
            
            Account realaccount = _userManager.Users.Where(o => o.Id == account.Id).Include(c => c.Posts).FirstOrDefault();

            AccountDTO accountDTO = _mapper.Map<AccountDTO>(realaccount);

            AccountViewModel accountViewModel = _mapper.Map<AccountViewModel>(accountDTO);

            return View("Index", accountViewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ReturnId() => Json((await _userManager.GetUserAsync(HttpContext.User)).Id);

        [AllowAnonymous]
        public async Task<IActionResult> ReturnUsername()
        {
            Account account = await _userManager.GetUserAsync(HttpContext.User);
            string username = account.UserName;
            return Json(username);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ReturnPosterUsername(string usernameId) => Json((await _userManager.FindByIdAsync(usernameId)).UserName);

        [AllowAnonymous]
        public IActionResult IndexWithUsername(string Username)
        {
            AccountViewModel account = _mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(_userManager.Users.Where(o => o.UserName == Username).Include(p => p.Posts)));
            return View("Index", account);
        }

        [AllowAnonymous]
        public IActionResult IndexWithId(string id)
        {
            AccountViewModel account = _mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(_userManager.Users.Where(o => o.Id == id).Include(p => p.Posts)));
            return View("Index", account);
        }

        public async Task<IActionResult> Edit() => View(_mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(await _userManager.GetUserAsync(HttpContext.User))));

        [HttpPost]
        public async Task<IActionResult> Edit(AccountViewModel accountViewModel)
        {
            Account Newestaccount = await _userManager.GetUserAsync(HttpContext.User);
            string password = new PasswordHasher<AccountViewModel>().HashPassword(accountViewModel, accountViewModel.Password);
            Newestaccount.PasswordHash = password;
            Newestaccount.PhoneNumber = accountViewModel.PhoneNumber;
            Newestaccount.DateOfBirth = accountViewModel.DateOfBirth;
            Newestaccount.Email = accountViewModel.Email;
            Newestaccount.UserName = accountViewModel.Username;
            Newestaccount.FirstName = accountViewModel.FirstName;
            Newestaccount.LastName = accountViewModel.LastName;
            await _userManager.UpdateAsync(Newestaccount);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(Newestaccount, true);
            return View("Index", _mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(await _userManager.GetUserAsync(HttpContext.User))));
        }

        private async Task CreateRole(string RoleName) => await _roleManager.CreateAsync(new IdentityRole(RoleName));

        public async Task AddToAdmin(Account account) => await _userManager.AddToRoleAsync(account, "Admin");

        public async Task<IActionResult> IsAccountInRole(string username)
        {
            if (await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(username), "Admin"))
                return Json(true);
            else
                return Json(false);
        }

        [AllowAnonymous]
        public async Task<IActionResult> IsItAuthenticated ()
        {
            if ((await _userManager.GetUserAsync(HttpContext.User)) == null)
                return Json(false);
            else
                return Json(true);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return View("SignIn");
        }
    }
}