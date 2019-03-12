using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        private string siteUrl = "https://www.citrus.ua";
        private ChromeDriver driver = null;
        private string searchFieldSelector = "#multisearch";
        private string textMacBookSelector = ".catalog-card-container";

        private string buttonTVSelector = "ul.h-nav>li>a[title = 'TV']";
        private string buttonLGLocator = "//*[@id='__layout']//div[3]//li[4]/label/span[2]/span[1]";
        private string textLGSelector = ".roww catalog-roww";

        private string nameOfFirstTVProductSelector = ".catalog-card-container:nth-child(5) div.title-itm>h5";
        private string priceOfFirstTVProductSelector = ".catalog-card-container:nth-child(5) span.base-price";

        private string filtersContainerSelector = ".filter-itm>h3";

        [SetUp]
        public void SetUp()
        {
            this.driver = new OpenQA.Selenium.Chrome.ChromeDriver { Url = this.siteUrl };
            this.driver.Manage().Window.Maximize();
        }

        [Test]
        public void FirstTest()
        {
            IWebElement searchFieldElement = this.driver.FindElement(By.CssSelector(this.searchFieldSelector));
            searchFieldElement.SendKeys("macbook");
            searchFieldElement.Submit();

            List<IWebElement> elements = driver.FindElements(By.CssSelector(this.textMacBookSelector)).ToList();

            var macBookInList = elements.Select(e => e.Text.ToLower()).ToList();
            var expected = macBookInList.All(e => e.Contains("macbook"));

            Assert.IsTrue(expected, $"List contains {macBookInList}");
        }

        [Test]
        public void SecondTest()
        {
            IWebElement buttonTVElement = this.driver.FindElement(By.CssSelector(this.buttonTVSelector));
            buttonTVElement.Click();
            Thread.Sleep(4000);

            IWebElement buttonLGElement = this.driver.FindElement(By.XPath(this.buttonLGLocator));
            buttonLGElement.Click();
            Thread.Sleep(4000);

            List<IWebElement> elements = driver.FindElements(By.CssSelector(this.textLGSelector)).ToList();

            var textLGInList = elements.Select(e => e.Text.ToLower()).ToList();
            var expected1 = textLGInList.All(e => e.Contains("LG"));

            Assert.IsTrue(expected1, $"List contains {textLGInList}");
        }

        [Test]
        public void ThirdTest()
        {
            IWebElement buttonTVElement = this.driver.FindElement(By.CssSelector(this.buttonTVSelector));
            buttonTVElement.Click();
            Thread.Sleep(4000);

            IWebElement nameOfFirstTVProductElement = this.driver.FindElement(By.CssSelector(this.nameOfFirstTVProductSelector));
            bool nameIsDisplayed = nameOfFirstTVProductElement.Displayed;

            IWebElement priceOfFirstTVProductElement = this.driver.FindElement(By.CssSelector(this.priceOfFirstTVProductSelector));
            bool priceIsDisplayed = priceOfFirstTVProductElement.Displayed;

            nameOfFirstTVProductElement.Click();
        }

        [Test]
        public void FourthTest()
        {
            IWebElement buttonTVElement = this.driver.FindElement(By.CssSelector(this.buttonTVSelector));
            buttonTVElement.Click();
            Thread.Sleep(4000);

            List<IWebElement> filterListElements = this.driver.FindElements(By.CssSelector(this.filtersContainerSelector)).ToList();
            List<string> actualFiltersList = filterListElements.Select(e => e.Text).ToList();

            List<string> expextedFiltersList = new List<string>()
            {
                "Цена",
                "Акции и скидки",
                "Бренд",
                "Диагональ",
                "Разрешение",
                "Тип телевизора",
                "Smart TV",
                "Поддержка 3D",
                "Изогнутый экран",
                "Суммарная мощность динамиков",
                "Операционная система"
            };

            Assert.AreEqual(expextedFiltersList, actualFiltersList);
        }

        [TearDown]
        public void TestTearDown()
        {
            this.driver.Quit();
        }
    }
}

