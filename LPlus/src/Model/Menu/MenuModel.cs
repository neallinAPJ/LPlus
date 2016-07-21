using System.Collections.Generic;

namespace Model.Menu
{
    public class MenuModel
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public string ResourceKey { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public bool IsShow { get; set; }
        public List<MenuModel> ChildrenMenu { get; set; }
        public string MenuIcon { get; set; }
        public bool IsActive { get; set; }
    }
}
