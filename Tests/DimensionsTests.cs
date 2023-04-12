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
}