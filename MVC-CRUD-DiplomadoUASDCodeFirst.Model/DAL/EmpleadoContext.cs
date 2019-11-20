using MVC_CRUD_DiplomadoUASDCodeFirst.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CRUD_DiplomadoUASDCodeFirst.Model.DAL
{
   public class EmpleadoContext : DbContext
    {
        public EmpleadoContext()
             : base("EmpleadoContext")
        {
        }

        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Registro> Registro { get; set; }
        public DbSet<Departamento> Departamento { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }

    }
}
