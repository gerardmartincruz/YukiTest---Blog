using Microsoft.EntityFrameworkCore;
using Blog.Entities;

namespace Blog.Context
{
    public class BlogContext : DbContext
    {

        public BlogContext(){}

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Author> Author { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Post");

                entity.ToTable("Post");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Title).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Content).HasMaxLength(int.MaxValue);
                entity.Property(e => e.AuthorId).HasMaxLength(450);

                entity.HasOne(d => d.Author).WithOne(p => p.Post).HasForeignKey<Author>(b => b.Id);  
            });
            
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Author");

                entity.ToTable("Author");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(e => e.Post).WithOne(e => e.Author).HasForeignKey<Author>(a => a.Id);
            });

        }

        public void InitializeData()
        {
            for (int i = 1; i < 100; i++)
            {
                Author.Add(new Author() { Id = 100 + i, Name = $"AuthorName{i}", Surname = $"AuthosSurname{i}" });
            }
            SaveChanges();
            for (int i = 1; i < 100; i++)
            {
                Post.Add(new Post { Id = i, Content = $"Content number{i}", Description = $"Description number {i}", Title = $"Title number {i}", AuthorId = 100 + i });
            }
            SaveChanges();
        }

    }
}