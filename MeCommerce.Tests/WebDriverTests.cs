using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MeCommerce.Tests
{
    /// <summary>
    /// Summary description for ProductViewTests
    /// </summary>
    [TestClass]
    public class WebDriverTests
    {
        private IWebDriver _driver;
        private string testUrl = "https://fypme.azurewebsites.net/";

        [TestInitialize]
        public void Init()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(testUrl);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void WebDriver_TestViewReturnsCorrectProduct()
        {
            _driver.FindElement(By.PartialLinkText("Browse All")).Click();
            string name = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > h3:nth-child(3)")).Text;
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(7) > a")).Click();
            string displayName = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > div > h2")).Text;
            Assert.AreEqual(name, displayName);
        }

        [TestMethod]
        public void WebDriver_TestViewReturnsCorrectBrand()
        {
            _driver.FindElement(By.PartialLinkText("Brands")).Click();
            string name = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > h3:nth-child(3)")).Text + " Products";
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(6) > a")).Click();
            string displayName = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > div.row > div > h2")).Text;
            Assert.AreEqual(name, displayName);
        }

        [TestMethod]
        public void WebDriver_TestViewReturnsCorrectCategory()
        {
            _driver.FindElement(By.PartialLinkText("Categories")).Click();
            string name = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > h3:nth-child(3)")).Text;
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(6) > a")).Click();
            string displayName = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > div.row > div > h2")).Text;
            Assert.AreEqual(name, displayName);
        }

        [TestMethod]
        public void WebDriver_TestPhonesNavbar()
        {
            _driver.FindElement(By.PartialLinkText("Phones")).Click();
            string actual = _driver.Url;
            string expected = "https://fypme.azurewebsites.net/Category/Details/1";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestTabletsNavbar()
        {
            _driver.FindElement(By.PartialLinkText("Tablets")).Click();
            string actual = _driver.Url;
            string expected = "https://fypme.azurewebsites.net/Category/Details/4";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestCategoryDrilldown()
        {
            _driver.FindElement(By.PartialLinkText("Categories")).Click();
            string expected = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > h3:nth-child(3)")).Text;
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(6) > a")).Click();
            string actual = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > div.row > div > h2")).Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestBrandDrilldown()
        {
            _driver.FindElement(By.PartialLinkText("Brands")).Click();
            string expected = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > h3:nth-child(3)")).Text + " Products";
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(6) > a")).Click();
            string actual = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > div.row > div > h2")).Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestMenuJQuery()
        {
            _driver.FindElement(By.CssSelector("body > main > div.navbar > div > div.navbar-header > a.hamburger > div > div.innerNavicon")).Click();
            string expected = "My Store";
            string actual = _driver.FindElement(By.CssSelector("#default > h3")).Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestCartJQuery()
        {
            _driver.FindElement(By.CssSelector("#carticon")).Click();
            string expected = "Your basket is empty :(";
            string actual = _driver.FindElement(By.CssSelector("#basket-panel > div > h4")).Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestSearchButton()
        {
            _driver.FindElement(By.CssSelector("#search-button")).Click();
            string expected = "Search Results";
            string actual = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > h2")).Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestSeeMoreOfSameCategoryMethod()
        {
            _driver.FindElement(By.PartialLinkText("Browse All")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(7) > a")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(4) > a")).Click();
            var expected = "https://fypme.azurewebsites.net/Category/Details/1";
            var actual = _driver.Url;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestSeeMoreofSameBrandMethod()
        {
            _driver.FindElement(By.PartialLinkText("Browse All")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(7) > a")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(5) > a")).Click();
            var expected = "https://fypme.azurewebsites.net/Brand/Details/1";
            var actual = _driver.Url;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestAddProductToCartMethod()
        {
            _driver.FindElement(By.PartialLinkText("Browse All")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(7) > a")).Click();
            var expected = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > div > h2")).Text;
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(2) > a")).Click();
            _driver.FindElement(By.CssSelector("#logo")).Click();
            _driver.FindElement(By.CssSelector("#carticon")).Click();
            var actual = _driver.FindElement(By.CssSelector("#basket-panel > div > table > tbody > tr:nth-child(2) > td:nth-child(1)")).Text;
            _driver.FindElement(By.CssSelector("#basket-panel > div > a:nth-child(9)")).Click();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestAddMultipleProductToCartMethod()
        {
            _driver.FindElement(By.PartialLinkText("Browse All")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(7) > a")).Click();
            var expectedProd1 = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > div > h2")).Text;
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(2) > a")).Click();
            _driver.FindElement(By.CssSelector("#logo")).Click();

            _driver.FindElement(By.PartialLinkText("Browse All")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(14) > a")).Click();
            var expectedProd2 = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > div > h2")).Text;
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(2) > a")).Click();
            _driver.FindElement(By.CssSelector("#logo")).Click();

            _driver.FindElement(By.CssSelector("#carticon")).Click();

            var actualProd1 = _driver.FindElement(By.CssSelector("#basket-panel > div > table > tbody > tr:nth-child(2) > td:nth-child(1)")).Text;
            var actualProd2 = _driver.FindElement(By.CssSelector("#basket-panel > div > table > tbody > tr:nth-child(3) > td:nth-child(1)")).Text;

            _driver.FindElement(By.CssSelector("#basket-panel > div > a:nth-child(9)")).Click();

            Assert.AreEqual(expectedProd1, actualProd1);
            Assert.AreEqual(expectedProd2, actualProd2);
        }

        [TestMethod]
        public void WebDriver_TestClearCartMethod()
        {
            _driver.FindElement(By.PartialLinkText("Browse All")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(7) > a")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(2) > a")).Click();
            _driver.FindElement(By.CssSelector("#logo")).Click();
            _driver.FindElement(By.CssSelector("#carticon")).Click();
            _driver.FindElement(By.CssSelector("#basket-panel > div > a:nth-child(9)")).Click();
            _driver.FindElement(By.CssSelector("#logo")).Click();
            _driver.FindElement(By.CssSelector("#carticon")).Click();
            var expected = "Your basket is empty :(";
            var actual = _driver.FindElement(By.CssSelector("#basket-panel > div > h4")).Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestAddQuantityCartMethod()
        {
            _driver.FindElement(By.PartialLinkText("Browse All")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(7) > a")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(2) > a")).Click();
            _driver.FindElement(By.CssSelector("#logo")).Click();
            _driver.FindElement(By.CssSelector("#carticon")).Click();
            _driver.FindElement(By.CssSelector("#basket-panel > div > table > tbody > tr:nth-child(2) > td:nth-child(2) > a:nth-child(2)")).Click();
            _driver.FindElement(By.CssSelector("#logo")).Click();
            _driver.FindElement(By.CssSelector("#carticon")).Click();
            var expected = "- 2 +";
            var actual = _driver.FindElement(By.CssSelector("#basket-panel > div > table > tbody > tr:nth-child(2) > td:nth-child(2)")).Text;

            _driver.FindElement(By.CssSelector("#basket-panel > div > a:nth-child(9)")).Click();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebDriver_TestCheckoutView()
        {
            _driver.FindElement(By.PartialLinkText("Browse All")).Click();
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(7) > a")).Click();
            var expected = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > div > h2")).Text;
            _driver.FindElement(By.CssSelector("body > main > div.container.body-content > p:nth-child(2) > a")).Click();
            _driver.FindElement(By.CssSelector("#logo")).Click();
            _driver.FindElement(By.CssSelector("#carticon")).Click();
            _driver.FindElement(By.CssSelector("#basket-panel > div > a:nth-child(3)")).Click();
            var actual = _driver.FindElement(By.CssSelector("body > main > div.container.body-content > table > tbody > tr:nth-child(2) > td:nth-child(1)")).Text;

            _driver.FindElement(By.CssSelector("#logo")).Click();
            _driver.FindElement(By.CssSelector("#carticon")).Click();
            _driver.FindElement(By.CssSelector("#basket-panel > div > a:nth-child(9)")).Click();
            Assert.AreEqual(expected, actual);
        }
    }
}