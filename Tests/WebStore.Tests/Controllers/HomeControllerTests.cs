using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Controllers;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.DTO;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.Services.DTO;
using WebStore.ViewModels;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers;


[TestClass]
public class HomeControllerTests
{

    [TestMethod]
    public void ConfigStr_Returns_ContentString()
    {
        #region Arrange
        const string expected_value = "test-value";
        const string expected_content = $"config: {expected_value}";

        var values = new Dictionary<string, string>()
        {
            {"ServerGreetings", expected_value },
        };
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(values)
            .Build();

        var controller = new HomeController(config);
        #endregion

        #region Act
        var result = controller.ConfigStr();
        #endregion

        #region Assert

        var content_result = Assert.IsType<ContentResult>(result);

        var actual_content = content_result.Content;

        Assert.Equal(expected_content, actual_content);

        #endregion
    }


    [TestMethod]
    public void ContentString_Thrown_ArgumentNullException_When_Id_is_null_1()
    {

        var configuration_mock = new Mock<IConfiguration>();
        var controller = new HomeController(configuration_mock.Object);

        try
        {
            _ = controller.ContentString(null);
            throw new AssertFailedException("Не было сгенерировано исключение");
        }
        catch (ArgumentNullException exception)
        {
            Assert.Equal("Id", exception.ParamName);
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ContentString_Thrown_ArgumentNullException_When_Id_is_null_2()
    {

        var configuration_mock = new Mock<IConfiguration>();
        var controller = new HomeController(configuration_mock.Object);

        controller.ContentString(null);
    }

    [TestMethod]
    public void ContentString_Thrown_ArgumentNullException_When_Id_is_null_3()
    {

        var configuration_mock = new Mock<IConfiguration>();
        var controller = new HomeController(configuration_mock.Object);

        var exception = Assert.Throws<ArgumentNullException>(() => controller.ContentString(null!));
        Assert.Equal("Id",exception.ParamName);
    }

    [TestMethod]
    public void Index_Returns_with_View()
    {
        var products = Enumerable.Range(1, 10)
            .Select(i => new ProductDTO
            {
                Id = i,
                Order = i,
                Name = $"Product-{i}",
                Price = i * 1000 - i,
                Section = new SectionDTO
                {
                    Id=i,
                    Name = $"Section-{i}",
                },
                ImageUrl = $"Image-{i}",
            })
            .ToArray();

        var configuration_mock = new Mock<IConfiguration>();
        var product_data_mock = new Mock<IProductDTOData>();
        product_data_mock.Setup(s => s.GetProducts(It.IsAny<ProductFilter>()))
            .Returns(products);

        var controller = new HomeController(configuration_mock.Object);

        var result = controller.Index(product_data_mock.Object);

        var view_result = Assert.IsType<ViewResult>(result);
        var product_obj = view_result.ViewData["Products"];

        var products_view_models = Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(product_obj);

        Assert.Equal(6, products_view_models.Count());
    }
}
