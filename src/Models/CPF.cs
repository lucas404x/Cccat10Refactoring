using Cccat10RefactoringCode.Utils;

namespace Cccat10RefactoringCode.Models;

public class CPF
{
    public static readonly int CPF_LENGTH = 11;
    public static readonly int HIGHEST_WEIGHT = 12;

    public string Value { get; init; }
    public bool IsValid { get; init; }

    public CPF(string value)
    {
        Value = CPFUtils.RemoveMask(value);
        IsValid = CPFUtils.IsValid(Value);
    }

    public override string ToString() => Value;
}