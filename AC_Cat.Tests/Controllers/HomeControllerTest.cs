using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AC_Cat;
using AC_Cat.Controllers;
using AC_Cat.ViewModels;

namespace AC_Cat.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        //httpget
        [TestMethod]
        public void WHEN_pristine_THEN_viewresult_is_not_null()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
