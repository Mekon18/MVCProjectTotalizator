using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Drawing;
using System.IO;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public Stream GetImage()
        {
            var ms = new MemoryStream();
            string apPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            var jpg = Image.FromFile(Path.Combine(apPath, "Ставки.jpg"));
            jpg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            
            ms.Position = 0;
            return ms;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
