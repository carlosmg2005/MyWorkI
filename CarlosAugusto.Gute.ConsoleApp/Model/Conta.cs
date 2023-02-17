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
            conta["name"] = "Volkswagen";
            conta["telephone1"] = "(32) 8461-9977";
            conta["fax"] = "(32) 3755-1269";

            //conta["dcp_nmr_total_opp"] = 0;
            //conta["dcp_tipoderelacao"] = new OptionSetValue(775050000);
            //conta["dcp_valor_total_opp"] = new Money(0);
            conta["primarycontactid"] = new EntityReference("contact", new Guid("79ae8582-84bb-ea11-a812-000d3a8b3ec6"));

            Guid accountId = this.ServiceClient.Create(conta);
            return accountId;
        }
        public bool Update(Guid accountId, string telephone1)
        {
            try
            {
                Entity conta = new Entity(this.LogicalName, accountId);
                conta["telephone1"] = telephone1;
                this.ServiceClient.Update(conta);
                return true;
            } 
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Delete(Guid accountId)
        {
            try
            {
                this.ServiceClient.Delete(this.LogicalName, accountId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public Entity GetAccountByName(String name)
        {
            QueryExpression queryAccount = new QueryExpression(this.LogicalName);
            queryAccount.ColumnSet.AddColumns("telephone1", "primarycontactid");
            queryAccount.Criteria.AddCondition("name", ConditionOperator.Equal, name);
            return RetrieveOneAccount(queryAccount);
        }

        private Entity RetrieveOneAccount(QueryExpression queryAccount)
        {
            EntityCollection accounts = this.ServiceClient.RetrieveMultiple(queryAccount);

            if (accounts.Entities.Count() > 0)

                return accounts.Entities.FirstOrDefault();
            else
                Console.WriteLine("Nenhuma conta encontrada com esse nome");

            return null;
        }

        public Entity GetAccountByContactName(String contactName, string[] columns)
        {
            QueryExpression queryAccount = new QueryExpression(this.LogicalName);
            queryAccount.ColumnSet.AddColumns(columns);
            queryAccount.AddLink("contact", "primarycontactid", "contactid"); //contact = tabela / primaryc. = tabela / contactid = coluna --- em branco inner join
            queryAccount.LinkEntities.FirstOrDefault().LinkCriteria.AddCondition("fullname", ConditionOperator.Equal, contactName);

            return RetrieveOneAccount(queryAccount);
        }

        public Entity GetAccountById(Guid id)
        {
            var context = new OrganizationServiceContext(this.ServiceClient);

            return (from a in context.CreateQuery("account")
                    join b in context.CreateQuery("contact")
                    on ((EntityReference)a["primarycontactid"]).Id equals b["contactid"]
                    where (Guid)a["accountid"] == id
                    select a).ToList().FirstOrDefault();
        }

        /*public Entity GetAccountByTelephone(String telephone1) 
        {
            string fetchXML = "<fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" distinct=\"false\">\r\n  <entity name=\"account\">\r\n    <attribute name=\"name\" />\r\n    <attribute name=\"primarycontactid\" />\r\n    <attribute name=\"telephone1\" />\r\n    <attribute name=\"accountid\" />\r\n    <order attribute=\"name\" descending=\"false\" />\r\n    <filter type=\"and\">\r\n      <condition attribute=\"telephone1\" operator=\"eq\" value=\"número de telefone\" />\r\n    </filter>\r\n    <link-entity name=\"contact\" from=\"contactid\" to=\"primarycontactid\" link-type=\"inner\" alias=\"ab\">\r\n      <filter type=\"and\">\r\n        <condition attribute=\"firstname\" operator=\"eq\" value=\"Nome do Contato\" />\r\n      </filter>\r\n    </link-entity>\r\n  </entity>\r\n</fetch>";
        
            return this.ServiceClient.RetrieveMultiple(
                new.FetchExpression(fetchXML)
                ).Entities.FirstOrDefault();
        */
    }
}
