using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdiMenTestTask.Interfaces
{
    interface ParseForm
    {
        string Parse(string input, Encoding? encoding = null);
    }
}
