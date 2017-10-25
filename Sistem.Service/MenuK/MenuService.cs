using Sistem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Sistem.Core.Data.MenuK;
using Sistem.Core.Data.StatisticK;
namespace Sistem.Service.MenuK
{
    public class MenuService : IMenu
    {
        private readonly IRepository<Menu> MenuRepository;
        private readonly IRepository<Product> MenuItemRepository;
        private readonly IRepository<MenuSalesStatistic> StatisticRepository;
        public MenuService(IRepository<Menu> MenuRepository, IRepository<Product> MenuItemRepository, IRepository<MenuSalesStatistic> StatisticRepository)
        {
            this.MenuRepository = MenuRepository;
            this.MenuItemRepository = MenuItemRepository;
            this.StatisticRepository = StatisticRepository;
        }
        #region Executer Functions
        #region Product
        public IQueryable<Product> GetAllItems()
        {
            return MenuItemRepository.Table;
        }
        public Product GetItemsById(int setProductId)
        {
            return MenuItemRepository.GetById(setProductId);
        }
        public void InsertItem(Product setObj)
        {
            MenuItemRepository.Insert(setObj);
        }
        public void UpdateItem(Product setObj)
        {
            MenuItemRepository.Update(setObj);
        }
        public void DeleteItem(Product setObj)
        {
            MenuItemRepository.Delete(setObj);
        }
        #endregion
        #region Menu
        public IQueryable<Menu> GetAllItem()
        {
            return MenuRepository.Table;
        }
        /// <summary>
        /// Return selected menu
        /// </summary>
        /// <param name="setMenuId"></param>
        /// <returns></returns>
        public Menu GetMenuById(int setMenuId)
        {
            return MenuRepository.GetById(setMenuId);
        }
        /// <summary>
        /// All menus and their ingredients
        /// </summary>
        /// <param name="MenuId"></param>
        /// <returns></returns>
        public IEnumerable<Menu> GetMenuAndItemsByMenuId()
        {
            return MenuRepository.Get(includeProperties: "Product");
        }
        /// <summary>
        /// Return selected menu and it's ingredient
        /// </summary>
        /// <param name="setMenuId"></param>
        /// <returns></returns>
        public IEnumerable<Menu> GetMenuAndItemsByMenuId(int setMenuId)
        {
            return MenuRepository.Get(filter: b => b.MenuId == setMenuId, includeProperties: "Product");
        }
        /// <summary>
        /// Return statistic for selected menu
        /// </summary>
        public IEnumerable<Menu> GetMenuStatisticByMenuId(int setMenuId)
        {
            return MenuRepository.Get(filter: b => b.MenuId == setMenuId, includeProperties: "Product,MenuSalesStatistic,MenuCritiq");
        }
        /// <summary>
        /// Create a new menu and create a new statistic record that is releted with menu.
        /// </summary>
        public void InsertMenu(Menu setObj)
        {
            MenuRepository.Insert(setObj);
            var createNewStatistic = createMenuSalesStatistic(setObj);
            setObj.MenuSalesStatistic.Add(createNewStatistic);
            MenuRepository.Update(setObj);
        }
        public void UpdateMenu(Menu setObj)
        {
            //Create a new record with UniqueKey(Month, Year, PriceOfProduct)
            var CheckRecordForThisUpdate = StatisticRepository.Get(filter: b => b.SalesMonth == DateTime.Now.Month && b.SalesYear == DateTime.Now.Year && b.SalesUniquePrice == setObj.Price && b.MenuId == setObj.MenuId).FirstOrDefault();
            #region Generate New Istatistik Info and Add Table
            if (CheckRecordForThisUpdate == null)
            {
                var createNewStatistic = createMenuSalesStatistic(setObj);
                setObj.MenuSalesStatistic.Add(createNewStatistic);
            }
            #endregion
            MenuRepository.Update(setObj);
        }
        public void DeleteMenu(Menu setObj)
        {
            MenuRepository.Delete(setObj);
        }
        public Boolean InsertProductInMenu(int setProductId, int setMenuId)
        {
            var findProduct = GetItemsById(setProductId);
            var findMenu = GetMenuAndItemsByMenuId(setMenuId).First();
            if (isMenuContainsThisProduct(findMenu, findProduct)) return false;
            findMenu.Product.Add(findProduct);
            UpdateMenu(findMenu);
            return true;
        }
        #endregion
        #endregion
        #region Private Functions
        private MenuSalesStatistic createMenuSalesStatistic(Menu setObj)
        {
            MenuSalesStatistic addNewStatistic = new MenuSalesStatistic();
            addNewStatistic.MenuId = setObj.MenuId;
            addNewStatistic.Vote = 0;
            addNewStatistic.SalesCount = 0;
            addNewStatistic.SalesYear = DateTime.Now.Year;
            addNewStatistic.SalesMonth = DateTime.Now.Month;
            addNewStatistic.SalesUniquePrice = setObj.Price;
            StatisticRepository.Insert(addNewStatistic);
            return addNewStatistic;
        }
        private Boolean isMenuContainsThisProduct(Menu setMenu, Product setProduct)
        {
            return setMenu.Product.Contains(setProduct);
        }
        #endregion
    }
}