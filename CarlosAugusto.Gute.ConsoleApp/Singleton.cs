using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlosAugusto.Gute.ConsoleApp
{
    public class Singleton
    {

        public static CrmServiceClient GetService()
        {
            string url = "avaliacao1";
            string clientId = "bf494572-ef15-4338-93ed-f500837d04a4";
            string clientSecret = "rhR8Q~MstQ31Rja6gy5n~NI5Qvhfg7Qffgj0_ccq";

            CrmServiceClient serviceClient = new CrmServiceClient($"AuthType=ClientSecret;Url=https://{url}.crm2.dynamics.com/;AppId={clientId};ClientSecret={clientSecret};");

            if (!serviceClient.CurrentAccessToken.Equals(null))
                Console.WriteLine("Conexão Realizada com Sucesso!!!");
            else
                Console.WriteLine("Erro na Conexão");

            return serviceClient;
        }
    }
}
