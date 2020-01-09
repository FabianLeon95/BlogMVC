using BlogMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name = "Título")]
        public string Title { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Contenido")]
        public string Content { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Creación")]
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Post() { }
        public Post(PostViewModel model, string filePath, string userId)
        {
            Id = model.Id;
            Title = model.Title;
            Description = model.Description;
            Content = model.Content;
            CategoryId = model.CategoryId;
            ImagePath = filePath;
            UserId = userId;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}