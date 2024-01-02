using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFIV_6_BeehiveManagementSystem
{
    interface IDefender : IWorker
    {
        void DefendHive();
    }
}
