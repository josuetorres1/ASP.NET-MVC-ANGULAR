using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AngularJSProofofConcept.Entities;

namespace AngularJSProofofConcept.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<EventCountry> Products { get; set; }
        public DbSet<Message> Messages { get; set; }
    }

    public class EFProductRepository 
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<EventCountry> Products
        {
            get { return context.Products; }
        }

        public IQueryable<Message> Messages
        {
            get { return context.Messages; }
        }
    }
}