using CoinStore.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinStore.Test
{
    //internal interface IRepositoryForTests
    //{
    //    static Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

    //}
    //{
    //    public object GetRepository()
    //    {
    //        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
    //        var prodArr = mock.Setup(m => m.Products).Returns((new Product[]
    //        {
    //            new Product{ProductId = Guid.NewGuid(), Name = "P1", Category = "Cat1"},
    //            new Product{ProductId = Guid.NewGuid(), Name = "P2", Category = "Cat2"},
    //            new Product{ProductId = Guid.NewGuid(), Name = "P3", Category = "Cat1"},
    //            new Product{ProductId = Guid.NewGuid(), Name = "P4", Category = "Cat2"},
    //            new Product{ProductId = Guid.NewGuid(), Name = "P5", Category = "Cat3"}
    //        }).AsQueryable<Product>());

    //        return prodArr;
    //    }
    //}
}
