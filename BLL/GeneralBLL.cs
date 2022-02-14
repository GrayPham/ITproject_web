using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL
{
    public class GeneralBLL
    {
        GeneralDAO dao = new GeneralDAO();
        AdsDAO adsdao = new AdsDAO();  
        AddressDAO addressdao = new AddressDAO();
        public GeneralDTO GetAllPosts()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPosts();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.PopularPost = dao.GetPopularPosts();
            dto.MostViewedPost = dao.GetMostViewedPosts();
            dto.Videos = dao.GetVideos();
            dto.Adslist = adsdao.GetAds();
            return dto;
        }
        public GeneralDTO GetContactPageItems()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            dto.Address = addressdao.GetAddresses().First();
            return dto;
        }
        public GeneralDTO GetPostDetailPageWithID(int ID)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            dto.PostDetail = dao.GetPostDetail(ID);

            return dto;
        }
        CategoryDAO categorydao = new CategoryDAO();
        public GeneralDTO GetCategoryPostList(string categoryName)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            if(categoryName == "video")
            {
                dto.Videos = dao.GetAllVideos();
                dto.CategoryName = "Video";

            }
            else
            {
                List<CategoryDTO> categorylist = categorydao.GetCategoryList();
                int CategoryID = 0;
                foreach(CategoryDTO item in categorylist)
                {
                    if(categoryName == SeoLink.GenerateUrl(item.CategoryName))
                    {
                        CategoryID = item.ID;
                        dto.CategoryName = item.CategoryName;
                        break;
                    }
                }
                dto.CategoryPostList = dao.GetCategoryPostList(CategoryID);
            }
            return dto;
        }
        
        public GeneralDTO GetSearchPosts(string searchText)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPosts();
            dto.Adslist = adsdao.GetAds();
            dto.CategoryPostList = dao.GetSearchPost(searchText);
            dto.SearchText = searchText;
            return dto;

        }
    }
}
