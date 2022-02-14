using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
    public class CategoryDAO : PostContext
    {
        public int AddCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return category.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        public List<CategoryDTO> GetCategoryList()
        {
            List<Category> listdao = db.Categories.Where(x => x.isDeleted==false).OrderBy(x=>x.AddDate).ToList();
            List<CategoryDTO> listCategory = new List<CategoryDTO>();
            foreach(var item in listdao)
            {
                CategoryDTO list = new CategoryDTO();
                list.ID = item.ID;
                list.CategoryName=item.CategoryName;
                listCategory.Add(list);
            }
            return listCategory;
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            IEnumerable<SelectListItem> categoryList = db.Categories.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = SqlFunctions.StringConvert((double)x.ID)
            }).ToList();
            return categoryList;
        }

        public List<Post> DeleteCategory(int ID)
        {
            try
            {
                Category ctg = db.Categories.First(x => x.ID == ID);
                ctg.isDeleted = true;
                ctg.LastUpdateUserID = UserStatic.UserID;
                ctg.LastUpdateDate = DateTime.Now;
                ctg.DeletedDate = DateTime.Now;
                db.SaveChanges();
                List<Post> postlist = db.Posts.Where(x => x.isDeleted == false && x.CategoryID == ID).ToList();
                return postlist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateCategory(CategoryDTO model)
        {
            Category dao = db.Categories.First(x => x.ID == model.ID);
            dao.CategoryName = model.CategoryName;
            db.SaveChanges();
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            Category dao = db.Categories.First(x => x.ID == ID);
            CategoryDTO category = new CategoryDTO();
            category.ID = dao.ID;
            category.CategoryName = dao.CategoryName;
            return category;    
        }
    }
}
