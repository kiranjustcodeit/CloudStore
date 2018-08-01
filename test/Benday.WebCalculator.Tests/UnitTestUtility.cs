using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.WebCalculator.Tests
{
    public static class UnitTestUtility
    {
        public static T GetModel<T>(IActionResult actionResult) where T : class
        {
            var asViewResult = actionResult as ViewResult;

            return asViewResult.Model as T;
        }

        public static void AssertIsHttpNotFound(IActionResult actionResult)
        {
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
