
using Microsoft.AspNetCore.Mvc;
using Server.Server.Menu;
using System.Threading.Tasks;

namespace LPlus.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private IMenuServer _menuServer { get; set; }
        public MenuViewComponent(IMenuServer menuServer)
        {
            _menuServer = menuServer;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menus = _menuServer.GetMenus();
            return await Task.Run<IViewComponentResult>(() => { return View(menus); });
        }
    }
}
