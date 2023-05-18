namespace Cccat10RefactoringDomain.Usecases;

public class CreateOrderCode
{
    public string Execute(int orderId) => $"{DateTime.Now.Year}{orderId}";
}
