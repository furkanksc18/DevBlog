using System;

namespace Blog.Entity;

public class Comment
{
    public int CommentId { get; set; }

    public int QuestionId { get; set; }
    public Question? Quession { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public string? CommentInfo { get; set; }
    public DateTime PublishedOn { get; set; }
}
