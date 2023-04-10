namespace Cccat10RefactoringCode.Utils;

public static class CPFUtils 
{
    public static string RemoveMask(string cpf) 
        => cpf.Replace(".", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty);
}
