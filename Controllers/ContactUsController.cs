using System;
using Microsoft.AspNetCore.Mvc;
using aspCart.Web.Models.ContactUsViewModels;
using aspCart.Core.Domain.Messages;
using aspCart.Infrastructure.Services.Messages;
using Microsoft.AspNetCore.Identity;
using aspCart.Infrastructure.EFModels;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace aspCart.Web.Controllers
{
    public class ContactUsController : Controller
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IContactUsService _contactUsService;

        #endregion

        #region Constructor

        public ContactUsController(UserManager<ApplicationUser> userManager, IContactUsService contactUsService)
        {
            _userManager = userManager;
            _contactUsService = contactUsService;
        }

        #endregion

        #region Methods

        // GET: /ContactUs/
        public async Task<IActionResult> Index()
        {
            var user=await _userManager.GetUserAsync(HttpContext.User);
            var model = new ContactUsModel { Email=user?.Email, Name=user?.FirstName };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMessage(ContactUsModel model)
        {
            bool err = true;
            if(ModelState.IsValid)
            {
                var messageEntity = new ContactUsMessage
                {
                    Name = model.Name,
                    Email = model.Email,
                    Title = model.Title,
                    Message = model.Message,
                    Read = false,
                    SendDate = DateTime.Now
                };

                _contactUsService.InsertMessage(messageEntity);
                err = false;
            }

            TempData["ContactUsErr"] = err;
            return RedirectToAction("Index");
        }

        #endregion
    }
}
