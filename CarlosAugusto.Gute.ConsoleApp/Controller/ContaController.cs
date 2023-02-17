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
        public bool Update(Guid accountId, string telephone1) 
        {
            Conta conta = new Conta(this.ServiceClient);
            return Conta.Update(accountId, telephone1);

        }
        public bool Delete(Guid accountId)
        {
            Conta conta = new Conta(this.ServiceClient);
            return Conta.Delete(accountId);

        }

        public Entity GetAccountById(Guid id)
        {
            return Conta.GetAccountById(id);
        }
     }
}
