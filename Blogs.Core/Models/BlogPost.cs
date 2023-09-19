using System;
using System.Collections.Generic;

namespace Blogs.Core.Models;

public partial class BlogPost
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string HtmlContent { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public Guid CreationUser { get; set; }

    public DateTime UpdateDate { get; set; }

    public Guid UpdateUser { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User CreationUserNavigation { get; set; } = null!;

    public virtual User UpdateUserNavigation { get; set; } = null!;
}
