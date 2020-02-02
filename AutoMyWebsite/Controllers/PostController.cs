using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMy.DomainModels;
using AutoMy.DomainModels.Exstensions;
using AutoMy.Interfaces;
using AutoMy.ServiceModels;
using AutoMyWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoMyWebsite.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IMapper mapper;

        private readonly IPostService postService;

        private readonly ICategoryService categoryService;

        private readonly UserManager<Account> _userManager;
        public PostController(IMapper _mapper, IPostService _postService, ICategoryService _categoryService, UserManager<Account> userManager)
        {
            mapper = _mapper;
            postService = _postService;
            categoryService = _categoryService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index() => View(mapper.Map<List<PostViewModel>>(postService.GetAllPosts().Reverse()));
        
        public async Task<IActionResult> Create()
        {
            var model = new PostViewModel();

            await ApplyModel(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel post)
        {
            post.PostedTime = DateTime.Now.Date;
            post.AccountId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            postService.AddPost(mapper.Map<PostDTO>(post));
            return View("Details", post);
        }
        
        [AllowAnonymous]
        public IActionResult Details(int Postid) => View(mapper.Map<PostViewModel>(postService.GetPostWithId(Postid)));

        public IActionResult Edit(int id)
        {
            PostViewModel post = mapper.Map<PostViewModel>(postService.GetPostWithId(id));
            ApplyModel(post);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel post)
        {
            post.AccountId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            PostDTO postdto = mapper.Map<PostDTO>(post);
            postService.UpdatePost(postdto);
            return View("Details", post);
        }

        [HttpPost]
        public async Task<IActionResult> Report(int postId, string reason)
        {
            if (reason.IsAnything())
            {
                bool boolean = postService.AddReport(new ReportDTO() { Reason = reason, PostId = postId, SenderAccountId = (await _userManager.GetUserAsync(HttpContext.User)).Id });
                if (boolean)
                    return Json("წარმატებით გაიგზავნა");
                else
                    return Json("მხოლოდ 1ხელ შეგიძლია განცხადების დარეპორტება");
            }
            else
                return Json("გთხოვთ შეიყვანეთ დარეპორტების მიზეზი");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Filter(int categoryId, int GreaterThanMoney, int LessThanMoney)
        {
            IEnumerable<PostViewModel> accountViewModels = mapper.Map<List<PostViewModel>>(postService.GetAllPosts().Where(o => o.CategoryId == categoryId && o.Price > GreaterThanMoney && o.Price < LessThanMoney));
            return View("Index", accountViewModels);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Reports() => View(mapper.Map<List<ReportViewModel>>(postService.GetAllReports()));

        private Task ApplyModel(PostViewModel post)
        {
            return Task.Run(() =>
            {
                post.DriveWheelsListItem = Enum.GetValues(typeof(LeadingWheels)).Cast<LeadingWheels>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
                post.FuelListItem = Enum.GetValues(typeof(FuelType)).Cast<FuelType>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
                post.GearBoxListItem = Enum.GetValues(typeof(GearBoxType)).Cast<GearBoxType>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
                post.CategoryListItem = categoryService.GetAllCategories().Select(o => new SelectListItem() { Text = o.Name, Value = o.Id.ToString() });
            });
        }

        [AllowAnonymous]
        public IActionResult ReturnCategory(int CategoryId) => Json(categoryService.GetCategoryById(CategoryId).Name);

        [Authorize(Roles = "Admin")]
        public IActionResult AcceptReport(int postId)
        {
            postService.RemoveAllReportsFromThisPost(postId);
            postService.RemovePostById(postId);
            return RedirectToAction("Reports");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RejectReport(int reportId)
        {
            postService.RemoveReportWithId(reportId);
            return RedirectToAction("Reports");
        }

        public IActionResult ReturnPostDetails(int id)
        {
            PostDTO post = postService.GetPostWithId(id);
            string JsonResultString = post.buyType.ToString() + " " + post.Company + " " + post.Model;
            return Json(JsonResultString);
        }
    }
}