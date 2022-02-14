using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DTO
{
    public class FavDTO
    {
        public int ID { set; get; }
        [Required(ErrorMessage = " Please fill the title area")]
        public string Title { set; get; }
        public string Fav { set; get; }
        public string Logo { set; get; }
        [Display(Name ="Logo Image")]
        public HttpPostedFileBase LogoImage { set; get; }
        [Display(Name = "Fav Image")]
        public HttpPostedFileBase FavImage { set; get; }

    }
}
