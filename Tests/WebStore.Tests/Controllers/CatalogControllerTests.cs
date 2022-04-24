﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.DTO;
using WebStore.Interfaces.Services.DTO;
using WebStore.ViewModels;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers;

[TestClass]
public class CatalogControllerTests
{

    [TestMethod]
    public void Details_Returns_with_View()
    {
        const int expected_id = 42;
        const string expected_name = "Test product";
        const int expected_order = 5;
        const decimal expected_price = 13.5m;
        const string expected_img_url = "/img/product.img";

        const int expected_brand_id = 7;
        const string expected_brand_name = "Test brand";
        const int expected_brand_order = 10;

        const int expected_section_id = 14;
        const string expected_section_name = "Test section";
        const int expected_section_order = 123;

        var products_data_mock = new Mock<IProductDTOData>();
        products_data_mock.Setup(s => s.GetProductById(It.IsAny<int>()))
            .Returns<int>(id => new ProductDTO()
            {
                Id = id,
                Name = expected_name,
                Order = expected_order,
                Price = expected_price,
                ImageUrl = expected_img_url,
                Brand = new()
                {
                    Id = expected_brand_id,
                    Name = expected_brand_name,
                    Order = expected_brand_order,
                },
                Section = new()
                {
                    Id = expected_section_id,
                    Name = expected_section_name,
                    Order = expected_section_order,
                }
            });

        var controller = new CatalogController(products_data_mock.Object);

        var result = controller.Details(expected_id);

        var view_result = Assert.IsType<ViewResult>(result);

        var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

        Assert.Equal(expected_id, model.Id);
        Assert.Equal(expected_name, model.Name);
        Assert.Equal(expected_price, model.Price);
        Assert.Equal(expected_img_url, model.ImageUrl);
        //Assert.Equal(expected_brand_name, model.Brand);
        //Assert.Equal(expected_section_name, model.Section);

        products_data_mock.Verify(s => s.GetProductById(It.Is<int>(id => id == expected_id)));
        products_data_mock.VerifyNoOtherCalls();
    }
}
