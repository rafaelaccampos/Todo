﻿using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Domain.Infra.Contexts
{
    public class TodoContext : DbContext
    {
        //Sempre sobrescreve o DbContext por boas práticas
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {

        }

        public TodoContext()
        {

        }

        public DbSet<TodoItem> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("Todo");
            modelBuilder.Entity<TodoItem>().Property(x => x.Id);
            modelBuilder.Entity<TodoItem>().Property(x => x.User).HasMaxLength(120).HasColumnType("varchar(120)");
            modelBuilder.Entity<TodoItem>().Property(x => x.Title).HasMaxLength(160).HasColumnType("varchar(120)");
            modelBuilder.Entity<TodoItem>().Property(x => x.Done).HasColumnType("bit");
            modelBuilder.Entity<TodoItem>().Property(x => x.Date);
            modelBuilder.Entity<TodoItem>().HasIndex(x => x.User);
        }
    }
}
