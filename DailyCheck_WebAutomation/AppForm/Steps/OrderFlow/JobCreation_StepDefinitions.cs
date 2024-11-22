using DailyCheck_WebAutomation.AppForm.Pages.PMO;
using DailyCheck_WebAutomation.Common;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DailyCheck_WebAutomation.AppForm.Steps.OrderFlow
{
    [Binding]
    public class JobCreation_StepDefinitions: Base
    {
        protected IWebDriver driver;
        JobCreation JobCreation = new JobCreation();


        [Given(@"Launch the solo browser")]
        public void GivenLaunchTheSoloBrowser()
        {
            openUrl(getConfigVal("Telent_URL"));
            if (tim > 9) LogIt("Time taken: " + tim + " seconds", logTyp.Warn);
        }

        [When(@"User login with valid solo credentials")]
        public void WhenUserLoginWithValidSoloCredentials()
        {
            typeText(JobCreation.userID, getConfigVal("Telent_UserID"));
            typeText(JobCreation.password, getConfigVal("Telent_Password"));
            ClickEl(JobCreation.submitButton); implicitWait(6);
        }

        [When(@"Click on Advanced job search link")]
        public void WhenClickOnAdvancedJobSearchLink()
        {
            hoverOn(JobCreation.hoverPMO);
            ClickEl(JobCreation.clickAdvancedJobSearch);
        }

        [Then(@"Verify the display of Advanced Job Search screen")]
        public void ThenVerifyTheDisplayOfAdvancedJobSearchScreen()
        {
            isThisShown(JobCreation.checkUI);
        }

        [Then(@"Verify the screen elements of Advanced Job Search screen")]
        public void ThenVerifyTheScreenElementsOfAdvancedJobSearchScreen()
        {
            isThisShown(JobCreation.clickeBusCheckbox);
            isThisShown(JobCreation.selectWorkstream);
        }

        [Given(@"Click on the My Job button")]
        public void GivenClickOnTheMyJobButton()
        {
            explicitWaitForElementToBeVisible(JobCreation.MyJobButton, 5);
            ClickEl(JobCreation.MyJobButton);
        }

        [Then(@"Click on the Add Job button")]
        public void ThenClickOnTheAddJobButton()
        {
            explicitWaitForElementToBeVisible(JobCreation.AddJobButton, 5);
            ClickEl(JobCreation.AddJobButton);
        }

        [Then(@"Verify the mandatory UI elements in Add job Form screen")]
        public void ThenVerifyTheMandatoryUIElementsInAddJobFormScreen()
        {
            isThisShown(JobCreation.checkAdvancedJobSearchUI);
        }

        [When(@"Select the Contract from dropdown")]
        public void WhenSelectTheContractFromDropdown()
        {
            SelectText(JobCreation.selectContract, getConfigVal("A537_Contract"));

            
      
        }

        [When(@"Select the Exchange Area from dropdown")]
        public void WhenSelectTheExchangeAreaFromDropdown()
        {
            throw new PendingStepException();
        }

        [When(@"Enter the value in Estimate No field")]
        public void WhenEnterTheValueInEstimateNoField()
        {
            throw new PendingStepException();
        }

        [When(@"Select the Job Category from dropdown")]
        public void WhenSelectTheJobCategoryFromDropdown()
        {
            throw new PendingStepException();
        }

        [When(@"Enter the value in Required by Date/Time field")]
        public void WhenEnterTheValueInRequiredByDateTimeField()
        {
            throw new PendingStepException();
        }

        [When(@"Enter the value in Originator field")]
        public void WhenEnterTheValueInOriginatorField()
        {
            throw new PendingStepException();
        }

        [When(@"Enter the value in OCU field")]
        public void WhenEnterTheValueInOCUField()
        {
            throw new PendingStepException();
        }

        [When(@"Enter the value in Receipt Date/Time field")]
        public void WhenEnterTheValueInReceiptDateTimeField()
        {
            throw new PendingStepException();
        }

        [Then(@"Select the Depot from dropdown")]
        public void ThenSelectTheDepotFromDropdown()
        {
            throw new PendingStepException();
        }

        [Then(@"Enter the value in COW field")]
        public void ThenEnterTheValueInCOWField()
        {
            throw new PendingStepException();
        }

        [Then(@"Enter the value in Location field")]
        public void ThenEnterTheValueInLocationField()
        {
            throw new PendingStepException();
        }


    }


}
