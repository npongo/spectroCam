using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using spectro.camera.service.DataObjects;
using spectro.camera.service.Models;

namespace spectro.camera.service.Models
{
    public class spectroCamContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public spectroCamContext() : base(connectionStringName)
        {
        }

        public DbSet<project> projects { get; set; }

        public DbSet<spectra> spectras { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            modelBuilder.Entity<spectra>()
                .HasRequired(acs => acs.Project)
                .WithMany(cs => cs.spectra)
                .HasForeignKey(acs => new { acs.ProjectId })
                .WillCascadeOnDelete(false);
            
        }

    }

}
