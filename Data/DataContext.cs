using System;
using Blog.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<User> Users => Set<User>();
}
