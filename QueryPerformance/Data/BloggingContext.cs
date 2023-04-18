using Microsoft.EntityFrameworkCore;
using QueryPerformance.Models;

namespace QueryPerformance.Data
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True");

        public static void SeedData(int numBlogs, int numPostsPerBlog)
        {
            using var context = new BloggingContext();
            context.AddRange(
                Enumerable.Range(0, numBlogs).Select(
                    _ => new Blog { Posts = Enumerable.Range(0, numPostsPerBlog).Select(_ => new Post()).ToList() }));
            context.SaveChanges();
        }
    }
}
