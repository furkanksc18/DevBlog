using System;

namespace Blog.Entity;

public class Question
{
    public int QuestionId { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public string? QuestionName { get; set; }
    public string? QuestionInfo { get; set; }
    public DateTime PublishedOn { get; set; }
    public List<Comment>? Comments { get; set; }
}
