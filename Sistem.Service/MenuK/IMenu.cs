using System;
using System.Collections.Generic;
using System.Linq;
using Sistem.Core.Data.MenuK;
namespace Sistem.Service.MenuK
{
    public interface IMenu
    {
        #region Menu/Ingredient
        IQueryable<Product> GetAllItems();
        Product GetItemsById(int setProductId);
        void InsertItem(Product setObj);
        void UpdateItem(Product setObj);
        void DeleteItem(Product setObj);
        #endregion
        #region Menu/Menu
        IQueryable<Menu> GetAllItem();
        /// <summary>
        /// Return selected menu
        /// </summary>
        /// <param name="setMenuId"></param>
        /// <returns></returns>
        Menu GetMenuById(int setMenuId);
        /// <summary>
        /// Return selected menu and it's ingredient
        /// </summary>
        /// <param name="setMenuId"></param>
        /// <returns></returns>
        IEnumerable<Menu> GetMenuAndItemsByMenuId(int setMenuId);
        /// <summary>
        /// All menus and their ingredients
        /// </summary>
        /// <param name="MenuId"></param>
        /// <returns></returns>
        IEnumerable<Menu> GetMenuAndItemsByMenuId();
        /// <summary>
        /// Return statistic for selected menu
        /// </summary>
        IEnumerable<Menu> GetMenuStatisticByMenuId(int setMenuId);
        void InsertMenu(Menu setObj);
        void UpdateMenu(Menu setObj);
        void DeleteMenu(Menu setObj);
        /// <summary>
        /// Insert selected product in Menu
        /// </summary>
        Boolean InsertProductInMenu(int setProductId, int setMenuId);
        #endregion
    }
}
