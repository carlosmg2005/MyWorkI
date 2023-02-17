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
    public class Contato
    {
      public CrmServiceClient ServiceClient { get; set; }

        public string LogicalName { get; set; }

        public Contato(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "contact";
        }

        public Guid Create()
        {
            Entity contato = new Entity(this.LogicalName);

            Console.WriteLine("1 - Por favor informe o nome do Contato: ");
            contato["fullname"] = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("2 - Digite o CPF: ");
            contato["dcp_cpf"] = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("3 - Escolha o Estado Civil (número): ");
            Console.WriteLine("1 - Solteiro");
            Console.WriteLine("2 - Casado");
            var fscode = int.Parse(Console.ReadLine());
            contato["familystatuscode"] = new OptionSetValue(fscode);
            Console.WriteLine();

            Console.WriteLine("4 - Digite o Limite de Crédito: ");
            var clb = decimal.Parse(Console.ReadLine());
            contato["creditlimit_base"] = new Money(clb);
            Console.WriteLine();

            Console.WriteLine("5 - Digite o Telefone Principal");
            var pcid = Console.ReadLine();
            contato["telephone1"] = Console.ReadLine();
            Console.WriteLine();
            
            Console.ReadKey();

            Guid contactId = this.ServiceClient.Create(contato);
            return contactId;
        }
    }
}
