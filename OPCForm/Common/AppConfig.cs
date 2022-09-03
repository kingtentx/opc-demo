using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCForm
{
    public class AppConfig
    {

#if DEBUG
        private static string path = Common.GetApplicationPath();
#else
        private static string path = AppDomain.CurrentDomain.BaseDirectory;
#endif
        public static string GetConfig
        {
            get
            {
                return path + "AppConfig/Config.xml";
            }
        }
    }
}
