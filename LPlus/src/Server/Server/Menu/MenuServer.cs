using Dapper;
using Model.Menu;
using Server.DataProvide;
using System.Collections.Generic;
using System.Linq;

namespace Server.Server.Menu
{
    public class MenuServer:IMenuServer
    {
        private IMySqlContext _context;
        public MenuServer(IMySqlContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 读取菜单信息
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetMenus()
        {
            IEnumerable<MenuModel> menus = _context.Connection.Query<MenuModel>("select * from Menu");
            List<MenuModel> menuList = menus.AsList<MenuModel>();
            List<MenuModel> siteMenu = menuList.Where(p => !p.ParentID.HasValue || (p.ParentID.HasValue && !menuList.Exists(m => m.ID == p.ParentID.Value))).ToList();
            siteMenu.ForEach(p => p.ChildrenMenu = GetChildMenu(p.ID, menuList));
            return siteMenu;
        }
        /// <summary>
        /// 读取子菜单信息
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="menuList"></param>
        /// <returns></returns>
        private List<MenuModel> GetChildMenu(int parentID, List<MenuModel> menuList)
        {
            List<MenuModel> siteMenu = null;
            if (menuList != null && menuList.Count > 0)
            {
                siteMenu = menuList.Where(p => p.ParentID.HasValue && p.ParentID.Value == parentID).ToList();
                siteMenu.ForEach(p => p.ChildrenMenu = GetChildMenu(p.ID, menuList));
            }
            return siteMenu;
        }
    }
}
