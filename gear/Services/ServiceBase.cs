using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Net.Messages;
using Gear.Net;


namespace Gear.Services
{
    public abstract class ServiceBase
    {
        private ConnectionListener listener;

        internal ServiceBase()
        {
            this.listener = new ConnectionListener(10000);
        }

        public void Run()
        {

        }
    }
}
