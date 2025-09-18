using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.API.Test
{
    [TestClass]
    public class FundsTests
    {
        [TestMethod]
        public void GetAllAsync_ReturnsFundDetails ()
        {
            // Arrange
            var mockService = new MockExternalService();
            var funds = new Funds(mockService);
            // Act
            var result = funds.GetFundDetails(null);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, ((System.Collections.ICollection)result).Count);
        }
    }
}
