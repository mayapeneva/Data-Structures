using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class Test16
{
    [TestCase]
    public void FindFirstByAlphabeticalOrder_On_EmptyStock_ShouldReturn_EmptyEnumeration()
    {
        //Arrange
        IProductStock stock = new Instock();
        //Act
        //Assert
        CollectionAssert.AreEqual(new List<Product>(), stock.FindFirstByAlphabeticalOrder(0));
    }
}