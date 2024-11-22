using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using BrowserStack;
using Microsoft.Extensions.Logging.Console.Internal;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using EdgeOptions = Microsoft.Edge.SeleniumTools.EdgeOptions;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using Path = System.IO.Path;

namespace DailyCheck_WebAutomation.Common
{
    public class Base
    {
        public static IWebDriver dr = null;
        public static string br;
        public static string url;
        public static string brNm;
        public static string brVer;
        public static string appVer;
        public static WebDriverWait wt = null;
        public static IJavaScriptExecutor js = null;
        public static Actions ac = null;
        public static IAlert alert = null;
        public static Local local;
        Random rand = new Random();

        static Stopwatch stopWatch = new Stopwatch();
        public static int tim = 0;
        public static ExtentTest FeatureName;
        public static ExtentTest scenarioName;
        public static ExtentTest logg = null;
        public static ExtentReports Extent = new ExtentReports();

        public static DataTable xltbl = new DataTable();
        public static string sceneStat;

        public static int webdriverWaitTimeInSeconds = 5;
        public static string pth = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
        public static string rPath = pth + "TestResults\\" + DateTime.Now.ToString("yyyy-MM-dd_") + DateTime.Now.ToString("HH") + "h" + DateTime.Now.ToString("mm") + "m";
        //public static string rPath = pth + "TestResults\\" + DateTime.Now.ToString("yyyy'-'MM'-'dd'_T_'HH':'mm':'ss");


        static GeneralLibraries GL = new GeneralLibraries();

        public enum logTyp { Console, Info, Error, Err, Fail, Fatal, Warn, WarnS, Skip, Pass, PassRest, Stop, Ignore, Success, Logs }

        /*---Initializing webdriver object--*/
        //Objective: This method starts the browser based on BROWSER value in Test.properties file, also initalizes javascript, action, webdriver wait objects at once.
        public static void Browser()
        {
            br = getConfigVal("BROWSER");
            // killdriver(br);
            try
            {
                if (br.Equals("CR"))
                {
                    //string USERNAME = "sudhanshusrivast3";
                    //string AUTOMATE_KEY = "XWyz2UiHxxjBqHkiwxRy";



                    ////IWebDriver driver;
                    //DesiredCapabilities capss = new DesiredCapabilities();



                    //capss.SetCapability("os", "Windows");
                    //capss.SetCapability("os_version", "10");
                    //capss.SetCapability("browser", "Chrome");
                    //capss.SetCapability("browser_version", "90");
                    //capss.SetCapability("browserstack.user", USERNAME);
                    //capss.SetCapability("browserstack.key", AUTOMATE_KEY);
                    //capss.SetCapability("name", "AppFormDailyTest");



                    //dr = new RemoteWebDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub/"), capss);

                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddUserProfilePreference("download.default_directory", rPath + "\\Screenshots\\downloads");
                    chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
                    ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                    dr = new ChromeDriver(chromeDriverService, chromeOptions);
                    dr.Manage().Window.Maximize();
                }
                else if (br.Equals("CRH"))
                {
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments(new List<string>() { "headless", "disable-gpu" });
                    dr = new ChromeDriver(chromeOptions);
                }
                else if (br.Equals("IE"))
                {
                    dr = new InternetExplorerDriver();
                }
                else if (br.Equals("FF"))
                {
                    dr = new FirefoxDriver();
                }
                else if (br.Equals("EEL"))
                {
                    EdgeOptions edgeOpt = new Microsoft.Edge.SeleniumTools.EdgeOptions();
                    edgeOpt.UseChromium = false;
                    dr = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeOpt);
                }
                else if (br.Equals("EE"))
                {
                    EdgeOptions edgeOpt = new Microsoft.Edge.SeleniumTools.EdgeOptions();
                    edgeOpt.UseChromium = true;
                    dr = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeOpt);
                }
                else if (br.Equals("EEH"))
                {
                    EdgeOptions edgeOpt = new Microsoft.Edge.SeleniumTools.EdgeOptions();
                    edgeOpt.UseChromium = true;
                    edgeOpt.AddArguments(new List<string>() { "headless", "disable-gpu" });
                    dr = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeOpt);
                }

                else if (br.Equals("BRSTACK"))
                {
                    string USERNAME = "sudhanshusrivast3";
                    string AUTOMATE_KEY = "XWyz2UiHxxjBqHkiwxRy";



                    HttpCommandExecutor commandExecutor = new HttpCommandExecutor(new Uri("http://sudhanshusrivast3:XWyz2UiHxxjBqHkiwxRy@hub-cloud.browserstack.com/wd/hub/"), TimeSpan.FromSeconds(60));
                    commandExecutor.Proxy = new WebProxy("http://proxy.mdu.local:8081", false);

                    var capability = new ChromeOptions();


                    capability.AddAdditionalCapability("build", "Natraj-Test", true);
                    capability.AddAdditionalCapability("name", "After_Proxy", true);
                    //capability.AddAdditionalCapability("browserstack.debug", true);
                    capability.AddAdditionalCapability("browser", "Chrome", true);
                    //options.AddAdditionalCapability("browser_version", "latest", true);
                    //options.AddAdditionalCapability("os", "Windows", true);
                    //options.AddAdditionalCapability("os_version", "10", true);
                    capability.AddAdditionalCapability("browserstack.user", USERNAME, true);
                    capability.AddAdditionalCapability("browserstack.key", AUTOMATE_KEY, true);
                    //options.AddAdditionalCapability("browserstack.local", true);
                    capability.AddArgument("start-maximized");

                    //driver = new RemoteWebDriver(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability.ToCapabilities());
                    dr = new RemoteWebDriver(commandExecutor, capability.ToCapabilities());

                }

                else if (br.Equals("BrLocalChrome"))
                {
                    string USERNAME = "sudhanshusrivast3";
                    string AUTOMATE_KEY = "XWyz2UiHxxjBqHkiwxRy";



                    HttpCommandExecutor commandExecutor = new HttpCommandExecutor(new Uri("http://sudhanshusrivast3:XWyz2UiHxxjBqHkiwxRy@hub-cloud.browserstack.com/wd/hub/"), TimeSpan.FromSeconds(60));
                    commandExecutor.Proxy = new WebProxy("http://proxy.mdu.local:8081", false);

                    local = new Local();

                    //replace <browserstack-accesskey> with your key. You can also set an environment variable - "BROWSERSTACK_ACCESS_KEY".
                    List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
  new KeyValuePair<string, string>("key", "XWyz2UiHxxjBqHkiwxRy"),
                };

                    bsLocalArgs.Add(new KeyValuePair<string, string>("proxyHost", "proxy.mdu.local"));
                    bsLocalArgs.Add(new KeyValuePair<string, string>("proxyPort", "8081"));
                    bsLocalArgs.Add(new KeyValuePair<string, string>("proxyUser", "MDU\\zz2252.admr"));
                    bsLocalArgs.Add(new KeyValuePair<string, string>("proxyPass", "London2021!!"));

