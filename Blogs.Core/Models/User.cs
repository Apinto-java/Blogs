using System;
using System.Collections.Generic;

namespace Blogs.Core.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public Guid CreationUser { get; set; }

    public Guid UpdateUser { get; set; }

    public virtual ICollection<BlogPost> BlogPostCreationUserNavigations { get; set; } = new List<BlogPost>();

    public virtual ICollection<BlogPost> BlogPostUpdateUserNavigations { get; set; } = new List<BlogPost>();

    public virtual ICollection<Comment> CommentCreationUserNavigations { get; set; } = new List<Comment>();

    public virtual ICollection<Comment> CommentUpdateUserNavigations { get; set; } = new List<Comment>();

    public virtual User CreationUserNavigation { get; set; } = null!;

    public virtual ICollection<User> InverseCreationUserNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseUpdateUserNavigation { get; set; } = new List<User>();

    public virtual User UpdateUserNavigation { get; set; } = null!;
}
