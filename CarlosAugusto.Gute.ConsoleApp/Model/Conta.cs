using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlosAugusto.Gute.ConsoleApp.Model
{
    public class Conta
    {
      public CrmServiceClient ServiceClient { get; set; }

        public string LogicalName { get; set; }

        public Conta(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "account";
        }

        public Guid Create()
        {
            Entity conta = new Entity(this.LogicalName);

            Console.WriteLine("1 - Por favor informe o nome da Conta: ");
            conta["name"] = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("2 - Digite o CNPJ: ");
            conta["dcp_cnpj1"] = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("3 - Escolha o tipo de relção (número): ");
            Console.WriteLine("1 - Concorrente");
            Console.WriteLine("2 - Consultor");
            Console.WriteLine("3 - Cliente");
            var ctcode = int.Parse(Console.ReadLine());
            conta["customertypecode"] = new OptionSetValue(ctcode);
            Console.WriteLine();

            Console.WriteLine("4 - Digite o Valor Total de Oportunidades: ");
            var dvt = decimal.Parse(Console.ReadLine());
            conta["dcp_valor_total"] = new Money(dvt);
            Console.WriteLine();

            Console.WriteLine("5 - Digite o ID do Contato Principal: (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)");
            var pcid = Console.ReadLine();
            conta["primarycontactid"] = new EntityReference("contact", new Guid(pcid));
            Console.WriteLine();

            
            //if(conta["name"] = Console.ReadLine())
            
                //Console.WriteLine("Conta já existe!"); não consegui desenvolver á parte de verificar se a conta já exite :(
            
            //else
            
            Guid accountId = this.ServiceClient.Create(conta);
            return accountId;
        }
    }
}