                    //starts the Local instance with the required arguments
                    local.start(bsLocalArgs);

                    // check if BrowserStack local instance is running
                    Console.WriteLine(local.isRunning());

                    //stop the Local instance
                    //local.stop();


                    var capability = new ChromeOptions();


                    capability.AddAdditionalCapability("build", "Natraj-Test", true);
                    capability.AddAdditionalCapability("name", "After_Proxy", true);
                    //capability.AddAdditionalCapability("browserstack.debug", true);
                    capability.AddAdditionalCapability("browser", "Chrome", true);
                    //options.AddAdditionalCapability("browser_version", "latest", true);
                    //options.AddAdditionalCapability("os", "Windows", true);
                    //options.AddAdditionalCapability("os_version", "10", true);
                    capability.AddAdditionalCapability("browserstack.local", "true", true);
                    capability.AddAdditionalCapability("acceptSslCerts", "true", true);
                    capability.AddAdditionalCapability("browserstack.user", USERNAME, true);
                    capability.AddAdditionalCapability("browserstack.key", AUTOMATE_KEY, true);

                    capability.AddArgument("start-maximized");

                    //driver = new RemoteWebDriver(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability.ToCapabilities());
                    dr = new RemoteWebDriver(commandExecutor, capability.ToCapabilities());

                }

                else if (br.Equals("MacSafari"))
                {
                    string USERNAME = "sudhanshusrivast3";
                    string AUTOMATE_KEY = "XWyz2UiHxxjBqHkiwxRy";



                    HttpCommandExecutor commandExecutor = new HttpCommandExecutor(new Uri("http://sudhanshusrivast3:XWyz2UiHxxjBqHkiwxRy@hub-cloud.browserstack.com/wd/hub/"), TimeSpan.FromSeconds(60));
                    commandExecutor.Proxy = new WebProxy("http://proxy.mdu.local:8081", false);

                    var capability = new SafariOptions();


                    capability.AddAdditionalCapability("build", "Natraj-Test");
                    capability.AddAdditionalCapability("name", "EarlyMorning_Checks");
                    //capability.AddAdditionalCapability("browserstack.debug", true);
                    //capability.AddAdditionalCapability("browser", "Chrome", true);
                    //options.AddAdditionalCapability("browser_version", "latest", true);
                    //options.AddAdditionalCapability("os", "Windows", true);
                    //options.AddAdditionalCapability("os_version", "10", true);
                    capability.AddAdditionalCapability("os", "OS X");
                    capability.AddAdditionalCapability("os_version", "Big Sur");
                    capability.AddAdditionalCapability("browser", "Safari");
                    capability.AddAdditionalCapability("browser_version", "14.0");
                    //capability.AddAdditionalCapability("browserstack.local", true);
                    capability.AddAdditionalCapability("browserstack.user", USERNAME);
                    capability.AddAdditionalCapability("browserstack.key", AUTOMATE_KEY);
                    //options.AddAdditionalCapability("browserstack.local", true);
                    //capability.AddArgument("start-maximized");

                    //driver = new RemoteWebDriver(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability.ToCapabilities());
                    dr = new RemoteWebDriver(commandExecutor, capability.ToCapabilities());

                }


                else
                {
                    brNm = "Unknown browser";
                    LogIt("We don't support this browser: " + br, logTyp.Stop);
                }
            }
            catch (Exception e)
            {
                LogIt("Couldn't start " + br + " webdriver :exc: " + e, logTyp.Stop);
            }
            //dr.Manage().Window.Maximize();

            //assign driver object to javascript and action class
            js = (IJavaScriptExecutor)dr;
            ac = new Actions(dr);
            wt = new WebDriverWait(dr, TimeSpan.FromSeconds(webdriverWaitTimeInSeconds));
            LogIt("Launched: " + br + " browser", logTyp.Info);

            //Get browser name and version
            ICapabilities caps = ((RemoteWebDriver)dr).Capabilities;
            brNm = caps.GetCapability("browserName").ToString();
            brVer = caps.GetCapability("browserVersion").ToString();
        }

        //Objective: Opens provided url and also logs notes url app version
        public static void openUrl(string browserUrl)
        {
            if (dr == null) LogIt("Webdriver not initialized", logTyp.Skip);

            try
            {
                startTime();
                LogIt("Launching url:" + browserUrl, logTyp.Info);
                dr.Navigate().GoToUrl(browserUrl);
                LogIt("Launched url:" + browserUrl, logTyp.Logs);
                stopTime();
            }
            catch (Exception e)
            {
                LogIt("Couldn't load the url:exc: " + e, logTyp.Ignore);
            }

        }


        public void ClickOnCookieAccept()
        {
            try
            {
                By CookieAccept = By.Id("CybotCookiebotDialogBodyButtonAccept");
                var wait = new WebDriverWait(dr, TimeSpan.FromSeconds(3));
                wait.Until(x => x.FindElement(CookieAccept)).Click();
            }
            catch { }
        }

        /*---Current Page Object model: Routines for getting webelement/s and it's labels--*/
        //Objective: Extracts ID, xpath or CSS value from page elements variables
        public static string GetEleVal(string ele)
        {
            return ele.Contains("##") ? Regex.Split(ele, "##")[0].Trim() : ele;
        }
        //Objective: Extracts labels from page elements variables
        public static string GetEleLbl(string ele)
        {
            return ele.Contains("##") ? Regex.Split(ele, "##")[1].Trim() : ele;
        }
        //Objective: Gets By object value based on ID, xpath or CSS value provided
        public static By getByLoc(string locVal)
        {
            By byLoc = null;
            //XPath
            if (locVal.Substring(0, 1).Equals("/")) //previously it was ("/")
                byLoc = By.XPath(GetEleVal(locVal)); //written by N
            //CssSelector
            else if (locVal.Substring(0, 4).Equals("CSS="))
                byLoc = By.CssSelector(GetEleVal(locVal).Replace("CSS=", "").Trim()); //written by N
/*            //Name
            else if (locVal.Substring(0, 5).Equals("<Name>"))
            byLoc = By.Name(GetEleVal(locVal));
            //ClassName                                                                         
            else if (locVal.Substring(0, 1).Equals("."))
                byLoc = By.ClassName(GetEleVal(locVal));
            //LinkText
            else if (locVal.Substring(0, 1).Equals("/"))
                byLoc = By.LinkText(GetEleVal(locVal));
            //PartialLinkText
            else if (locVal.Substring(0, 17).Equals("<PartialLinkText>"))
                byLoc = By.PartialLinkText(GetEleVal(locVal).Replace("<PartialLinkText>", "").Trim());
            //TagName
            else if (locVal.Substring(0, 9).Equals("<TagName>"))
                byLoc = By.TagName(GetEleVal(locVal).Replace("<TagName>", "").Trim());*/
            //Id
            else
                byLoc = By.Id(GetEleVal(locVal)); //written by N
            return byLoc;
        }
        //Objective: Gets Webelement object based on locator value
        public static IWebElement GetElement(string LocatorValue)
        {
            IWebElement ele = null;

            try
            {
                //ele = wt.Until(ExpectedConditions.ElementIsVisible(getByLoc(LocatorValue)));

                var wait = new WebDriverWait(dr, TimeSpan.FromSeconds(5));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                ele = wait.Until(dr => dr.FindElement(getByLoc(LocatorValue)));

            }
            catch (Exception e)
            {
                LogIt(GetEleLbl(LocatorValue) + " not found :exc:" + e, logTyp.Fail);
            }
            return ele;
        }
        //Objective: Gets list of Webelements object based on locator value
        public static IList<IWebElement> GetElements(string LocatorValue)
        {
            IList<IWebElement> els = new List<IWebElement>();

            try
            {
                els = wt.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(getByLoc(LocatorValue)));
                LogIt(els.Count + " count of lements found for " + GetEleLbl(LocatorValue), logTyp.Logs);
            }
            catch (Exception e)
            {
                LogIt(GetEleLbl(LocatorValue) + "  elements not found :exc:" + e, logTyp.Logs);
            }

            return els;
        }

        /*---Routines for alert--*/
        //Objective: Checks the presence of an alert
        public static bool IsAlert()
        {
            bool presentFlag = false;
            try
            {
                alert = dr.SwitchTo().Alert();
                presentFlag = true;
                LogIt("Checked for alert and alert shown as: " + getAlertText(), logTyp.Info);
            }
            catch (Exception e) { LogIt("Checked for alert and found none:exc:" + e, logTyp.Logs); }
            return presentFlag;
        }
        //Objective: Gets the alert text message
        public static string getAlertText()
        {
            string alrtMsg = null;
            if (IsAlert())
            {
                try
                {
                    alrtMsg = alert.Text;
                    LogIt("Got texts: " + alrtMsg, logTyp.Logs);
                }
                catch (Exception e)
                {
                    try
                    {
                        alrtMsg = js.ExecuteScript("returnwindow.alert.myAlertText;").ToString();
                        LogIt("Got texts: " + alrtMsg + " :using javascript :exc:" + e, logTyp.Logs);
                    }
                    catch (Exception ex)
                    {
                        LogIt("Could not get texts from alert :exc: " + ex, logTyp.Error);
                    }
                }
            }
            else { LogIt("No alert present to get the alert message", logTyp.Fail); }
            return alrtMsg;
        }
        //Objective: Cancels alert
        public static void dismissAlert()
        {
            string logMsg = "";
            if (IsAlert())
            {
                logMsg = getAlertText();
                try
                {
                    alert.Dismiss();
                    LogIt("Alert Cancelled with message: " + logMsg, logTyp.Info);
                }
                catch (Exception e)
                {
                    LogIt("Could not cancel alert " + logMsg + " :exc: " + e, logTyp.Error);
                }
            }
            else { LogIt("No alert present to cancel the alert", logTyp.Fail); }
        }
        //Objective: Accepts alert
        public static void acceptAlert()
        {
            string logMsg;
            if (IsAlert())
            {
                logMsg = getAlertText();
                try
                {
                    alert.Accept();
                    LogIt("Alert accepted with message: " + logMsg, logTyp.Info);
                }
                catch (Exception e)
                {
                    LogIt("Could not accept alert " + logMsg + " :exc: " + e, logTyp.Error);
                }
            }
            else { LogIt("No alert present to accept the alert", logTyp.Fail); }
        }

        /*---Routines for sending and clearing keys on webelement--*/
        //Objective: Entering texts on Xpath/ID/CSS
        public static void typeText(string eleXP, string text)
        {
            typeTextW(GetElement(eleXP), GetEleLbl(eleXP), text);
        }
        //Objective: Entering texts on Web Element
        public static void typeTextW(IWebElement el, string lbl, string text)
        {
            string lbltext = lbl.ToUpper().Contains("PASSWORD") ? "**********" : text;
            try
            {
                el.SendKeys(text);
                LogIt("Typed '" + lbltext + "' in element: " + lbl, logTyp.Info);
            }
            catch (Exception e)
            {
                try
                {
                    if (getAttributeW(el, lbl, "class").Contains("Datepicker")) { text = DateTime.ParseExact(text, "ddMMyyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy"); }
                    js.ExecuteScript("arguments[0].setAttribute('value','" + text + "')", el);
                    LogIt("Could type '" + lbltext + "' in element: " + lbl + " :using javascript:exc:" + e, logTyp.Info);
                }
                catch (Exception ex)
                {
                    LogIt("Could not type '" + lbltext + "' in element: " + lbl + " :exc: " + ex, logTyp.Fail);
                }
            }
        }
        //Objective: Getting texts from Xpath/ID/CSS
        public static string getText(string eleXP)
        {
            return getTextW(GetElement(eleXP), GetEleLbl(eleXP));
        }
        //Objective: Getting texts from Web Element
        public static string getTextW(IWebElement el, string lbl)
        {
            string text = "";
            try
            {
                text = el.Text;
                if (text.Equals("") || text == null)
                {
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", el);
                    text = (string)js.ExecuteScript("return arguments[0].innerText;", el);
                    LogIt("Got text:<" + text + "> using JS from element:exc:" + lbl, logTyp.Logs);
                }
                else
                {
                    LogIt("Got text:<" + text + "> from element: " + lbl, logTyp.Logs);
                }
            }
            catch (Exception e)
            {
                LogIt("Could not get text from element: " + lbl + " :exc: " + e, logTyp.Fail);
            }
            return text;
        }
        //Objective: Clearing texts from Xpath/ID/CSS
        public static void clearText(string eleXP)
        {
            clearTextW(GetElement(eleXP), GetEleLbl(eleXP));
        }
        //Objective: Clearing texts from Web Element
        public static void clearTextW(IWebElement el, string lbl)
        {
            try
            {
                el.Clear();
                LogIt("Cleared texts from element: " + lbl, logTyp.Info);
            }
            catch (Exception e)
            {
                try
                {
                    js.ExecuteScript("arguments[0].value = '';", el);
                    LogIt("Cleared from element: " + lbl + " using js:exc:" + e, logTyp.Info);
                }
                catch (Exception ex)
                {
                    LogIt("Could not clear text from element: " + lbl + " :exc: " + ex, logTyp.Fail);
                }
            }
        }

        /*---Routines for web element attributes--*/
        //Objective: Gets provided attribute value from given Xpath/ID/CSS
        public static string getAttribute(string eleXP, string attr)
        {
            return getAttributeW(GetElement(eleXP), GetEleLbl(eleXP), attr);
        }
        //Objective: Gets provided attribute value from given Web Element
        public static string getAttributeW(IWebElement ele, string lbl, string attr)
        {
            string attrVal = "";
            try
            {
                attrVal = ele.GetAttribute(attr);
                LogIt("Got value:[" + attrVal + "] from element: " + lbl + " for attribute: " + attr, logTyp.Logs);
            }
            catch (Exception e)
            {
                LogIt("Could not get attribute value for " + attr + " from element: " + lbl + " :exc: " + e, logTyp.Fail);
            }
            return attrVal;
        }
        //Objective: Checks provided attribute present in given Xpath/ID/CSS
        public bool isAttribute(string eleXP, string attr)
        {
            return isAttributeW(GetElement(eleXP), GetEleLbl(eleXP), attr);
        }
        //Objective: Checks provided attribute present in the given Web Element
        public bool isAttributeW(IWebElement element, string lbl, string attribute)
        {
            bool flag;
            flag = element.GetAttribute(attribute) != null ? true : false;
            if (flag)
                LogIt(lbl + " has atrribute: " + attribute, logTyp.Logs);
            else
                LogIt(lbl + " doesn't have atrribute: " + attribute, logTyp.Error);

            return flag;
        }
        //Objective: Sets provided attribute value on given Xpath/ID/CSS
        public void setAttribute(string eleXP, string attr, string text)
        {
            setAttributeW(GetElement(eleXP), GetEleLbl(eleXP), attr, text);
        }
        //Objective: Sets provided attribute value to given Web Element
        public void setAttributeW(IWebElement el, string lbl, string attr, string text)
        {
            try
            {
                js.ExecuteScript("arguments[0].setAttribute('" + attr + "', '" + text + "')", el);
                LogIt("Attribute '" + attr + "' set as '" + text + "' for element: " + lbl, logTyp.Info);
            }
            catch (Exception ex)
            {
                LogIt("Attribute '" + attr + "' could not be set as '" + text + "' for element: " + lbl + " :exc: " + ex, logTyp.Fail);
            }
        }

        /*---Routines for dropdown webelements--*/
        //Objective: Gets selected texts from given Xpath/ID/CSS of a dropdown
        public string GetSelectedText(string locval, string v)
        {
            return GetSelectedTextW(GetElement(GetEleVal(locval)), GetEleLbl(locval));
        }
        //Objective: Gets selected texts from given dropdown web element
        public string GetSelectedTextW(IWebElement el, string lbl)
        {
            SelectElement Slct = new SelectElement(el);
            string rtrnVal = null;
            try
            {
                rtrnVal = Slct.SelectedOption.Text;
                LogIt("Got value: " + rtrnVal + " from dropdown: " + lbl, logTyp.Logs);
            }
            catch (Exception e)
            {
                LogIt("Could not get selected text from dropdown: " + lbl + " :exc: " + e, logTyp.Fail);
            }

            return rtrnVal;
        }
        //Objective: Selects random texts from given Xpath/ID/CSS of a dropdown
        public string selectRandom(string locval)
        {
            return selectRandomW(GetElement(GetEleVal(locval)), GetEleLbl(locval));
        }
        //Objective: Selects random texts from given dropdown web element
        public string selectRandomW(IWebElement ddele, string lbl)
        {
            SelectElement Select = new SelectElement(ddele);
            string retVal = null;
            try
            {
                Select.SelectByIndex(rand.Next(Select.Options.Count));
                if (GetSelectedTextW(ddele, lbl).Trim().Equals(""))
                {
                    Select.SelectByIndex(rand.Next(Select.Options.Count));
                    if (GetSelectedTextW(ddele, lbl).Trim().Equals(""))
                    {
                        Select.SelectByIndex(rand.Next(Select.Options.Count));
                        if (GetSelectedTextW(ddele, lbl).Trim().Equals(""))
                        {
                            LogIt("Cannot proceed as empty value got selected randomly everytime in dropdown" + lbl, logTyp.Fail);
                        }
                    }
                }
                retVal = GetSelectedTextW(ddele, lbl);
                LogIt("Selected: " + retVal + " randomly from element: " + lbl, logTyp.Info);
            }
            catch (Exception e)
            {
                LogIt("Couldn't select random value from element: " + lbl + " due to :exc: " + e, logTyp.Fail);
            }
            return retVal;
        }
        //Objective: Selects provided texts from given Xpath/ID/CSS of a dropdown
        public void SelectText(string eleXP, string txt)
        {
            SelectTextW(GetElement(eleXP), GetEleLbl(eleXP), txt);
        }
        //Objective: Selects provided texts from given dropdown web element
        public void SelectTextW(IWebElement el, string lbl, string txt)
        {
            SelectElement Select = new SelectElement(el);
            string al;
            try { al = Select.SelectedOption.Text; } catch { al = "<<nothing>>"; }
            if (al.Equals("<<nothing>>") || !txt.Equals(al))
            {
                try
                {
                    Select.SelectByText(txt);
                    LogIt("Selected: " + txt + " from element: " + lbl, logTyp.Info);
                }
                catch (Exception e)
                {
                    LogIt("Could not select: " + txt + " from element: " + lbl + " :exc: " + e, logTyp.Fail);
                }
            }
            else
            {
                LogIt("Dropdown already has value: " + txt, logTyp.Info);
            }
        }
        //Objective: Checks whether given dropdown Xpath/ID/CSS with  has provided value
        public void dropdownDoesntHave(string eleXP, string txt)
        {
            dropdownDoesntHaveW(GetElement(eleXP), GetEleLbl(eleXP), txt);
        }
        //Objective: Checks whether given dropdown web element has provided value
        public bool dropdownDoesntHaveW(IWebElement el, string lbl, string SelectText)
        {
            bool flag = false;
            SelectElement Select = new SelectElement(el);
            try
            {
                if (SelectText.Equals(Select.SelectedOption.Text))
                    flag = true;
            }
            catch
            {
                try
                {
                    Select.SelectByText(SelectText);
                    flag = true;
                    LogIt("Found: " + SelectText + " from element: " + lbl + " which is not expected", logTyp.Fail);
                }
                catch (Exception e)
                {
                    LogIt("Cound not find: " + SelectText + " from element: " + lbl + " as expected :exc:" + e, logTyp.Logs);
                }
            }
            return flag;
        }
        //Objective: Selects value from given Xpath/ID/CSS of a dropdown based on provided index
        public string SelectByIndex(string eleXP, int ind)
        {
            return SelectByIndexW(GetElement(eleXP), GetEleLbl(eleXP), ind);
        }
        //Objective: Selects value from given dropdown web element based on provided index
        public string SelectByIndexW(IWebElement el, string lbl, int ind)
        {
            SelectElement Select = new SelectElement(el);
            try
            {
                Select.SelectByIndex(ind);
                LogIt("Selected: " + GL.getOrdinal(ind) + " value from dropdown: " + lbl, logTyp.Logs);
            }
            catch (Exception e)
            {
                LogIt("Could not select: " + GL.getOrdinal(ind) + " value from dropdown: " + lbl + " :exc: " + e, logTyp.Fail);
            }
            return GetSelectedTextW(el, lbl);
        }
        //Objective: Gets count of values from given Xpath/ID/CSS of a dropdown
        public int getDropdowncount(string eleXP)
        {
            return getDropdowncountW(GetElement(eleXP), GetEleLbl(eleXP));
        }
        //Objective: Gets count of values from given dropdown web element
        public int getDropdowncountW(IWebElement el, string lbl)
        {
            SelectElement Select = new SelectElement(el);
            LogIt(lbl + " has " + Select.Options.Count + " values", logTyp.Logs);
            return Select.Options.Count;
        }

        /*--click routines--*/
        //Objective: Checks whether given Xpath/ID/CSS is clickable
        public bool isClick(string eleXP)
        {
            return isClickW(GetElement(eleXP), GetEleLbl(eleXP));
        }
        //Objective: Checks whether given Web element is clickable
        public bool isClickW(IWebElement el, string lbl)
        {
            bool flag = false;
            try
            {
                el.Click();
                LogIt("Clicked element: " + lbl, logTyp.Logs);
                flag = true;
            }
            catch (Exception e)
            {
                LogIt("Element: " + lbl + " not clickable :exc:" + e, logTyp.Error);
            }
            return flag;
        }
        //Objective: Clicks given Xpath/ID/CSS
        public void ClickEl(string eleXP)
        {
            ClickElW(GetElement(eleXP), GetEleLbl(eleXP));
        }
        //Objective: Clicks given web element
        public void ClickElW(IWebElement el, string lbl)
        {
            try
            {
                el.Click();
                LogIt("Clicked element: " + lbl, logTyp.Info);
            }
            catch
            {
                try
                {
                    js.ExecuteScript("arguments[0].click();", el);
                    LogIt("Clicked '" + lbl + "' using javascript", logTyp.Info);
                }
                catch
                {
                    try
                    {
                        ScrollToTop();
                        try
                        {
                            el.Click();
                            LogIt("Clicked element: " + lbl + " after scrolling up", logTyp.Info);
                        }
                        catch
                        {
                            js.ExecuteScript("arguments[0].click();", el);
                            LogIt("Clicked '" + lbl + "' after scrolling up using javascript", logTyp.Info);
                        }
                    }
                    catch
                    {
                        ScrollToBottom();
                        try
                        {
                            el.Click();
                            LogIt("Clicked element: " + lbl + " after scrolling down", logTyp.Info);
                        }
                        catch
                        {
                            JSClickW(el, lbl + " after scrolling down");
                        }
                    }
                }
            }
        }

        //Objective: Clicks given Xpath/ID/CSS using javascript
        public void JSClick(string eleXP)
        {
            JSClickW(GetElement(eleXP), GetEleLbl(eleXP));
        }
        //Objective: Clicks given web element using javascript
        public void JSClickW(IWebElement ele, string lbl)
        {
            try
            {
                js.ExecuteScript("arguments[0].click();", ele);
                LogIt("Clicked '" + lbl + "' using javascript", logTyp.Info);
            }
            catch (Exception e)
            {
                LogIt("Could not click element: " + lbl + " :exc: " + e, logTyp.Fail);
            }

        }

        /*---- element present, visible, etc -----*/
        //Objective: Checks given Xpath/ID/CSS is present
        public static bool IsElepresent(string LocatorValue)
        {
            bool flag = false;
            By byLoc = getByLoc(LocatorValue);

            try
            {
                dr.FindElement(byLoc);
                flag = true;
            }
            catch
            {
                try
                {
                    ScrollToBottom();
                    dr.FindElement(byLoc);
                    flag = true;
                }
                catch
                {
                    try
                    {
                        ScrollToTop();
                        dr.FindElement(byLoc);
                        flag = true;
                    }
                    catch
                    {
                        LogIt(GetEleLbl(LocatorValue) + " not found.", logTyp.Logs);
                    }
                }
            }
            return flag;
        }

        //Objective: Checks given Xpath/ID/CSS is present
        public static bool isDisplayed(string LocatorValue)
        {
            bool flag = false;
            if (IsElepresent(LocatorValue))
            {
                flag = dr.FindElement(getByLoc(LocatorValue)).Displayed;
                if (!flag)
                {
                    ScrollToBottom();
                    flag = dr.FindElement(getByLoc(LocatorValue)).Displayed;
                }
                if (!flag)
                {
                    ScrollToTop();
                    flag = dr.FindElement(getByLoc(LocatorValue)).Displayed;
                }
                else
                {
                    LogIt("Web element present but not displayed" + GetEleLbl(LocatorValue), logTyp.Logs);
                }
            }
            else
            {
                LogIt("Web element not present" + GetEleLbl(LocatorValue), logTyp.Logs);
            }
            return flag;
        }

        public static void isThisShown(string LocatorValue)
        {
            if (isDisplayed(LocatorValue))
                LogIt(GetEleLbl(LocatorValue) + " is shown as expected", logTyp.Pass);
            else
                LogIt(GetEleLbl(LocatorValue) + " is not shown in page", logTyp.Fail);
        }
        /*--------Window handles--------*/
        //Objective: Switches to window based on window title
        public static void switchToWin(string titl)
        {
            string[] handles = dr.WindowHandles.ToArray();
            for (int i = 0; i < handles.Length; i++)
            {
                if (handles[i].Equals(titl))
                {
                    dr.SwitchTo().Window(handles[i]);
                    break;
                }
            }
            if (dr.Title.Equals(titl))
            {
                LogIt("Switched to Window: " + titl + " successfully", logTyp.Info);
            }
            else
            {
                LogIt("Could not switch to Window: " + titl, logTyp.Fail);
            }
        }
        //Objective: Closes window based on window title
        public static void closeWin(string titl)
        {
            bool flag = false;
            string[] handles = dr.WindowHandles.ToArray();
            for (int i = 0; i < handles.Length; i++)
            {
                if (handles[i].Equals(titl))
                {
                    dr.SwitchTo().Window(handles[i]).Close();
                    LogIt("Closed Window: " + titl + " successfully", logTyp.Info);
                    flag = true;
                    break;
                }
            }
            if (flag == false)
            {
                LogIt("Could not find or close Window: " + titl, logTyp.Fail);
            }
        }
        //Objective: Closes all other windows keeping one
        public static void keepOneWindow()
        {
            foreach (var win in dr.WindowHandles)
            {
                dr.SwitchTo().Window(win);
                if (dr.WindowHandles.Count > 1) dr.Close();
                wait(0.25);
            }
        }

        /*------ Page scrolling ------*/
        //Objective: scrolls to top of page
        public static void ScrollToTop()
        {
            try
            {
                js.ExecuteScript("window.scrollTo(document.body.scrollHeight, 0)");
                LogIt("scrolled to top of the page.", logTyp.Logs);
            }
            catch (Exception e)
            {
                LogIt("Couldn't scroll to top of page :exc: " + e, logTyp.Logs);
            }
        }
        //Objective: scrolls to bottom of page
        public static void ScrollToBottom()
        {
            long scrollHeight = 0;
            do
            {
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");
                if (newScrollHeight == scrollHeight)
                {
                    break;
                }
                else
                {
                    scrollHeight = newScrollHeight;
                    wait(0.4);
                }
            } while (true);
        }

        /*------- Action classes -------*/
        //Objective: drags one Xpath/ID/CSS to another Xpath/ID/CSS
        public void drag(string eleXP1, string eleXP2)
        {
            dragW(GetElement(GetEleVal(eleXP1)), GetEleLbl(eleXP1), GetElement(GetEleVal(eleXP2)), GetEleLbl(eleXP2));
        }
        //Objective: drags one web element to another web element using action class
        public void dragW(IWebElement srcW, string src, IWebElement targetW, string target)
        {
            try
            {
                js.ExecuteScript("arguments[0].click()", srcW);
                ac.DragAndDrop(srcW, targetW).Build().Perform();
                LogIt("Dragged " + src + " to " + target, logTyp.Info);
            }
            catch (Exception e)
            {
                LogIt("Couldn't drag " + src + " to " + target + ":exc:" + e, logTyp.Fail);
            }
        }
        //Objective: hovers on given Xpath/ID/CSS using action class
        public void hoverOn(string hovrEl)
        {
            hoverOnW(GetElement(hovrEl), GetEleLbl(hovrEl));
        }
        //Objective: hovers on given web element using action class
        public void hoverOnW(IWebElement el, string lbl)
        {
            try
            {
                ac.MoveToElement(el).Perform();
                LogIt("Hovered on :" + lbl, logTyp.Info);
            }
            catch (Exception e)
            {
                LogIt("Could not hover on " + lbl + " :exc: " + e, logTyp.Fail);
            }
        }

        /*------ Match routines for all data types -----*/
        //Objective: Matches boolean expected and actual values
        public void Match(bool act, bool exp, string msg)
        {
            if (act == exp)
            {
                LogIt("PASS >> " + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Info);
            }
            else
            {
                LogIt("ERROR >>" + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Error);
            }
        }
        //Objective: Matches integer expected and actual values
        public void Match(int act, int exp, string msg)
        {
            if (act == exp)
            {
                LogIt("PASS >> " + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Info);
            }
            else
            {
                LogIt("ERROR >>" + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Error);
            }
        }
        //Objective: Matches double expected and actual values
        public void Match(double act, double exp, string msg)
        {
            if (Math.Abs(act - exp) < 0.01)
            {
                LogIt("PASS >> " + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Info);
            }
            else
            {
                LogIt("ERROR >>" + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Error);
            }
        }
        //Objective: Matches string expected and actual values, Gives warning when case is different and also when either of them contains each other but <contains> keyword will pass them
        public void Match(string act, string exp, string msg)
        {
            if (act.Trim().Equals(exp.Trim()))
            {
                msg = msg.Replace("<contains>", "");
                LogIt("PASS >> " + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Info);
            }
            else if (act.ToUpper().Trim().Equals(exp.ToUpper().Trim()))
            {
                msg = msg.Replace("<contains>", "");
                LogIt("WARNING matched ignoring  case >> " + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Warn);
            }
            else if (msg.Contains("<contains>"))
            {
                msg = msg.Replace("<contains>", "");
                if (act.Contains(exp.Trim()))
                {
                    LogIt("Matched as Actual contains Expected >> " + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Info);
                }
                else if (exp.Contains(act.Trim()))
                {
                    LogIt("Matched as Expected contains Actual >> " + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Info);
                }
                else
                {
                    LogIt("ERROR >>" + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Error);
                }
            }
            else
            {
                if (act.Contains(exp.Trim()))
                {
                    LogIt("Not matched but Actual contains Expected >> " + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Warn);
                }
                else if (exp.Contains(act.Trim()))
                {
                    LogIt("Not matched but Expected contains Actual >> " + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Warn);
                }
                else
                {
                    LogIt("ERROR >>" + msg + " >> Actual: " + act + " :: Expected: " + exp, logTyp.Error);
                }
            }
        }

        /*----- Wait methods------*/
        //Objective: Halts code till provided seconds of time
        public static void wait(double x)
        {
            LogIt("Waiting for " + x + " second/s", logTyp.Logs);
            Thread.Sleep(Convert.ToInt32(x * 1000));
        }
        //Objective: Waits for given Xpath/ID/CSS untill provided seconds of time
        public static void waitTill(string eleXP, int k)
        {
            double t = 0;
            while (t < k)
            {
                if (IsElepresent(eleXP)) break;
                wait(0.1); t = t + 0.1;
            }
            if (t > 1) LogIt("Waited till " + t + " second/s for " + GetEleLbl(eleXP), logTyp.Info);
        }
        //Objective: Waits for web element untill provided seconds of time
        public static void waitTillW(IWebElement el, int k, string lbl)
        {
            double t = 0;
            while (t < k)
            {
                if (el.Displayed) break;
                wait(0.1); t = t + 0.1;
            }
            if (t > 1) LogIt("Waited till " + t + " second/s for element: " + lbl, logTyp.Info);
        }

        //Objective: Waits for ajax to complete
        public static void WaitForJqueryAjax()
        {
            int delay = 10;
            while (delay > 0)
            {
                Thread.Sleep(1000);
                var jquery = (bool)js.ExecuteScript("return window.jQuery == undefined");
                if (jquery)
                    break;
                var ajaxIsComplete = (bool)js.ExecuteScript("return window.jQuery.active == 0");
                if (ajaxIsComplete)
                    break;
                delay--;
            }
        }

        public void implicitWait(int seconds) //--It will work globally
        {
            dr.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }

        public void explicitWaitForElementToBeVisible(string locator, int seconds) //--It will work specific element
        {
            WebDriverWait wait = new WebDriverWait(dr, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(locator)));
        }

        //Objective: Takes snapshot immediately and stores in Debug\screenshots folder
        public static string getSnapShot()
        {
            string scenNme = TestContext.CurrentContext.Test.Name;
            string filNm = scenNme + ".png";
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)dr).GetScreenshot();
                screenshot.SaveAsFile(rPath + "\\Screenshots\\" + filNm, ScreenshotImageFormat.Png);
            }
            catch
            {
                filNm = "noScreenshot.png";
                File.Copy(pth + "Misc\\noScreenshot.png", rPath + "\\Screenshots\\noScreenshot.png", false);
            }
            return "Screenshots/" + filNm;
        }

        //Objective: Logs messages, screenshots in report and console and also controls execution
        public static void LogIt(string logMsg, logTyp lt)
        {
            string rMsg = "→ " + Regex.Split(logMsg, ":exc:")[0];

            switch (lt)
            {
                case logTyp.Console: //Logs message on only console for debugging purpose
                    Console.WriteLine(logMsg);
                    break;
                case logTyp.Logs: //Logs message on only log file
                    LogWrite(logMsg);
                    break;
                case logTyp.Info: //Logs message on both report & console
                    if (logg != null) logg.Info(rMsg); LogWrite(logMsg);
                    break;
                case logTyp.Err: //Logs error on both report & console with screenshot on report but continues executing scenario steps
                    if (logg != null) sceneStat = "ERROR"; logg.Error(MarkupHelper.CreateLabel(rMsg, ExtentColor.Pink)); failStep(logMsg);
                    break;
                case logTyp.Error: //Logs error on both report & console with screenshot on report but continues executing scenario steps
                    if (logg != null) sceneStat = "ERROR"; logg.Error(MarkupHelper.CreateLabel(rMsg, ExtentColor.Pink)).AddScreenCaptureFromPath(getSnapShot()); failStep(logMsg);
                    break;
                case logTyp.Fail: //Fails the scenario, skips rest scenarios steps and logs Failure on both report & console with screenshot on report
                    if (logg != null) sceneStat = "ERROR"; logg.Fail(MarkupHelper.CreateLabel("TEST FAILED: " + rMsg, ExtentColor.Red)).AddScreenCaptureFromPath(getSnapShot()); LogWrite("FAIL: " + logMsg); Assert.Fail(logMsg);
                    break;
                case logTyp.Fatal: //Fails the scenario, skips all the scenarios of current feature and logs Failure on both report & console with screenshot on report
                    if (logg != null) sceneStat = "ERROR"; logg.Fatal(MarkupHelper.CreateLabel("FEATURE BLOCKED: " + rMsg, ExtentColor.Orange)).AddScreenCaptureFromPath(getSnapShot()); setProperty("StopFeature", "YES"); LogWrite("FATAL: " + logMsg); Assert.Fail(logMsg);
                    break;
                case logTyp.PassRest: //Passes the scenario, skips next steps of scenario and logs message on both report and logs
                    if (logg != null) logg.Pass(MarkupHelper.CreateLabel(rMsg, ExtentColor.Green)); LogWrite("PASS: " + logMsg + "\nRest steps have been passed based on this."); Assert.Pass();
                    break;
                case logTyp.Pass: //Passes the scenario, skips next steps of scenario and logs message on both report and console
                    if (logg != null) sceneStat = "PASS"; logg.Pass(MarkupHelper.CreateLabel("TEST PASSED: " + rMsg, ExtentColor.Green)).AddScreenCaptureFromPath(getSnapShot()); LogWrite("PASS: " + logMsg);
                    break;
                //case logTyp.Pass: //Passes the scenario, skips next steps of scenario and logs message on both report and console
                //    if (logg != null) logg.Pass(MarkupHelper.CreateLabel(rMsg, ExtentColor.Green)); LogWrite("PASS: " + logMsg);
                //    break;
                case logTyp.Warn: //Logs warning on both report and console with screenshot on report but continues executing scenario steps
                    if (logg != null) sceneStat = "WARN"; logg.Warning(MarkupHelper.CreateLabel(rMsg, ExtentColor.Amber)); LogWrite("WARN: " + logMsg); Assert.Warn(logMsg);
                    break;
                case logTyp.WarnS: //Logs warning on both report and console with screenshot on report but continues executing scenario steps
                    if (logg != null) sceneStat = "WARN"; logg.Warning(MarkupHelper.CreateLabel(rMsg, ExtentColor.Amber)).AddScreenCaptureFromPath(getSnapShot()); LogWrite("WARN: " + logMsg); Assert.Warn(logMsg);
                    break;
                case logTyp.Skip: //Skips current scenario and logs message on both report & console goes to next scenario 
                    if (logg != null) sceneStat = "SKIP"; logg.Skip(MarkupHelper.CreateLabel("TEST SKIPPED: " + rMsg, ExtentColor.Grey)); LogWrite("SKIPPED: " + logMsg); Assert.Inconclusive();
                    break;
                case logTyp.Ignore: //Logs warning on both report and console with screenshot on report but skips executing remaining scenario steps
                    if (logg != null) sceneStat = "WARN"; logg.Warning(MarkupHelper.CreateLabel("TEST IGNORED: " + rMsg, ExtentColor.Cyan)).AddScreenCaptureFromPath(getSnapShot()); LogWrite("IGNORED: " + logMsg); Assert.Ignore(logMsg);
                    break;
                case logTyp.Success: //Logs message on both report & console along with screenshot on report
                    if (logg != null) logg.Info(MarkupHelper.CreateLabel(rMsg, ExtentColor.Green)).AddScreenCaptureFromPath(getSnapShot()); LogWrite("SUCCESS: " + logMsg);
                    break;
                case logTyp.Stop: //Stops execution and skips all remaining tests after this is logged
                    if (logg != null) sceneStat = "STOP"; logg.Skip(MarkupHelper.CreateLabel("TEST EXECUTION STOPPED: " + rMsg, ExtentColor.Grey)); setProperty("StopExecute", "YES"); LogWrite("STOPPED: " + logMsg); Assert.Inconclusive();
                    break;
                default: // just logs warning and continues
                    if (logg != null) sceneStat = "WARN"; logg.Warning(rMsg); LogWrite("UNKNOWN LOGTYPE: " + logMsg); Assert.Warn("Undefined log type" + logMsg);
                    break;
            }
        }

        public static void LogWrite(string logMessage)
        {
            string logPath = rPath + "\\logs.txt";
            try
            {
                using (StreamWriter w = File.AppendText(logPath))
                {
                    try
                    {
                        w.WriteLine("\n{0} :: {1}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"), logMessage);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unable to log: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to log: " + ex);
            }
        }

        public static void failStep(string msg)
        {
            try { Assert.Fail(""); }
            catch { LogWrite(msg); Console.WriteLine("ERROR: " + msg); }
        }
        public static void KillProcessByName(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    if (!string.IsNullOrWhiteSpace(e.StackTrace))
                        Console.WriteLine(e.StackTrace);
                }
            }
        }

        public static void killdriver(string browsr)
        {
            try
            {
                //if (browsr.Equals("IE"))
                //{ foreach (var proc in Process.GetProcessesByName("IEDriverServer")) { proc.Kill(); } }
                //else if (browsr.Equals("CR"))
                //{ foreach (var proc in Process.GetProcessesByName("chromedriver")) { proc.Kill(); } }
                //else if (browsr.Equals("FF"))
                //{ foreach (var proc in Process.GetProcessesByName("geckodriver")) { proc.Kill(); } }
                //else if (browsr.Equals("EEL"))
                //{ foreach (var proc in Process.GetProcessesByName("MicrosoftWebDriver")) { proc.Kill(); } }
                //else if (browsr.Equals("EEL"))
                //{ foreach (var proc in Process.GetProcessesByName("msedgedriver")) { proc.Kill(); } }
                //else { }
                switch (browsr)
                {
                    case "CR":
                        {
                            Console.WriteLine("Closing Chrome instances on the machine");
                            try { KillProcessByName("chrome"); } catch { }
                            try { KillProcessByName("chromedriver"); } catch { }
                            break;
                        }
                        //case "FF":
                        //    {
                        //        Console.WriteLine("Closing FireFox instances on the machine");
                        //        try { KillProcessByName("Firefox"); } catch { }
                        //        try { KillProcessByName("geckodriver"); } catch { }
                        //        break;
                        //    }
                        //case Browser.InternetExplorer:
                        //    {
                        //        Console.WriteLine("Closing InternetExplorer instances on the machine");
                        //        try { DriverHelper.KillProcessByName("iexplore"); } catch { }
                        //        try { DriverHelper.KillProcessByName("IEDriverServer"); } catch { }
                        //        break;
                        //    }
                        //case Browser.Edge:
                        //    {
                        //        try { DriverHelper.KillProcessByName("MicrosoftEdgeCP"); } catch { }
                        //        try { DriverHelper.KillProcessByName("MicrosoftEdge"); } catch { }
                        //        try { DriverHelper.KillProcessByName("MicrosoftWebDriver.exe"); } catch { }

                        //        break;
                        //    }
                }
            }

            catch
            {

            }
        }


        /*-----Getting and setting config values and also property values------*/
        //Objective: Gets provided config value from appsettings
        public static string getConfigVal(string key)
        {
            string configVal = null;
            try
            {
                configVal = ConfigurationManager.AppSettings[key];
                if (configVal == null)
                    LogIt("Couldn't get '" + key + "' value from DailyCheck_WebAutomation configuration.", logTyp.Error);
                else
                    LogIt("Got '" + key + "' value from DailyCheck_WebAutomation configuration as: " + configVal, logTyp.Logs);
            }
            catch (Exception e) { LogIt("Couldn't get '" + key + "' value from DailyCheck_WebAutomation configuration.:exc:" + e, logTyp.Error); }

            return configVal;
        }
        //Objective: Sets provided config value in appsettings
        public static void SetConfigVal(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove(key);
                config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
                LogIt("Added key: '" + key + "' with value: " + value + " in DailyCheck_WebAutomation configuration", logTyp.Logs);
            }
            catch (Exception e)
            {
                LogIt("Could not add key: '" + key + "' with value: " + value + " in DailyCheck_WebAutomation configuration:exc:" + e, logTyp.Error);
            }
        }
        //Objective: Gets provided property value from DailyCheck_WebAutomation properties
        public static string getProperty(string prop)
        {
            string FileName = pth + "Properties\\Test.properties";
            string key = "NoKey"; string val = "NoValue";
            foreach (string row in File.ReadAllLines(FileName))
            {
                key = row.Split('=')[0].Trim();
                if (key.Equals(prop, StringComparison.OrdinalIgnoreCase))
                {
                    val = row.Replace(key + " = ", "").Trim();
                    break;
                }
            }
            LogIt("Got value" + val + " for Key: " + key + " from Test.properties", logTyp.Logs);
            return val.Contains('#') ? val.Split('#')[0].Trim() : val;
        }
        //Objective: Sets provided property value in DailyCheck_WebAutomation properties
        public static void setProperty(string prop, string value)
        {
            string pth = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string FileName = pth + "Properties\\Test.properties";
            string key = "NoKey"; string val = "NoValue";
            foreach (string row in File.ReadAllLines(FileName))
            {
                key = row.Split('=')[0].Trim();
                if (key.Equals(prop, StringComparison.OrdinalIgnoreCase))
                {
                    val = row;
                    break;
                }
            }
            if (val.Equals("NoValue"))
                File.AppendAllText(FileName, Environment.NewLine + prop + " = " + value);
            else
                File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(val, prop + " = " + value));

            LogIt("Updated " + prop + " = " + value + " in Test.properties", logTyp.Console);
        }

        //Objective: Get map values using key
        public string getMapVal(Dictionary<string, string> dict, string key)
        {
            if (dict != null && dict.ContainsKey(key))
                return dict[key];
            else return null;
        }

        //Timer method
        public static void startTime()
        {
            stopWatch.Restart(); tim = 0;
            LogIt("Timer started:", logTyp.Logs);
        }

        public static void stopTime()
        {
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            tim = (int)ts.TotalSeconds;
            LogIt("Timer stopped at, " + tim + " seconds", logTyp.Logs);
        }

        public string ClickOnElementInNewTab(string eleXP)
        {


            var currentWindow = dr.CurrentWindowHandle;
            try
            {

                dr.SwitchTo().Window(dr.WindowHandles.Last());

                ClickElW(GetElement(eleXP), GetEleLbl(eleXP));
            }
            catch (Exception e)
            {
                // return tile;
            }
            return null;
        }

        public string isThisShownInNewTab(string LocatorValue)
        {


            var currentWindow = dr.CurrentWindowHandle;
            try
            {

                dr.SwitchTo().Window(dr.WindowHandles.Last());

                if (isDisplayed(LocatorValue))
                    LogIt(GetEleLbl(LocatorValue) + " is shown as expected", logTyp.Pass);
                else
                    LogIt(GetEleLbl(LocatorValue) + " is not shown in page", logTyp.Err);
            }
            catch (Exception e)
            {
                // return tile;
            }
            return null;
        }

        public string getSelectedTextInNewTab(string locval)
        {


            var currentWindow = dr.CurrentWindowHandle;
            try
            {

                dr.SwitchTo().Window(dr.WindowHandles.Last());

                return GetSelectedTextW(GetElement(GetEleVal(locval)), GetEleLbl(locval));


            }
            catch (Exception e)
            {
                // return tile;
            }
            finally
            {
                dr.SwitchTo().Window(currentWindow);
            }
            return null;


        }

        public string datePicker()
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("dd-MMM-yyyy");
                return formattedDate;
            }
            catch (Exception e)
            {
                // Handle the exception here
                Console.WriteLine("An error occurred: " + e.Message);
                return null; // or any other appropriate value or action
            }
        }

        public static void actionMethod()
        {
            try
            {
                Actions actions = new Actions(dr);
                actions.MoveByOffset(0, 0).Click().Build().Perform();
            }
            catch (Exception e)
            {
                // Handle the exception here
                Console.WriteLine("An error occurred: " + e.Message);
                // or throw the exception to propagate it to the caller
                throw e;
            }
        }

        public static void uploadFile(string locVal, string textVal)
        {
            try
            {
                GetElement(locVal).SendKeys(textVal);
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element not found: " + e.Message);
            }
        }

        public static void fileUpload(string chooseFile)
        {
            string filePath = pth + "TestData\\" + "SamplePdf.pdf";
            try
            {
                uploadFile(chooseFile, filePath);
                Console.WriteLine("File uploaded successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while uploading the file: " + e.Message);
            }
        }

        public void doubleClick(string dateEle)
        {
           // IWebElement element = dr.FindElement(By.XPath("//html/body/div[10]/div[1]/div[1]/button[2]"));
            var element = GetElement(dateEle);
            Actions actions = new Actions(dr);
            actions.DoubleClick(element).Build().Perform();
        }

        protected static void selectByIndex(string v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
