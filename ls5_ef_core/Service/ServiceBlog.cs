using System;
using System.Collections.Generic;
using System.Linq;

namespace ls5_ef_core.Service
{
    public class ServiceBlog : IDisposable
    {
        public BloggingEntities _db {  get; set; }
        public ServiceBlog(BloggingEntities db) 
        {
            _db = db;
        }
        public void CreateBlog(Blog blog)
        {
            _db.Blogs.Add(blog);
            _db.SaveChanges();
        }
        public void DeleteBlogById(int id)
        {
            var blogToDelete = _db.Blogs.Find(id);
            if (blogToDelete != null)
            {
                foreach (var item in blogToDelete.Posts.ToList())
                {
                    blogToDelete.Posts.Remove(item);
                }
                _db.Blogs.Remove(blogToDelete);
                _db.SaveChanges();
            }
        }
        public void UpdateBlogById(int id, Blog blog)
        {
            var blogToUpdate = _db.Blogs.Find(id);
            if (blogToUpdate != null)
            {
                blogToUpdate.Name = blog.Name;
                blogToUpdate.Posts = blog.Posts;
                _db.SaveChanges();
            }
        }
        public ICollection<Blog> ReadBlogs()
        {
            return _db.Blogs.ToList();
        }
        public void AddPostToBlog(int blogId, Post post)
        {
            var blog = _db.Blogs.Find(blogId);
            if (blog != null)
            {
                blog.Posts.Add(post);
                _db.SaveChanges();
            }
        }
        public Blog FindBlogById(int id)
        {
            var blog = _db.Blogs.Find(id);
            return blog;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
