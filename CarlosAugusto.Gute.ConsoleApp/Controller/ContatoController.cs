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
    class ContatoController
    {
        public CrmServiceClient ServiceClient { get; set; }
        public Contato Contato { get; set; }

        public ContatoController(CrmServiceClient crmServiceClient) 
        { 
            ServiceClient= crmServiceClient;
            this.Contato = new Contato(ServiceClient);
        }
        public Guid Create()
        {
            return Contato.Create();
        }
     }
}
