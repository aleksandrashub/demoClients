using clients.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clients
{
    public static class Helper
    {
        public static readonly MyprojContext User724Context = new MyprojContext();
        public static int editClInd;
        public static List<DateTime> visits = new List<DateTime>();
    }
}
