using Business;
using DataAccess;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DependencyInjection
{
    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.LookForRegistries();
                    scan.AssemblyContainingType<BusinessRegistry>();
                    scan.AssemblyContainingType<DataAccessRegistry>();
                });
        }
    }
}
