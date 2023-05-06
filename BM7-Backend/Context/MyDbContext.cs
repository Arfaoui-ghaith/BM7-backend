using System;
using System.Collections.Generic;
using BM7_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BM7_Backend.Context;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-chb13ee7avjcvo371e2g-a.frankfurt-postgres.render.com; Database=bm7_6icy; Username=bm7_6icy_user; Password=OZuuDnpBvDkw6O5KATdYtTSjOmv0KwJb");

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
