using CarlosAugusto.Gute.ConsoleApp.Controller;
using CarlosAugusto.Gute.ConsoleApp.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CarlosAugusto.Gute.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CrmServiceClient serviceClient = Singleton.GetService(); //faz a conexão

            ContaController contaController = new ContaController(serviceClient);
            Create(contaController);

            ContatoController contatoController = new ContatoController(serviceClient);
            CreateContato(contatoController);

            Console.ReadKey();

        }

        private static void Create(ContaController contaController)
        {
                MakeCreate(contaController);            
        }

        private static void CreateContato(ContatoController contatoController)
        {
            Console.WriteLine("\nDeseja Criar um Contato para esta Conta? (S/N)");
            var opc = Console.ReadLine();

            if (opc.ToString() == "S")
            {
                MakeCreateContato(contatoController);
            }
            else
            {
                Console.WriteLine("\n\nSaindo...");
            }
        }

        private static void MakeCreate(ContaController contaController)
        {
            //Console.WriteLine("\nAguarde enquanto a nova conta é criada\n");
            Guid accountId = contaController.Create();              //pega o Id da conta
            Console.WriteLine("\nConta Criada com Sucesso\n");

            Console.WriteLine($@"https://avaliacao1.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&forceUCI=1&pagetype=entityrecord&etn=account&id={accountId}"); //passa o ID pro link
        }

        private static void MakeCreateContato(ContatoController contatoController)
        {
            Console.WriteLine("\nAguarde enquanto o novo contato é criado\n");
            Guid contactId = contatoController.Create();              //pega o Id do contato
            Console.WriteLine("\nContato Criado com Sucesso\n");

            Console.WriteLine($@"https://avaliacao1.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&forceUCI=1&pagetype=entityrecord&etn=contact&id={contactId}"); //passa o ID pro link
        }
    }
}
