using Entities;
using System;
using System.Collections.Generic;
using Utilities;
using Xunit;

namespace Tests
{
    public class ValidationTests
    {
        [Fact]
        public void TestIfCanInitializeOrder()
        {
            // Arrange
            OrderDetail detail = new OrderDetail(1, 12, 65, 1);
            Order order = default;

           // Assert
           //order = new Order(1, new DateTime(1991, 12, 19), new DateTime(1991, 11, 19), new DateTime(1991, 06, 19), 3, 192, "autist", "ok", "p", "vejle", "928", "adanmark", new List<OrderDetail>() { detail });

        }
    }
}
