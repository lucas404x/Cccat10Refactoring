namespace Cccat10RefactoringAPI.Models;

public class ApiResponse<T>
{
    public T? Result { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
