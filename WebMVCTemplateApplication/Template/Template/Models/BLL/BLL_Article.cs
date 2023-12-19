using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Models.DAL;
using Template.Models.Entities;

namespace Template.Models.BLL
{
    public class BLL_Article
    {
        public static void Add(Article article)
        {
            DAL_Article.Add(article);
        }

        public static void Update(int id, Article article)
        {
            DAL_Article.Update(id, article);
        }

        public static void Delete(int pId)
        {
            DAL_Article.Delete(pId);
        }
        public static Article GetArticle(int id)
        {
            return DAL_Article.SelectById(id);
        }
        public static List<Article> GetAll()
        {
            return DAL_Article.SelectAll();
        }
    }
}
