using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP
{
    internal class AuthStatus
    {
        public static bool InAuth { get; set; } = false;
        public static string Login { get; set; }
        public static string Status{ get; set; }
    }
}
