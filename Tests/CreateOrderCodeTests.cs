using Cccat10RefactoringDomain.Usecases;

namespace Cccat10RefactoringTests;

public class CreateOrderCodeTests
{
    [Test]
    public void CreateOrderCode_Execute_ReturnsExpectedCode()
    {
        var createOrderCode = new CreateOrderCode();
        var actual = createOrderCode.Execute(9421);
        Assert.That(actual, Is.EqualTo($"{DateTime.Now.Year}9421"));
    }
}