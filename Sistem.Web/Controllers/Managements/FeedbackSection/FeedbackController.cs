using Sistem.Service.CritiqK;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using static Sistem.Service.SystemHelpers.Enums;
/// <summary>
/// At this controller; Manager can view Customer Comments.
/// Onyl Unread Critics are displayed.
/// </summary>
namespace Sistem.Web.Controllers.Managements.FeedbackSection
{
    [Authorize(Roles = "Managements")]
    public class FeedbackController : Controller
    {
        private readonly ICritiq CriticService;
        public FeedbackController(ICritiq ICriticService)
        {
            CriticService = ICriticService;
        }
        public ActionResult Index(FeedBackType Type = FeedBackType.None)
        {
            if (Type == FeedBackType.None || Type == FeedBackType.General) return View("~/Views/Managements/FeedbackSection/FeedbackProcess/IndexGeneral.cshtml", CriticService.GetUnreadListForCommon());
            if (Type == FeedBackType.Menu) return View("~/Views/Managements/FeedbackSection/FeedbackProcess/IndexMenu.cshtml", CriticService.GetUnreadListForMenu());
            if (Type == FeedBackType.Order) return View("~/Views/Managements/FeedbackSection/FeedbackProcess/IndexOrder.cshtml", CriticService.GetUnreadListForOrder());
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpGet]
        public ActionResult DetailsAboutOrderFeedback(int? FeedbackId)
        {
            if (FeedbackId == null) return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            var allcriticsForOrder = CriticService.GetObjForOrderAndCriticsByCriticsId(FeedbackId.Value).FirstOrDefault();
            if (allcriticsForOrder == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return View("~/Views/Managements/FeedbackSection/FeedbackProcess/DetailsAboutOrderFeedback.cshtml", allcriticsForOrder);
        }
        [HttpGet]
        public ActionResult DetailsAboutGeneralFeedback(int? FeedbackId)
        {
            if (FeedbackId == null) return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            var allCriticsForOrder = CriticService.GetListForCommon().Where(b => b.IsSeen == false && b.GeneralCritiqId == FeedbackId.Value).FirstOrDefault();
            if (allCriticsForOrder == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return View("~/Views/Managements/FeedbackSection/FeedbackProcess/DetailsAboutGeneralFeedback.cshtml", allCriticsForOrder);
        }
        [HttpGet]
        public ActionResult DetailsAboutMenuFeedback(int? FeedbackId)
        {
            if (FeedbackId == null) return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            var allCriticsForOrder = CriticService.GetUnreadListForMenu().FirstOrDefault(b => b.CritiqMenuId == FeedbackId.Value);
            if (allCriticsForOrder == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return View("~/Views/Managements/FeedbackSection/FeedbackProcess/DetailsAboutMenuFeedback.cshtml", allCriticsForOrder);
        }
        [HttpPost]
        public ActionResult AnswerForOrderFeedback(int? FeedbackId, String AnswerForFeedBack)
        {
            if (FeedbackId == null || AnswerForFeedBack.Trim() == String.Empty) return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            var allCriticsForOrder = CriticService.GetObjForOrderCritsById(FeedbackId.Value);
            if (allCriticsForOrder == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            allCriticsForOrder.Answer = AnswerForFeedBack;
            allCriticsForOrder.IsSeen = true;
            CriticService.UpdateCritics(allCriticsForOrder);
            return RedirectToAction("Index", new { Type =FeedBackType.Order });
        }
        [HttpPost]
        public ActionResult AnswerForGeneralFeedback(int? FeedbackId, string AnswerForFeedBack)
        {
            if (FeedbackId == null || AnswerForFeedBack.Trim() == String.Empty) throw (new Exception("Missing Url Parameter."));
            var criticsForGeneral = CriticService.GetObjForCommonByCriticId(FeedbackId.Value);
            if (criticsForGeneral == null) throw (new Exception("Missing Url Parameter."));
            criticsForGeneral.AnswerText = AnswerForFeedBack;
            criticsForGeneral.IsSeen = true;
            CriticService.UpdateCommon(criticsForGeneral);
            return RedirectToAction("Index", new { Type = 1 });
        }
        [HttpPost]
        public ActionResult AnswerForMenuFeedback(int? FeedbackId, String AnswerForFeedBack)
        {
            if (FeedbackId == null || AnswerForFeedBack.Trim() == String.Empty) throw (new Exception("Missing Url Parameter."));
            var criticsForMenu = CriticService.GetObjForMenuByCriticId(FeedbackId.Value);
            if (criticsForMenu == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            criticsForMenu.Answer = AnswerForFeedBack;
            criticsForMenu.IsSeen = true;
            CriticService.UpdateCritics(criticsForMenu);
            return RedirectToAction("Index", new { Type = 2 });
        }
    }
}