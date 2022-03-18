using Microsoft.AspNetCore.Http;
using System;

namespace MyBlog.ViewModels
{
    public class EditPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedTime { get; set; }
        public string ImagePath { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
