using AutoMy.DomainModels;
using AutoMy.Interfaces;
using AutoMy.ServiceModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMy.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Account> _userManager;
        public AccountService(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }

    }
}
