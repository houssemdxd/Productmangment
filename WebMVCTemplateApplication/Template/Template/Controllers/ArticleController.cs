using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Template.Models.BLL;
using Template.Models.Entities;

namespace Template.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListArticle = BLL_Article.GetAll();
            return View();
        }
        [HttpPost]
        public JsonResult AddArticle(Article article)
        {
            try
            {
                BLL_Article.Add(article);
                return Json(new { success = true, message = "Add Successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult UpdateArticle(int id,Article article)
        {
            try
            {
                BLL_Article.Update(id, article);
                return Json(new { success = true, message = "Update Successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteArticle(int id)
        {
            try
            {
                BLL_Article.Delete(id);
                return Json(new { success = true, message = "Delete Successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public Article GetArticle(int id)
        {
            return BLL_Article.GetArticle(id);

        }
        
    }
}
