using System;
using System.Collections.Generic;
using System.Linq;
using Sistem.Core.Data.CritiqK;
namespace Sistem.Service.CritiqK
{
    /// <summary>
    ///Designed for customer's critics progress like general critis, menu critis, order critics 
    /// </summary>
    public interface ICritiq
    {
        #region CritisForCommon
        /// <summary>
        /// Return Common Critisim as List
        /// </summary>
        /// <returns></returns>
        IQueryable<GeneralCritiq> GetListForCommon();
        /// <summary>
        /// Return common critisim for selected customerId
        /// </summary>
        IEnumerable<GeneralCritiq> GetObjForCommonByCustId(String setCustomerId);
        /// <summary>
        /// Return critisim that is unread by Restaurant.
        /// </summary>
        IEnumerable<GeneralCritiq> GetUnreadListForCommon();
        /// <summary>
        /// Return Common Critisim for selected CritisimId
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        GeneralCritiq GetObjForCommonByCriticId(int setId);
        void InsertCommon(GeneralCritiq setObj);
        void UpdateCommon(GeneralCritiq setObj);
        void DeleteCommon(GeneralCritiq setObj);
        #endregion
        #region CritisForMenu
        /// <summary>
        /// Return all critics for menu
        /// </summary>
        IQueryable<MenuCritiq> GetListForMenu();
        /// <summary>
        /// Return Menu Critics for given Id
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        MenuCritiq GetObjForMenuByCriticId(int setId);
        /// <summary>
        /// Get Menu critics from CustomerId
        /// </summary>
        IEnumerable<MenuCritiq> GetListForMenuByCustomerId(String setCustomerId);
        /// <summary>
        /// Get unread menu critics from Restaurant
        /// </summary>
        IEnumerable<MenuCritiq> GetUnreadListForMenu();
        void InsertCritics(MenuCritiq setObj);
        void UpdateCritics(MenuCritiq setObj);
        void DeleteCritics(MenuCritiq setObj);
        #endregion
        #region CriticsForOrder
        /// <summary>
        /// Return all critics for order
        /// </summary>
        /// <returns></returns>
        IQueryable<CritiqOrder> GetListForOrder();
        /// <summary>
        /// Return all unread order critis with order  
        /// </summary>
        IEnumerable<CritiqOrder> GetUnreadListForOrder();
        /// <summary>
        /// Return critis for given orderId as List
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        IEnumerable<CritiqOrder> GetObjForOrderAndCriticsByCriticsId(int setId);
        CritiqOrder GetObjForOrderCritsById(int setId);
        void InsertCritis(CritiqOrder setObj);
        void UpdateCritics(CritiqOrder setObj);
        void DeleteCritics(CritiqOrder setObj);
        #endregion
    }
}