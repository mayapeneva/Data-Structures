using NUnit.Framework;

[TestFixture]
internal class OrgTests7
{
    [Test]
    public void ContainsByName_NonExistingName_ShouldReturnFalse()
    {
        // Arrange
        IOrganization org = new Organization();

        // Act
        bool actual = org.ContainsByName("Ivan");

        // Assert
        Assert.False(actual);
    }
}