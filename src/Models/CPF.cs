using Cccat10RefactoringCode.Utils;

namespace Cccat10RefactoringCode.Models;

public class CPF
{
    private const int CPF_LENGTH = 11;
    private const int HIGHEST_WEIGHT = 12;

    public string Value { get; init; }
    public bool IsValid { get; init; }

    public CPF(string value)
    {
        Value = CPFUtils.RemoveMask(value);
        IsValid = ValidateCPF();
    }

    private bool ValidateCPF()
    {
        if (string.IsNullOrWhiteSpace(Value))
            return false;

        if (Value.Length != CPF_LENGTH)
            return false;

        if (AreAllDigitsTheSame())
            return false;

        int firstCheckerDigitWeight = 0;
        int secondCheckerDigitWeight = 0;
        for (int factor = 1; factor < Value.Length - 1; factor++)
        {
            int currentDigit = int.Parse(Value[factor - 1].ToString());
            firstCheckerDigitWeight += MultiplyDigitByWeight(currentDigit, factor, CPF_LENGTH);
            secondCheckerDigitWeight += MultiplyDigitByWeight(currentDigit, factor, HIGHEST_WEIGHT);
        }
        int firstCheckerDigit = FindCheckerDigit(firstCheckerDigitWeight); 
        secondCheckerDigitWeight += 2 * firstCheckerDigit;
        int secondCheckerDigit = FindCheckerDigit(secondCheckerDigitWeight);
        
        var actualCheckerDigits = Value.Substring(Value.Length - 2);
        var expectedCheckerDigits = $"{firstCheckerDigit}{secondCheckerDigit}";
        return expectedCheckerDigits == actualCheckerDigits;
    }

    private bool AreAllDigitsTheSame()
    {
        var firstDigit = Value.Substring(0, 1);
        return Value.Split("").All(digit => digit == firstDigit);
    }

    private int MultiplyDigitByWeight(int currentDigit, int factor, int highestWeight)
    {
        int currentCheckerDigitWeight = highestWeight - factor;
        return currentDigit * currentCheckerDigitWeight;
    }

    private int FindCheckerDigit(int checkerDigitWeight)
    {
        int checkerDigitReminder = checkerDigitWeight % CPF_LENGTH;
        return checkerDigitReminder < 2 ? 0 : CPF_LENGTH - checkerDigitReminder;
    }
}