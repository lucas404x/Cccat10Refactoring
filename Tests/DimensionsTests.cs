using Cccat10RefactoringCode.Models;

namespace Cccat10RefactoringTests;

public class DimensionsTests
{
    [Test]
    public void Dimensions_GetCalculatedVolume_ReturnsResult()
    {
        var d = Dimensions.FromCentimers(20, 15, 10);
        var actual = d.Volume;
        Assert.That(actual, Is.EqualTo(0.003));
    }

    [TestCase(-20, 30, 10)]
    [TestCase(20, -30, 10)]
    [TestCase(20, 30, -10)]
    public void CreateDimensions_AnyNegativeValue_ThrowsException(double width, double height, double length) 
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Dimensions.FromCentimers(width, height, length));
    }
}