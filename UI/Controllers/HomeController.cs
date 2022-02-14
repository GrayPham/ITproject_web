using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        LayoutBLL layoutBll = new LayoutBLL();
        GeneralBLL bll = new GeneralBLL();
        PostBLL postbll = new PostBLL();
        ContactBLL contactbll = new ContactBLL();
        public ActionResult Index()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutBll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetAllPosts();
            return View(dto);
        }
        public ActionResult CategoryPostList(string CategoryName)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutBll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetCategoryPostList(CategoryName);
            return View(dto);
        }
        public ActionResult PostDetail(int ID)
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutBll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetPostDetailPageWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public ActionResult PostDetail(GeneralDTO model)
        {
            if(model.Name != null && model.Email!= null && model.Message != null)
            {
                if (postbll.AddComment(model))
                {
                    //Use Toastr
                    ViewData["CommentState"] = "Success";
                    ModelState.Clear();
                }
                else
                {
                    ViewData["CommentState"] = "Error";
                }
            }
            else
            {
                ViewData["CommentState"] = "Error";
                ModelState.Clear();
            }
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutBll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            model = bll.GetPostDetailPageWithID(model.PostID);
            
            return View(model);
        }


        [Route("contactus")]
        public ActionResult ContactUs()
        {
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutBll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();

            dto = bll.GetContactPageItems();
            return View(dto);
        }

        [Route("contactus")]
        [HttpPost]
        public ActionResult ContactUs(GeneralDTO model)
        {
            if(model.Name != null && model.Subject != null && model.Email!= null  && model.Message != null)
            {
                if (contactbll.AddContact(model))
                {
                    ViewData["CommentState"] = "Success";
                }
                else
                    ViewData["CommentState"] = "Error";
            }
            else
            {
                ViewData["CommentState"] = "Error";
            }
            HomeLayoutDTO layoutdto = new HomeLayoutDTO();
            layoutdto = layoutBll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutdto;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetContactPageItems();
            return View(dto);
        }

        [Route("search")]
        [HttpPost]
        public ActionResult Search(GeneralDTO model)
        {
            HomeLayoutDTO layoutDTO = new HomeLayoutDTO();
            layoutDTO = layoutBll.GetLayoutData();
            ViewData["LayoutDTO"] = layoutDTO;
            GeneralDTO dto = new GeneralDTO();
            dto = bll.GetSearchPosts(model.SearchText);

            return View(dto);
        }
    }
}