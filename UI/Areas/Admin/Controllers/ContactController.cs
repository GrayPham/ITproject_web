using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        ContactBLL bll = new ContactBLL();
        // GET: Admin/Contact
        public ActionResult UnreadMessages()
        {
            List<ContactDTO> list = new List<ContactDTO>();
            list = bll.GetUpreadMessages();
            return View(list);
        }
        public ActionResult AllMessages()
        {
            List<ContactDTO> list = new List<ContactDTO>();
            list = bll.GetAllMessages();
            return View(list);
        }
        public ActionResult ReadMessage(int ID)
        {
            bll.ReadMessage(ID);
            return RedirectToAction("UnreadMessages");
        }
        public ActionResult ReadMessage2(int ID)
        {
            bll.ReadMessage(ID);
            return RedirectToAction("AllMessages");
        }
        public JsonResult DeleteMessage(int ID)
        {
            bll.DeleteMessage(ID);
            return Json("");    
        }
    }
}