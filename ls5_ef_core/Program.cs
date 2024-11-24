using ls5_ef_core.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ls5_ef_core
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var _blogs = GetBlogs();
            //using (var db = new BloggingEntities())
            //{
            //    foreach (var blog in _blogs)
            //    {
            //        db.Blogs.Add(blog);
            //    }
            //    db.SaveChanges();
            //}

            ////1.Створити новий блог під назвою "Моє життя в IT".
            using (var service = new ServiceBlog(new BloggingEntities()))
            {
                service.CreateBlog(new Blog { Name = "My life in IT" });
            }
            //2.Отримати список усіх блогів.
            using (var service = new ServiceBlog(new BloggingEntities()))
            {
                var blogs = service.ReadBlogs();
                foreach (var blog in blogs)
                {
                    Console.WriteLine(blog.Name);
                    foreach (var post in blog.Posts)
                    {
                        Console.WriteLine("\t" + post.Content);
                    }
                    Console.WriteLine();
                }
            }
            //3.Обновити назву блогу з ідентифікатором 1 на "Блог про програмування".
            using (var service = new ServiceBlog(new BloggingEntities()))
            {
                Blog blog = new Blog { Name = "Blog about programing" };
                service.UpdateBlogById(1, blog);
            }
            //4.Видалити блог із ідентифікатором 2.
            using (var service = new ServiceBlog(new BloggingEntities()))
            {
                service.DeleteBlogById(2);
            }
            Console.WriteLine("-------------------------------------------------------");
            using (var db = new BloggingEntities())
            {
                var blogs = db.Blogs.Include(b => b.Posts).ToList();
                foreach (var blog in blogs)
                {
                    Console.WriteLine(blog.BlogId + " " + blog.Name);
                    foreach (var post in blog.Posts)
                    {
                        Console.WriteLine("\t" + post.Content);
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
        public static List<Blog> GetBlogs()
        {
            return new List<Blog>
            {
                new Blog
                {
                    BlogId = 1,
                    Name = "ZX Spectrum Classics",
                    Posts = new List<Post>
                    {
                    new Post { PostId = 1, Title = "Manic Miner Review", Content = "A deep dive into the classic platformer." },
                    new Post { PostId = 2, Title = "Jet Set Willy Cheats", Content = "Secret tips and tricks for the legendary game." }
                    }
                },
                new Blog
                { 
                    BlogId = 2,
                    Name = "ZX Spectrum Programming",
                    Posts = new List<Post>
                    {
                        new Post { PostId = 3, Title = "Basic Programming Tutorial", Content = "Learn to code your own games." },
                        new Post { PostId = 4, Title = "Z80 Assembly Tips", Content = "Advanced tips for Z80 assembly language." }
                    }
                }
            };
        }
    }
}
