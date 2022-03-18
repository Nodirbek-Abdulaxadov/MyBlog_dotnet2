using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        
        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View(PostService.GetAllPosts());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueName = String.Empty;
                if (viewModel.ImageFile is not null)
                {
                    string uploadFolder = Path.Combine(_environment.WebRootPath, "Images");
                    uniqueName = Guid.NewGuid().ToString() + viewModel.ImageFile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    viewModel.ImageFile.CopyTo(fileStream);
                    fileStream.Close();
                }

                Post newPost = new Post()
                {
                    Id = PostService.GetAllPosts().Count + 1,
                    Title = viewModel.Title,
                    Body = viewModel.Body,
                    CreatedTime = DateTime.Now,
                    ImagePath = uniqueName
                };

                PostService.AddPost(newPost);

                return RedirectToAction("index");
            }


            return View();
        }

        public IActionResult Detail(int id)
        {
            return View(PostService.GetPost(id));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Post post = PostService.GetPost(id);
            EditPostViewModel viewModel = new EditPostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                CreatedTime = post.CreatedTime,
                ImagePath = post.ImagePath,
                NewImage = null
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(EditPostViewModel viewModel)
        {
            Post post = new Post()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Body = viewModel.Body,
                CreatedTime = viewModel.CreatedTime,
                ImagePath = viewModel.ImagePath
            };

            if (viewModel.NewImage is not null)
            {
                string uplodFolder = Path.Combine(_environment.WebRootPath, "Images");
                string filePath = Path.Combine(uplodFolder, PostService.GetPost(viewModel.Id).ImagePath);
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }

                string uploadFolder = Path.Combine(_environment.WebRootPath, "Images");
                post.ImagePath = Guid.NewGuid().ToString() + viewModel.NewImage.FileName;
                filePath = Path.Combine(uploadFolder, post.ImagePath);
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                viewModel.NewImage.CopyTo(fileStream);
                fileStream.Close();
            }

            PostService.Update(post);

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            string uplodFolder = Path.Combine(_environment.WebRootPath, "Images");
            string filePath = Path.Combine(uplodFolder, PostService.GetPost(id).ImagePath);
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            PostService.DeletePost(id);
            return RedirectToAction("index");
        }
    }
}
