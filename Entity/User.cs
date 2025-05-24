using System;

namespace Blog.Entity;

public class User
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Title { get; set; }
    public DateTime Birthday { get; set; }
    public string? Email { get; set; }
    public string? Nick { get; set; }
    public string? Password { get; set; }
    public string? About { get; set; }
    public DateTime PublishedOn { get; set; }
    public bool Admin { get; set; }
    public bool Active { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<Question>? Questions { get; set; }

}
