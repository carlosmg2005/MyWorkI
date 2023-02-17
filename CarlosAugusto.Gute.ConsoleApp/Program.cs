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
            CreateUpdateDelete(contaController);
            //RetornarContas(contaController);

            Console.ReadKey();

        }

        private static void RetornarContas(ContaController contaController)
        {
            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("1 - Pesquisar uma conta por ID");
            Console.WriteLine("2 - Pesquisar uma conta por Nome");
            Console.WriteLine("3 - Pesquisar uma conta por nome do contato");

            var answer = Console.ReadLine();

            if (answer == "1")
            {
                Console.WriteLine("Qual id da conta você deseja pesquisar?");
                var accountId = Console.ReadLine();
                Entity account = contaController.GetAccountById(new Guid(accountId));
                Console.WriteLine($"A conta recuperada se chama {account["name"].ToString()} ");
            }
        }

        private static void CreateUpdateDelete(ContaController contaController)
        {
            Console.WriteLine("Digite 1 para Create/Update");
            Console.WriteLine("Digite 2 para Delete");

            var answerWhatToDo = Console.ReadLine();

            if (answerWhatToDo.ToString() == "1")
            {
                MakeCreateAndUpdate(contaController);
            }
            else
            {
                if (answerWhatToDo.ToString() == "2")
                {
                    MakeDelete(contaController);
                }
                else
                {
                    Console.WriteLine("Opção Inválida, Reinicie o App");
                }
            }
        }

        private static void MakeDelete(ContaController contaController)
        {
            Console.WriteLine("Digite o ID da conta que você quer deletar");
            var accountId = Console.ReadLine();
            contaController.Delete(new Guid(accountId));
            Console.WriteLine("Conta Excluída com Sucesso!");
        }

        private static void MakeCreateAndUpdate(ContaController contaController)
        {
            Console.WriteLine("Aguarde enquanto a nova conta é criada");
            Guid accountId = contaController.Create();              //pega o Id da conta
            Console.WriteLine("Conta Criada com Sucesso");

            Console.WriteLine($@"https://avaliacao1.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&forceUCI=1&pagetype=entityrecord&etn=account&id={accountId}"); //passa o ID pro link

            Console.WriteLine("Deseja atualizar a conta recém criada? (S/N)");
            var answerToUpdate = Console.ReadLine();

            if (answerToUpdate.ToString() == "S")
            {
                Console.WriteLine("Por favor informe o novo telefone");
                var newTelephone = Console.ReadLine();
                bool contaAtualizada = contaController.Update(accountId, newTelephone);      //nos argumentos, garante que a conta a ser atualizada está correta

                if (contaAtualizada)
                    Console.WriteLine("Conta Atualizada com Sucesso!");
                else
                    Console.WriteLine("Erro na atualizção da conta");
            }
        }
    }
}
