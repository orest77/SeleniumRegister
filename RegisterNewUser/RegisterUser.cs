using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;


namespace RegisterNewUser
{
    [TestFixture]
    public class RegisterUser
    {
        IWebDriver driver;
        public string Email { get; set; }
        


        const string URL = "http://atqc-shop.epizy.com/";
        const string URL_LOGOUT = "http://atqc-shop.epizy.com/index.php?route=account/logout";
        const string URL_HOME = "http://atqc-shop.epizy.com/index.php?route=common/home";

        [OneTimeSetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            
        }

        [SetUp]
        public void SetUp()
        {                        
            driver.Navigate().GoToUrl(URL);
        }

        [TearDown]
        public void Logout()
        {
            driver.Navigate().GoToUrl(URL_LOGOUT);
            driver.Navigate().GoToUrl(URL_HOME);
            //driver.FindElement(By.CssSelector(".account-logout a.btnbtn-primary")).Click();

        }


        [OneTimeTearDown]
        public void CloseDriver()
        {
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Quit();
        }

        [Test, Order(0)]
        [TestCase("Orest", "Shkhumov", "orest123@gmail.com", "0637239610", "04532354", "Soft", "Fedkovicha",
            "Sadova", "Lviv", "64738", "Ukraine", "L'vivs'ka Oblast'", "orest", "orest")]
        public void RegisterTests1(string FirstName, string LastName, string EMail, string Telephone, string Fax,
            string Company, string Address1, string Address2, string City, string PostCode, string Country,
            string Region, string Password, string PasswordConfirm)
        {
            //Click button Register
            driver.FindElement(By.CssSelector("#top-links .dropdown .fa.fa-user")).Click();
            driver.FindElement(By.XPath("//a[contains(@href,'/register')]")).Click();
            Thread.Sleep(2000);
            //Verify page
            Assert.AreEqual("login page", driver.FindElement(By.CssSelector("#content p a")).Text);

            //Enter data in empty fields
            driver.FindElement(By.Id("input-firstname")).SendKeys(FirstName);
            driver.FindElement(By.Id("input-lastname")).SendKeys(LastName);          
            driver.FindElement(By.Id("input-email")).SendKeys(EMail);            
            driver.FindElement(By.Id("input-telephone")).SendKeys(Telephone);
            driver.FindElement(By.Id("input-fax")).SendKeys(Fax);
            driver.FindElement(By.Id("input-company")).SendKeys(Company);
            driver.FindElement(By.Id("input-address-1")).SendKeys(Address1);
            driver.FindElement(By.Id("input-address-2")).SendKeys(Address2);
            driver.FindElement(By.Id("input-city")).SendKeys(City);
            driver.FindElement(By.Id("input-postcode")).SendKeys(PostCode);
            driver.FindElement(By.Id("input-country")).SendKeys(Country);
            driver.FindElement(By.Id("input-zone")).SendKeys(Region);
            driver.FindElement(By.Id("input-password")).SendKeys(Password);
            driver.FindElement(By.Id("input-confirm")).SendKeys(PasswordConfirm);
            //Subscribe
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.ClassName("radio-inline")), 1, 1).Click().Perform();
            //I have read and agree to the Privacy Policy
            Actions action2 = new Actions(driver);
            action2.MoveToElement(driver.FindElement(By.Name("agree")), 1, 1).Click().Perform();
            //Clik Continue button
            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Submit();
            //Verify
            Assert.AreEqual("Your Account Has Been Created!", driver.FindElement(By.CssSelector("#content h1")).Text);
            //Assert.IsTrue(driver.FindElement(By.ClassName(".buttons.pull-right")).GetAttribute("href").Contains("account"));
            //Clik Continue button
            driver.FindElement(By.CssSelector("a.btn.btn-primary")).Click();
            //Thread.Sleep(2000);

        }

        [Test, Order(1)]
        [TestCase("orest123@gmail.com", "orest")]
        public void EditAccountForExistUser(string email, string password)
        {
            //Click button login
            driver.FindElement(By.CssSelector("#top-links .dropdown .fa.fa-user")).Click();
            driver.FindElement(By.XPath("//a[contains(@href,'/login')]")).Click();
            //Input date
            driver.FindElement(By.Id("input-email")).Clear();
            driver.FindElement(By.Id("input-email")).SendKeys(email);
            driver.FindElement(By.Id("input-password")).Clear();
            driver.FindElement(By.Id("input-password")).SendKeys(password);
            //Button login
            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            //Ckik Edit account
            driver.FindElement(By.XPath("//a[contains(@href,'/edit')]")).Click();
            //Input new data
            driver.FindElement(By.Id("input-firstname")).Clear();
            driver.FindElement(By.Id("input-firstname")).SendKeys("Bogdan");
            driver.FindElement(By.Id("input-lastname")).Clear();
            driver.FindElement(By.Id("input-lastname")).SendKeys("Dovagan");
            driver.FindElement(By.Id("input-email")).Clear();
            //Add new email
            TenMinEmail mail = new TenMinEmail();
            driver.FindElement(By.Id("input-email")).SendKeys(mail.ObtainEmailBox());
            driver.FindElement(By.Id("input-telephone")).Clear();
            driver.FindElement(By.Id("input-telephone")).SendKeys("+380637239615");
            // Button
            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            //Assert
            Assert.IsTrue(driver.FindElement(By.CssSelector("div.alert.alert-success")).Displayed);

        }
    }
}
