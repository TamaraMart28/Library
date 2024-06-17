using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryDatebaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryDatebaseImplement
{
    public class LibraryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=LibraryDatabase;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Admin> Admins { set; get; }
        public virtual DbSet<Book> Books { set; get; }
        public virtual DbSet<BookBranch> BookBranches { set; get; }
        public virtual DbSet<Branch> Branches { set; get; }
        public virtual DbSet<Librarian> Librarians { set; get; }
        public virtual DbSet<Reader> Readers { set; get; }
        public virtual DbSet<ReaderBook> ReaderBooks { set; get; }
        public virtual DbSet<FavoriteBooks> FavoriteBooks { set; get; }
    }

}
