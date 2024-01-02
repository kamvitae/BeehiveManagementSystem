using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFIV_6_BeehiveManagementSystem
{
    interface IWorker
    {
        string Job { get; }

        void WorkNextShift();
    }
}
