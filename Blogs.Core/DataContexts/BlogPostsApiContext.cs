using System;
using System.Collections.Generic;
using Blogs.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Core.DataContexts;

public partial class BlogPostsApiContext : DbContext
{
    public BlogPostsApiContext()
    {
    }

    public BlogPostsApiContext(DbContextOptions<BlogPostsApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BlogPost__3214EC071FC3A3F1");

            entity.ToTable("BlogPost");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreationUserNavigation).WithMany(p => p.BlogPostCreationUserNavigations)
                .HasForeignKey(d => d.CreationUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BlogPost__Creati__3B75D760");

            entity.HasOne(d => d.UpdateUserNavigation).WithMany(p => p.BlogPostUpdateUserNavigations)
                .HasForeignKey(d => d.UpdateUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BlogPost__Update__3C69FB99");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC072A9A297F");

            entity.ToTable("Comment");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.BlogPost).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BlogPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__BlogPos__440B1D61");

            entity.HasOne(d => d.CreationUserNavigation).WithMany(p => p.CommentCreationUserNavigations)
                .HasForeignKey(d => d.CreationUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__Creatio__44FF419A");

            entity.HasOne(d => d.UpdateUserNavigation).WithMany(p => p.CommentUpdateUserNavigations)
                .HasForeignKey(d => d.UpdateUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__UpdateU__45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC074F6B1840");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(32);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(16)
                .IsUnicode(false);

            entity.HasOne(d => d.CreationUserNavigation).WithMany(p => p.InverseCreationUserNavigation)
                .HasForeignKey(d => d.CreationUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__CreationU__37A5467C");

            entity.HasOne(d => d.UpdateUserNavigation).WithMany(p => p.InverseUpdateUserNavigation)
                .HasForeignKey(d => d.UpdateUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__UpdateUse__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
