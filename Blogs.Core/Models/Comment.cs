using System;
using System.Collections.Generic;

namespace Blogs.Core.Models;

public partial class Comment
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;

    public Guid BlogPostId { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid CreationUser { get; set; }

    public DateTime UpdateDate { get; set; }

    public Guid UpdateUser { get; set; }

    public virtual BlogPost BlogPost { get; set; } = null!;

    public virtual User CreationUserNavigation { get; set; } = null!;

    public virtual User UpdateUserNavigation { get; set; } = null!;
}
