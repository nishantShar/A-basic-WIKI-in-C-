﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CrossLinkDBEntities1 : DbContext
    {
        public CrossLinkDBEntities1()
            : base("name=CrossLinkDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Page_Details> Page_Details { get; set; }
        public virtual DbSet<Page_Content> Page_Content { get; set; }
        public virtual DbSet<User_Details> User_Details { get; set; }
        public virtual DbSet<Version> Versions { get; set; }
    }
}
