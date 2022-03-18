using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedTime { get; set; }
        public string ImagePath { get; set; }
    }

    public static class PostService
    {
        public static List<Post> Posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                            Title = ".NET bo'yicha maqola yozish mavsumimiz yana boshlandi  ",
                            Body = "Agar kimdir kitob yozmoqchi bo'lsa Diyorbek Mamataliyev (http://t.me/diyorbek_mamataliyev) ga murojaat qilsin.",
                            CreatedTime = DateTime.Now,
                            ImagePath = "logo.jpg"
                        },
                new Post()
                {
                    Id = 2,
                    Title = ".NET bo'yicha maqola yozish mavsumimiz yana boshlandi  ",
                    Body = "Agar kimdir kitob yozmoqchi bo'lsa Diyorbek Mamataliyev (http://t.me/diyorbek_mamataliyev) ga murojaat qilsin.",
                    CreatedTime = DateTime.Now,
                    ImagePath = "photo_2021-11-27_00-43-16.jpg"
                },
                new Post()
                {
                    Id = 3,
                    Title = ".NET bo'yicha maqola yozish mavsumimiz yana boshlandi  ",
                    Body = "Agar kimdir kitob yozmoqchi bo'lsa Diyorbek Mamataliyev (http://t.me/diyorbek_mamataliyev) ga murojaat qilsin.",
                    CreatedTime = DateTime.Now,
                    ImagePath = "photo_2021-11-27_00-43-32.jpg"
                }
            };

        public static List<Post> GetAllPosts()
        {
            return Posts;
        }

        public static void AddPost(Post post)
        {
            Posts.Add(post);
        }

        public static Post GetPost(int id)
        {
            return Posts.FirstOrDefault(post => post.Id == id);
        }

        public static void DeletePost(int id)
        {
            Post post = Posts.FirstOrDefault(post => post.Id == id);
            Posts.Remove(post);
        }

        public static Post Update(Post post)
        {
            foreach (var p in Posts)
            {
                if (p.Id == post.Id)
                {
                    p.Id = post.Id;
                    p.Title = post.Title;
                    p.Body = post.Body;
                    p.CreatedTime = post.CreatedTime;
                    p.ImagePath = post.ImagePath;
                }
            }

            return post;
        }
    }
}
