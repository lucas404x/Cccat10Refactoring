namespace Cccat10RefactoringDomain.Usecases;

public class CreateOrderCode
{
    public static string Execute(Guid orderId)
    {
        string id = orderId.ToString().Replace("-", "").ToUpper();
        return $"{DateTime.Now.Year}{id}";
    }
}
