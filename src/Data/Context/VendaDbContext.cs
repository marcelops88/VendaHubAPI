﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class VendaDbContext : DbContext
    {
        public VendaDbContext(DbContextOptions<VendaDbContext> options) : base(options)
        {
        }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
