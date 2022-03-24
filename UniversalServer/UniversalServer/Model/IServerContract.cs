using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalServer.Model
{
    public interface IServerContract
    {
        event StatusChangedEventHandler StatusPropertyChanged;
        event MessageReceivedEventHandler MessageReceived;
        void Start();
    }
}
