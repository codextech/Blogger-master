using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogger.Models
{
    public class PostDetail
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Title is required")]

        public string Title { get; set; }


        [AllowHtml]
        [Required(ErrorMessage = "Post is required")]
        public string Post_Content { get; set; }
        [DisplayName("Create Time:")]

        public DateTime Create_time { get; set; }

        [Required(ErrorMessage = "Tag is required")]
        [DisplayName("Tags:")]
        public string Tages { get; set; }
        [Required(ErrorMessage = "Featured Image is required")]
        [DisplayName("Featured Image:")]
        public string FeaturedImage { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }


        public int CategoryId { get; set; }
        public virtual Category CategoryDetail { get; set; }
     

        public virtual IEnumerable<Category> CategoryDetails { get; set; }
        public PostDetail()
        {
            FeaturedImage = "~/Content/Images/default.png";
        }
    }
}