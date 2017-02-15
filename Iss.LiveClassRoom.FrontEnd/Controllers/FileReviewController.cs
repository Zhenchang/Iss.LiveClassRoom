using Iss.LiveClassRoom.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Iss.LiveClassRoom.FrontEnd.Controllers
{
    public class FileReviewController : BaseController
    {
        private IVideoService _service;
        public FileReviewController(IVideoService service)
        {
            _service = service;
        }
        // GET: FileReview
        public ActionResult Index()
        {            
            return View(_service.GetAll());
        }
        public ActionResult Accept(string videoId)
        {
            //invoke workflow
            fileuploadClient client = new fileuploadClient();
            client.ReceiveAcceptOrNot(true, videoId,GetLoggedInUserId(),"");
            return Redirect("/FileReview/");
        }

        [HttpPost]
        public ActionResult Reject(string videoId,string comment)
        {
            //invoke workflow
            fileuploadClient client = new fileuploadClient();
            client.ReceiveAcceptOrNot(false, videoId, GetLoggedInUserId(), comment);
            return Redirect("/FileReview/");
        }

        public ActionResult AddComment(string videoId)
        {
            //invoke workflow
            return View(_service.GetById(videoId).Result);
        }
    }
}