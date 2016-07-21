
using Model.Menu;
using System.Collections.Generic;

namespace Server.Server.Menu
{
    public interface IMenuServer
    {
        List<MenuModel> GetMenus();
    }
}
