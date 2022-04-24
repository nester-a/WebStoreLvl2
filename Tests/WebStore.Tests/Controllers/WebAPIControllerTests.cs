﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebStore.Controllers;
using WebStore.Interfaces.TestAPI;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers;

[TestClass]
public class WebAPIControllerTests
{

    [TestMethod]
    public void Index_Returns_with_View_with_values()
    {
        var expected_values = Enumerable.Range(1, 10).Select(i => $"Values-{i}").ToArray();

        var values_service_mock = new Mock<IValuesService>();
        values_service_mock.Setup(s => s.GetValues()).Returns(expected_values);

        var controller = new WebAPIController(values_service_mock.Object);

        var result = controller.Index();

        var view_result = Assert.IsType<ViewResult>(result);

        var actual_values = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

        Assert.Equal(expected_values, actual_values);

        values_service_mock.Verify(s => s.GetValues());
        values_service_mock.VerifyNoOtherCalls();
    }
}
