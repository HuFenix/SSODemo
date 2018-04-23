using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using EntityFramework.DynamicFilters;
using DataEntity.EntityModel;

namespace DataEntity
{
    public  class SSoTestEntities : DbContext
    {
        private string _tenantId;
        public SSoTestEntities()
            : base("name=SSoTestEntities")
        {
            //_tenantId = tenant_id;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //throw new UnintentionalCodeFirstException();
            modelBuilder.Filter("ProductsFilter", (Products b, string tenantId) => (b.Tenant_id == tenantId), () => { return "47AB0C3994359CD1483BF348A87269F4"; });
        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<TenantsInfo> TenantsInfo { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
