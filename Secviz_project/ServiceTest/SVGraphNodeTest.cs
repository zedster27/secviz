using ServerService.AttackRecognition.DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;

namespace ServiceTest
{
    
    
    /// <summary>
    ///This is a test class for SVGraphNodeTest and is intended
    ///to contain all SVGraphNodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SVGraphNodeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for SVGraphNode Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\NTT\\Desktop\\secviz\\trunk\\Secviz_project\\ServerService", "/")]
        //[UrlToTest("http://localhost:13413/")]
        public void SVGraphNodeConstructorTest()
        {
            SVGraphNode target = new SVGraphNode("type", "id");
            Assert.IsNotNull(target);
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AlertID
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\NTT\\Desktop\\secviz\\trunk\\Secviz_project\\ServerService", "/")]
        //[UrlToTest("http://localhost:13413/")]
        public void AlertIDTest()
        {
            SVGraphNode target = new SVGraphNode("type", "id"); 
            string expected = "id"; // TODO: Initialize to an appropriate value
            string actual;
            target.AlertID = expected;
            actual = target.AlertID;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Type
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\NTT\\Desktop\\secviz\\trunk\\Secviz_project\\ServerService", "/")]
       // [UrlToTest("http://localhost:13413/")]
        public void TypeTest()
        {
            SVGraphNode target = new SVGraphNode("type", "id"); // TODO: Initialize to an appropriate value
            string expected = "type"; // TODO: Initialize to an appropriate value
            string actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddPostNode method
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\NTT\\Desktop\\secviz\\trunk\\Secviz_project\\ServerService", "/")]
        //[UrlToTest("http://localhost:13413/")]
        public void AddPostNodeTest()
        {
            SVGraphNode target = new SVGraphNode("type", "id");
            SVGraphNode tmp = new SVGraphNode("type1", "id1");
            target.AddPostNode(tmp);
            List<SVGraphNode> expected = new List<SVGraphNode>();
            expected.Add(tmp);

            List<SVGraphNode>  actual = target.PostNodes;
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var item in actual)
            {
                Assert.IsTrue(expected.Contains(item));
            }
        }
    }
}
