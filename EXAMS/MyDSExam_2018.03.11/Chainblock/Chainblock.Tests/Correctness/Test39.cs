using NUnit.Framework;
using System;

[TestFixture]
public class Test39
{
    [TestCase]
    public void GetBySenderAndMinimumAmountDescending_ShouldThrowOnEmpty_CB()
    {
        //Arrange
        IChainblock cb = new Chainblock();
        //Act
        //Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            cb.GetBySenderAndMinimumAmountDescending("pesho", 5);
        });
    }
}