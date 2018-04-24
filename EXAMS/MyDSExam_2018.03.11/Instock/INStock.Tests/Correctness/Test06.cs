using NUnit.Framework;

[TestFixture]
public class Test06
{
    [TestCase]
    public void Contains_On_Empty_Collection_ShouldReturnFalse()
    {
        //Arrange
        IProductStock stock = new Instock();
        //Act
        //Assert
        Assert.IsFalse(stock.Contains(new Product("Bakar", 5.5, 15)));
    }
}