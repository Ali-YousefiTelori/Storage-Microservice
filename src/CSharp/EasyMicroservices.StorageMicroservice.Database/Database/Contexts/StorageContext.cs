﻿using EasyMicroservices.Cores.Relational.EntityFrameworkCore;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using EasyMicroservices.StorageMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.StorageMicroservice.Database.Contexts
{
    public class StorageContext : RelationalCoreContext
    {
        public StorageContext(IEntityFrameworkCoreDatabaseBuilder builder) : base(builder)
        {
        }

        public DbSet<FileEntity> Files { get; set; }
        public DbSet<FolderEntity> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileEntity>(model =>
            {
                model.HasKey(r => r.Id);

                model.HasIndex(r => r.Key);

                model.HasOne(x => x.Folder)
                   .WithMany(x => x.Files)
                   .HasForeignKey(x => x.FolderId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FolderEntity>(model =>
            {
                model.HasKey(x => x.Id);
            });

            modelBuilder.Entity<FolderEntity>().HasData(
                new FolderEntity()
                {
                    Id = 1,
                    CreationDateTime = System.DateTime.Now,
                    Name = "Root",
                });
        }
    }
}