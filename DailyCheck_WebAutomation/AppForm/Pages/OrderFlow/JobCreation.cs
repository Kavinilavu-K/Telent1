using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCheck_WebAutomation.AppForm.Pages.PMO
{
    public class JobCreation
    {
        //--PMO_ValidationJobQueue-->
        //Telent Solo website elements
        public string userID = "//input[@id='UserName']";
        public string password = "//input[@id='Password']";
        public string submitButton = "//input[@id='LoginButton']";
        public string homepage = "//body[@id='MaterBodyId']";
        //Advanced Job Search
        public string hoverPMO = "//td[@id='ctl00_cCtrlMenu-menuItem000']";
        public string clickAdvancedJobSearch = "//*[@id='ctl00_cCtrlMenu-menuItem000-subMenu-menuItem001']";
        public string checkUI = "//div[@class='container-fluid']";
        public string MyJobButton = "//input[@id='ctl00_cphContent_btnMyJobs']";
        public string AddJobButton = "//input[@id='ctl00_cphContent_btnNAddJobs']";
        public string mandatoryUI = "//div[@class='container-fluid']";
        public string clickeBusCheckbox = "//input[@id='ctl00_cphContent_ChkeBus']";
        public string selectWorkstream = "//select[@id='ctl00_cphContent_ddlWorkstream']";
        public string checkAdvancedJobSearchUI = "//div[@class='panel panel-default']";
        public string selectContract = "//select[@id='ctl00_cphContent_JobPoolTabContainer_TabA537_ddlA537Contract']";


    }
}
