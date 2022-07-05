using AventStack.ExtentReports;
using Dapper;
using NUnit.Framework;
using RestSharp;
using SeleniumNUnitExtentReport.Config;
using SeleniumNUnitExtentReport.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNUnitExtentReport.TestCases
{
    [TestFixture]
    class Registration : ExtentManager
    {
        ExtentReports rep = ExtentManager.Onetime();

        [Test]
        [Category("Verify Registration")]
        public void RegisterUser()
        {
            string id = "2001014800086";
            List<DecisionOptInOptOutData> registeredUserData = new List<DecisionOptInOptOutData>();

            SqlConnection db = new SqlConnection("Server=DEVDSQL01;Database=OptInOptOut;Trusted_Connection=true");
            {
                db.Open();

                var client = new RestClient("http://datapp32/DAConsentEngineAPI/DAConsentEngine/Registration");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json-patch+json");
                request.AddHeader("Accept", "application/json");

                var body = @"{
                               ""idNumber"": ""[[id]]"",
                               ""sourceSystem"": ""Sanlamlife"",
                               ""agentContext"": ""FRG""

                              }";

                body = body.Replace("[[id]]", id);


                request.AddParameter("application/json-patch+json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                registeredUserData = db.Query<DecisionOptInOptOutData>($"USE OptInOptOut SELECT TOP 1 R.* FROM Party P(NOLOCK) JOIN PartyCompany PC(NOLOCK) ON P.PArtyId = PC.PartyId JOIN Registrations R(NOLOCK) ON R.PartyCompanyId = PC.PArtyCompanyId WHERE R.Registered = 1 AND P.CustomerIdNumber = '2001014800086' AND PC.HoldingCompanyId = 1").ToList();

                string registered = registeredUserData[0].Registered;
                Assert.AreEqual(registered, "True");

            }
        }

        [Test]
        [Category("Verify Deregistration")]
        public void DeRegisterUser()
        {
            string id = "2001014800086";
            List<DecisionOptInOptOutData> decisionSimulatorData = new List<DecisionOptInOptOutData>();

            SqlConnection db = new SqlConnection("Server=DEVDSQL01;Database=OptInOptOut;Trusted_Connection=true");
            {
                db.Open();

                var client = new RestClient("http://datapp32/DAConsentEngineAPI/DAConsentEngine/DeRegistration");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json-patch+json");
                request.AddHeader("Accept", "application/json");

                var body = @"{
                               ""idNumber"": ""[[id]]"",
                               ""sourceSystem"": ""Sanlamlife"",
                               ""agentContext"": ""FRG""

                              }";

                body = body.Replace("[[id]]", id);


                request.AddParameter("application/json-patch+json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                decisionSimulatorData = db.Query<DecisionOptInOptOutData>($"USE OptInOptOut SELECT TOP 1 R.* FROM Party P(NOLOCK) JOIN PartyCompany PC(NOLOCK) ON P.PArtyId = PC.PartyId JOIN Registrations R(NOLOCK) ON R.PartyCompanyId = PC.PArtyCompanyId WHERE R.Registered = 1 AND P.CustomerIdNumber = '2001014800086' AND PC.HoldingCompanyId = 1").ToList();

                Assert.AreEqual(decisionSimulatorData.Count, 0);
                //string registered = decisionSimulatorData[0].Registered;
            }
        }
        
    }
}
