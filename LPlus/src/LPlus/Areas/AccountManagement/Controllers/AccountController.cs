using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Server.Server.User;
using Model.User;
using Common.Helper;

namespace LPlus.Controllers
{
    [Area("AccountManagement")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IUserServer _userServer;
        public AccountController(UserManager<UserModel> userManager, IUserServer userServer)
        {
            _userManager = userManager;
            _userServer = userServer;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            UserModel model = new UserModel();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                bool result = await _userServer.CheckPasswordAsync(model);
                if (result)
                {
                    var user = await _userManager.FindByNameAsync(model.Account);
                    if (user != null)
                    {
                        await UserHelper.SignInAsync(user, model.RememberMe, HttpContext);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

            }
            ModelState.AddModelError("Account", "Account is error or Password is error");
            ModelState.AddModelError("Email", "");
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            UserModel model = new UserModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel model)
        {
            ValidateModel(model);
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(model);
                if (result.Succeeded)
                {
                    await UserHelper.SignInAsync(model, model.RememberMe, HttpContext);
                    return RedirectToAction(nameof(HomeController.Index), "Home",new { area=""});
                }
                ModelState.AddModelError("", result.Errors.ToString());
            }
            return View(model);
        }


        private void ValidateModel(UserModel model)
        {
            if (!model.Password.Equals(model.EncryptPassword))
            {
                ModelState.AddModelError("", "输入的两次密码不相同。");
            }
        }

        public IActionResult Forbidden()
        {
            return View();
        }
        public async Task<IActionResult> SignOut()
        {
            await UserHelper.SignOutAsync(HttpContext);
            return Redirect("~/");
        }
        public IActionResult TestPage()
        {
            return View();
        }
    }
}