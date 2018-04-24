using NUnit.Framework;
using System;

[TestFixture]
internal class OrgTests14
{
    [Test]
    public void GetAtIndex_OnNonExistingIndex_ShouldThrow()
    {
        // Arrange
        IOrganization org = new Organization();

        // Act
        Person GetAtIndex() => org.GetAtIndex(3);

        // Assert
        Assert.That(GetAtIndex, Throws.InstanceOf<IndexOutOfRangeException>());
    }
}