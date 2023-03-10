using CarlosAugusto.Gute.ConsoleApp.Model;
using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlosAugusto.Gute.ConsoleApp.Controller
{
    class ContaController
    {
        public CrmServiceClient ServiceClient { get; set; }
        public Conta Conta { get; set; }

        public ContaController(CrmServiceClient crmServiceClient) 
        { 
            ServiceClient= crmServiceClient;
            this.Conta = new Conta(ServiceClient);
        }
        public Guid Create()
        {
            return Conta.Create();
        }

     }
}
