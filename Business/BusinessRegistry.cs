using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            //For<IBusinessLayer>().Use(() => new BusinessLayer());
            //For<IBusinessLayer>().Use<BusinessLayer>();

            //Scan(
            //    scan =>
            //    {
            //        scan.TheCallingAssembly();
            //    });
        }
    }
}
