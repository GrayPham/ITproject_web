using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoDAO : PostContext
    {
        public int AddVideo(Video video)
        {
            try
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return video.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<VideoDTO> GetVideo()
        {
            List<Video> list = db.Videos.Where(x=>x.isDeleted==false).OrderBy(x=>x.AddDate).ToList();
            List<VideoDTO> listdto = new List<VideoDTO>();
            foreach(var item in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.Title = item.Title;
                dto.ID = item.ID;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.VideoPath = item.VideoPath;
                dto.AddDate = item.AddDate.Date;
                listdto.Add(dto);
            }
            return listdto;
        }

        public void UpdateVideo(VideoDTO model)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == model.ID);
                video.VideoPath = model.VideoPath;
                video.Title = model.Title;
                video.OriginalVideoPath = model.OriginalVideoPath;
                video.LastUpdateDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();   

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        public VideoDTO GetVideoWithID(int ID)
        {
            Video video = db.Videos.First(x => x.ID == ID);
            VideoDTO dto = new VideoDTO();
            dto.ID = video.ID;
            dto.OriginalVideoPath = video.OriginalVideoPath;
            dto.Title=video.Title;
            return dto;
        }
        public void DeleteVideo(int ID)
        {
            try
            {
                Video video = db.Videos.First(x => x.ID == ID);
                video.isDeleted = true;
                video.DeletedDate = DateTime.Now;
                video.LastUpdateUserID = UserStatic.UserID;
                video.LastUpdateDate = DateTime.Now;
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
