using NUnit.Framework;

[TestFixture]
public class Test08
{
    [TestCase]
    public void Count_Should_Be_0_On_EmptyCollection()
    {
        //Arrange
        IChainblock cb = new Chainblock();
        //Act
        //Assert
        Assert.AreEqual(0, cb.Count);
    }
}