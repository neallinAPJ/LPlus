
using Microsoft.AspNetCore.Mvc;
using Model.User;
using Server.Server.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LPlus.ViewComponents
{
    public class ContactListViewComponent:ViewComponent
    {
        private IUserServer _userServer { get; set; }
        public ContactListViewComponent(IUserServer userServer)
        {
            _userServer = userServer;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //string UserName = User.Identity.IsAuthenticated ? User.Identity.Name: string.Empty;
            string UserID = HttpContext.User.FindFirst("UserID").Value;
            //string UserName = User.Identities.First().IsAuthenticated ? User.Identities.First(u => u.IsAuthenticated).FindFirst(ClaimTypes.Name).Value : string.Empty;
            IEnumerable<UserModel> userList = new List<UserModel>();
            userList = await _userServer.GetContactList(Convert.ToInt32(UserID));
            return await Task.Run<IViewComponentResult>(() => { return View(userList); });
        }
    }
}
