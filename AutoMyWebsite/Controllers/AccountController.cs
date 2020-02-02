using System;
using System.Collections.Generic;
using System.Linq;
using MailKit;
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
using MimeKit;
using System.Net.Mail;
using System.Net;

namespace AutoMyWebsite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMapper _mapper;

        public AccountController(UserManager<Account> userManager,
            SignInManager<Account> signInManager, IMapper mapper,
            RoleManager<IdentityRole> roleManager)
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

        //public IActionResult Details(string username)
        //{
        //    Account account = _userManager.Users.Where(o => o.UserName == username).Include(p => p.Posts).FirstOrDefault();
        //    return View(_mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(account)));
        //}
        
        public async Task<IActionResult> Details() => View(_mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(await _userManager.GetUserAsync(HttpContext.User))));


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
            AccountViewModel accountViewModel = _mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(_userManager.Users.Where(o => o.UserName == Username).Include(p => p.Posts).FirstOrDefault()));
            return View("Index", accountViewModel);
        }

        [AllowAnonymous]
        public IActionResult IndexWithId(string id)
        {
            AccountViewModel account = _mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(_userManager.Users.Where(o => o.Id == id).Include(p => p.Posts).FirstOrDefault()));
            return View("Index", account);
        }

        public async Task<IActionResult> Edit() => View(_mapper.Map<AccountViewModel>(_mapper.Map<AccountDTO>(await _userManager.GetUserAsync(HttpContext.User))));



        [HttpPost]
        public async Task<IActionResult> Edit(AccountViewModel accountViewModel)
        {
            Account Newestaccount = await _userManager.GetUserAsync(HttpContext.User);
            Newestaccount.PasswordHash = HashPassword(accountViewModel.Password);
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

        public async Task CreateRole(string RoleName) => await _roleManager.CreateAsync(new IdentityRole(RoleName));

        public async Task AddToAdmin(string AccountId) => await _userManager.AddToRoleAsync((await _userManager.FindByIdAsync(AccountId)), "Admin");

        public async Task<IActionResult> IsAccountInRole(string username)
        {
            if (await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(username), "Admin") && username.ToLower() == (await _userManager.GetUserAsync(HttpContext.User)).UserName.ToLower())
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

        [AllowAnonymous]
        public IActionResult ForgotPassword() => View();

        [AllowAnonymous]
        public IActionResult EnterVerificationCode(string mail) => View(model: mail);

        [AllowAnonymous]
        public async Task<IActionResult> SendVeryficationCode(string email)
        {
            if (!email.IsAnything())
                return Json("გთხოვთ შეიყვანეთ იმეილი");
            else
            {
                Account account = await _userManager.FindByEmailAsync(email);
                if (account == null || !account.Id.IsAnything())
                    return Json("მოხმარებელი ამ იმეილით არ არსებობს");
                else
                {
                    await SendMessageToGmail(email);
                    return Json("success");
                }
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ReturnPhoneNumber(string accountId) => Json((await _userManager.FindByIdAsync(accountId)).PhoneNumber);

        [AllowAnonymous]
        public async Task<IActionResult> ReturnPhoneNumberWithUsername(string username) => Json((await _userManager.FindByNameAsync(username)).PhoneNumber);

        [AllowAnonymous]
        private async Task<int> MakeAndApplyActivationCode(string Email)
        {
            Random rand = new Random();
            string ActivationString = "";
            for (var i = 0; i < 6; i++)
            {
                ActivationString = ActivationString + rand.Next(0, 10);
            }
            int ActivationInt = Convert.ToInt32(ActivationString);
            Account account = await _userManager.FindByEmailAsync(Email);
            account.ActivationCode = ActivationInt;
            await _userManager.UpdateAsync(account);
            return ActivationInt;
        }

        [AllowAnonymous]
        public async Task<IActionResult> CheckVerificationCode(int verificationCode, string Email)
        { 
            Account account = await _userManager.FindByEmailAsync(Email);
            if (account.ActivationCode == verificationCode)
                return Json("success");
            else
                return Json("კოდი არასწორია, გთხოვთ გადაამოწმეთ");
        }

        [AllowAnonymous]
        public IActionResult ChangePassword(string mail) => View(model: mail);

        [AllowAnonymous]
        [HttpPost]
        public async Task ChangePassword(string email, string password)
        {
            Account acc = await _userManager.FindByEmailAsync(email);
            acc.PasswordHash = HashPassword(password);
            await _userManager.UpdateAsync(acc);
            await _signInManager.SignInAsync(acc, true);
        }

        [AllowAnonymous]
        private async Task SendMessageToGmail(string MessageRecieverGmail)
        {
            Account SendingAccount = await _userManager.FindByEmailAsync(MessageRecieverGmail);
            var Message = new MailMessage("lukito.javaxishvili2004@gmail.com", MessageRecieverGmail);
            int VerificationCode = await MakeAndApplyActivationCode(MessageRecieverGmail);
            Message.Subject = "პაროლის აღდგენა";
            Message.Body = "<h1>გამარჯობა " + SendingAccount.UserName + ", თუ გსურთ თქვენი პაროლის აღდგენა... შეიყვანეთ ეს კოდი: " + VerificationCode  + "</h1>";
            Message.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("lukito.javaxishvili2004@gmail.com", "2N1GL7F1");
                smtp.EnableSsl = true;
                smtp.Send(Message);
            }
        }
        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}