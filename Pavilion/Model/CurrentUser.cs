using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pavilion.Model
{
    public static class CurrentUser
    {
        public static int employer_id { get; set; }
        public static string surname { get; set; }
        public static string name { get; set; }
        public static string middlename { get; set; }
        public static string login { get; set; }
        public static string password { get; set; }
        public static int post_id { get; set; }
        public static string phone { get; set; }
        public static string gender { get; set; }
        public static byte[] photo { get; set; }
    }
}
