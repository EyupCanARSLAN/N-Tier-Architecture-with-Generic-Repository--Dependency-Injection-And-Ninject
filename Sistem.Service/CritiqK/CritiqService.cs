using Sistem.Core.Data.CritiqK;
using Sistem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Sistem.Service.CritiqK
{
    /// <summary>
    ///Designed for customer's critics progress like general critis, menu critis, order critics 
    /// </summary>
    public class CritiqService : ICritiq
    {
        private readonly IRepository<GeneralCritiq> GeneralCriticsRepository;
        private readonly IRepository<MenuCritiq> CriticsForMenuRepository;
        private readonly IRepository<CritiqOrder> CriticsForOrder;
        public CritiqService(IRepository<GeneralCritiq> ElestiriGenelRepository, IRepository<MenuCritiq> ElestiriMenuRepository, IRepository<CritiqOrder> ElestiriSiparisRepository)
        {
            this.GeneralCriticsRepository = ElestiriGenelRepository;
            this.CriticsForMenuRepository = ElestiriMenuRepository;
            this.CriticsForOrder = ElestiriSiparisRepository;
        }
        #region Common Critisim
        /// <summary>
        /// Return Common Critisim as List
        /// </summary>
        /// <returns></returns>
        public IQueryable<GeneralCritiq> GetListForCommon()
        {
            return GeneralCriticsRepository.Table;
        }
        /// <summary>
        /// Return common critisim for selected customerId
        /// </summary>
        public IEnumerable<GeneralCritiq> GetObjForCommonByCustId(string setCustomerId)
        {
            return GeneralCriticsRepository.Get(filter: b => b.CustomerId == setCustomerId).ToList();
        }
        /// <summary>
        /// Return common critisim that is unread by Restaurant.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GeneralCritiq> GetUnreadListForCommon()
        {
            return GeneralCriticsRepository.Get(filter: b => b.IsSeen == false).ToList();
        }
        /// <summary>
        /// Return Common Critisim for selected CritisimId
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        public GeneralCritiq GetObjForCommonByCriticId(int setId)
        {
            return GeneralCriticsRepository.GetById(setId);
        }
        public void InsertCommon(GeneralCritiq setObj)
        {
            GeneralCriticsRepository.Insert(setObj);
        }
        public void UpdateCommon(GeneralCritiq setObj)
        {
            GeneralCriticsRepository.Update(setObj);
        }
        public void DeleteCommon(GeneralCritiq setObj)
        {
            GeneralCriticsRepository.Delete(setObj);
        }
        #endregion
        #region Critics On Menu
        /// <summary>
        /// Return all Critics for Menu
        /// </summary>
        public IQueryable<MenuCritiq> GetListForMenu()
        {
            return CriticsForMenuRepository.Table;
        }
        /// <summary>
        /// Return Menu Critics for given Id
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        public MenuCritiq GetObjForMenuByCriticId(int setId)
        {
            return CriticsForMenuRepository.GetById(setId);
        }
        /// <summary>
        /// Get Menu critics from CustomerId
        /// </summary>
        public IEnumerable<MenuCritiq> GetListForMenuByCustomerId(String setCustomerId)
        {
            return CriticsForMenuRepository.Get(filter: b => b.CustomerId == setCustomerId, includeProperties: "Menu").ToList();
        }
        /// <summary>
        /// Get unread menu critics from Restaurant
        /// </summary>
        public IEnumerable<MenuCritiq> GetUnreadListForMenu()
        {
            return CriticsForMenuRepository.Get(filter: b => b.IsSeen == false, includeProperties: "Menu").ToList();
        }
        public void InsertCritics(MenuCritiq setObj)
        {
            CriticsForMenuRepository.Insert(setObj);
        }
        public void UpdateCritics(MenuCritiq setObj)
        {
            CriticsForMenuRepository.Update(setObj);
        }
        public void DeleteCritics(MenuCritiq setObj)
        {
            CriticsForMenuRepository.Delete(setObj);
        }
        #endregion
        #region Critics On Order
        /// <summary>
        /// Return all critics for order
        /// </summary>
        /// <returns></returns>
        public IQueryable<CritiqOrder> GetListForOrder()
        {
            return CriticsForOrder.Table;
        }
        /// <summary>
        /// Return all unread order critis with order  
        /// </summary>
        public IEnumerable<CritiqOrder> GetUnreadListForOrder()
        {
            return CriticsForOrder.Get(filter: b => b.IsSeen == false, orderBy: q => q.OrderByDescending(b => b.CritiqOrderId), includeProperties: "Order").ToList();
        }
        /// <summary>
        /// Return critis for given orderId as List
        /// </summary>
        /// <param name="setId"></param>
        /// <returns></returns>
        public IEnumerable<CritiqOrder> GetObjForOrderAndCriticsByCriticsId(int setId)
        {
            return CriticsForOrder.Get(filter: b => b.IsSeen == false & b.CritiqOrderId == setId, includeProperties: "Order,Order.OrderDetail,Order.OrderDetail.Menu").ToList();
        }
        public CritiqOrder GetObjForOrderCritsById(int setId)
        {
            return CriticsForOrder.GetById(setId);
        }
        public void InsertCritis(CritiqOrder setObj)
        {
            CriticsForOrder.Insert(setObj);
        }
        public void UpdateCritics(CritiqOrder setObj)
        {
            CriticsForOrder.Update(setObj);
        }
        public void DeleteCritics(CritiqOrder setObj)
        {
            CriticsForOrder.Delete(setObj);
        }
        #endregion
    }
}