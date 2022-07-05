using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumNUnitExtentReport.Config;
using SeleniumNUnitExtentReport.Data;
using SeleniumNUnitExtentReport.PageMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json;
using OpenQA.Selenium.Support.UI;

namespace SeleniumNUnitExtentReport.TestCases
{
    [TestFixture]
    public class VerifyConsent : ExtentManager
    {

        ExtentReports rep = ExtentManager.Onetime();
        Verify_PartyConsent_NewID newID;

        [Test]
        [Category("Consent")]
        public void Verify_Consent()
        {
            newID = new Verify_PartyConsent_NewID(GetDriver());
            string idnumber = newID.GetNewId(5, 2, 3);

            Registration reguser = new Registration();
            reguser.RegisterUser();

        }

        [Test]
        [Category("Verify Consent")]
        public void Verify_AcceptanceStatus_OfCustomer()
        {

            List<DecisionOptInOptOutData> decisionSimulatorData = new List<DecisionOptInOptOutData>();

            SqlConnection db = new SqlConnection("Server=DEVDSQL01;Database=OptInOptOut;Trusted_Connection=true");

            {
                db.Open();

                decisionSimulatorData = db.Query<DecisionOptInOptOutData>($"USE OptInOptOut SELECT TOP 100 P.CustomerIdNumber, P.PartyId, RB.Value AS Brand, PB.BrandId, RP.Code AS Product, RC.Code AS Channel, BC.AcceptanceStatus, BC.AcceptanceStatusType, BC.ModifiedDate, p.SourceSystem, pc.SourceSystem, pc.CustomerType, p.CreatedDate FROM BrandChannel(NOLOCK) BC JOIN ProductBrand(NOLOCK) PB on PB.PartyProductId = BC.PartyProductId join PartyProductInformation(NOLOCK) PPI on PPI.PartyInformationId = PB.PartyInformationId join Party(NOLOCK) P on P.PartyId = PPI.partyid JOIN dbo.PartyCompany(NOLOCK) pc ON pc.PartyId = P.PartyId Join Reference.Channel(NOLOCK) RC on RC.ChannelId = BC.ChannelId join Reference.Brand(NOLOCK) RB on RB.BrandId = PB.BrandId Join Reference.Product(NOLOCK) RP on RP.ProductId = PPI.ProductId where P.CustomerIdNumber = '1908025023006' ORDER BY Brand, Product, Channel").ToList();


                decisionSimulatorData = db.Query<DecisionOptInOptOutData>($"SELECT TOP (1000) [partyid],[idno],[idtype],[firstid],[partycontextdescr],[marketingoptoutstatus],[activestatusflag],[LOADDATE],[updatedtimestamp],[ConsentParty_id]FROM[ConsentManagementSourceData].[dbo].[consent_party_HIVE]WHERE partyid = 309418").ToList();

                decisionSimulatorData = db.Query<DecisionOptInOptOutData>($" UPDATE A SET A.daemail = 3, A.wesemail = 1, a.fnbphone = 2, a.daphone = 2, a.dasms = 2, A.updatedtimestamp = GETDATE() FROM[ConsentManagementSourceData].[dbo].[consent_marketing_HIVE_RawFile] A WHERE A.partyid = 309418").ToList();

                var affectedRows = db.Execute($"USE OptInOptOut");
                var procedure = "dbo.CM_NeedLoad";
                var values = new { @iVar_testPack = 0 };
                var results = db.Query(procedure, null, commandTimeout: 1000, commandType: CommandType.StoredProcedure).ToList();

                decisionSimulatorData = db.Query<DecisionOptInOptOutData>($"USE OptInOptOut SELECT TOP 100 P.CustomerIdNumber, P.PartyId, RB.Value AS Brand, PB.BrandId, RP.Code AS Product, RC.Code AS Channel, BC.AcceptanceStatus, BC.AcceptanceStatusType, BC.ModifiedDate, p.SourceSystem, pc.SourceSystem, pc.CustomerType, p.CreatedDate FROM BrandChannel(NOLOCK) BC JOIN ProductBrand(NOLOCK) PB on PB.PartyProductId = BC.PartyProductId join PartyProductInformation(NOLOCK) PPI on PPI.PartyInformationId = PB.PartyInformationId join Party(NOLOCK) P on P.PartyId = PPI.partyid JOIN dbo.PartyCompany(NOLOCK) pc ON pc.PartyId = P.PartyId Join Reference.Channel(NOLOCK) RC on RC.ChannelId = BC.ChannelId join Reference.Brand(NOLOCK) RB on RB.BrandId = PB.BrandId Join Reference.Product(NOLOCK) RP on RP.ProductId = PPI.ProductId where P.CustomerIdNumber = '2202075043000' ORDER BY Brand, Product, Channel").ToList();

                Thread.Sleep(10000);

                for (int i = 0; i <= 5; i++)
                {
                    decisionSimulatorData = db.Query<DecisionOptInOptOutData>(@"USE OptInOptOut SELECT TOP 100 P.CustomerIdNumber,                        
P.PartyId, RB.Value AS Brand, PB.BrandId, RP.Code AS Product, 
RC.Code AS Channel, BC.AcceptanceStatus, BC.AcceptanceStatusType, BC.ModifiedDate, p.SourceSystem, 
pc.SourceSystem, pc.CustomerType, p.CreatedDate FROM BrandChannel(NOLOCK) BC JOIN ProductBrand(NOLOCK) PB on PB.PartyProductId = BC.PartyProductId join PartyProductInformation(NOLOCK) PPI on PPI.PartyInformationId = PB.PartyInformationId join Party(NOLOCK) P on P.PartyId = PPI.partyid JOIN dbo.PartyCompany(NOLOCK) pc ON pc.PartyId = P.PartyId Join Reference.Channel(NOLOCK) RC on RC.ChannelId = BC.ChannelId join Reference.Brand(NOLOCK) RB on RB.BrandId = PB.BrandId Join Reference.Product(NOLOCK) RP on RP.ProductId = PPI.ProductId where P.CustomerIdNumber = '2202075043000' ORDER BY Brand, Product, Channel").ToList();
                    Thread.Sleep(10000);
                    if (decisionSimulatorData.Count != 0)
                    {
                        break;
                    }
                }
                var accpeatedStatus = decisionSimulatorData[0].AcceptanceStatus;
                var accpeatedStatusType = decisionSimulatorData[0].AcceptanceStatusType;
                try
                {
                    Assert.AreEqual("1", accpeatedStatus);
                    Assert.AreEqual("2", accpeatedStatusType);
                }
                catch (Exception ex)
                {
                    //BuildMessage(ex.Message, ex.StackTrace);
                }
            }
        }

        [Test]
        [Category("Verify Consent")]
        public void VerifyExclusionId()
        {

            List<DecisionOptInOptOutData> decisionSimulatorData = new List<DecisionOptInOptOutData>();

            SqlConnection db = new SqlConnection("Server=DEVDSQL01;Database=OptInOptOut;Trusted_Connection=true");

            {
                db.Open();

                decisionSimulatorData = db.Query<DecisionOptInOptOutData>($"USE OptInOptOut SELECT TOP (1000) [ConsentExclusionId],[IdNumber],[CreatedDate],[ExclusionTypeId]FROM[OptInOptOut].[dbo].[ConsentExclusion]").ToList();

                string idNumber = decisionSimulatorData[0].IdNumber;

                _driver.Url = "http://datapp32/DAConsentEngineUI/customerexclusionlist";
                _driver.FindElement(By.Id("inputIdNumber")).SendKeys(idNumber);
                _driver.FindElement(By.XPath("//button[contains(text(),'Search')]")).Click();
                Thread.Sleep(5000);
                _driver.FindElement(By.XPath("//button[contains(text(),'Add user to an exclusion list')]")).Click();
                Thread.Sleep(2000);
                SelectElement selectHoldingCompany = new SelectElement(_driver.FindElement(By.XPath("//body/app[1]/div[2]/div[2]/div[1]/div[2]/div[1]/form[1]/div[1]/div[1]/select[1]")));
                selectHoldingCompany.SelectByText("Sanlam");
                Thread.Sleep(2000);
                SelectElement selectExclusionList = new SelectElement(_driver.FindElement(By.XPath("//body/app[1]/div[2]/div[2]/div[1]/div[2]/div[1]/form[1]/div[1]/div[2]/select[1]")));
                selectExclusionList.SelectByText("Minor");

                Thread.Sleep(2000);
                _driver.FindElement(By.XPath("//body/app[1]/div[2]/div[2]/div[1]/div[2]/div[1]/form[1]/div[1]/div[3]/button[1]")).Click();
                Thread.Sleep(2000);
                _driver.FindElement(By.XPath("//tbody/tr[3]/td[1]")).Click();
                Thread.Sleep(2000);
                _driver.FindElement(By.XPath("//body[1]/app[1]/div[2]/div[2]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[4]/td[1]/div[1]/table[1]/tbody[1]/tr[1]/td[4]/button[1]/span[1]")).Click();


                Thread.Sleep(2000);
                decisionSimulatorData = db.Query<DecisionOptInOptOutData>($"USE OptInOptOut SELECT TOP (1000) [ConsentExclusionId],[IdNumber],[CreatedDate],[ExclusionTypeId],[HoldingCompanyId]FROM[OptInOptOut].[dbo].[RemoveConsentExclusion] where IdNumber = '5101255057003'; ").ToList();
                string holdingCompanyId = decisionSimulatorData[0].HoldingCompanyId;
                Assert.AreEqual(holdingCompanyId, "2");


            }
        }

    }
}


