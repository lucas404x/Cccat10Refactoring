using Cccat10RefactoringDomain.Utils;

namespace Cccat10RefactoringDomain.Models;

public class CPF
{
    private const int CPF_LENGTH = 11;
    private const int HIGHEST_WEIGHT = 12;

    public string Value { get; private set; }

    public CPF(string value)
    {
        Value = CPFUtils.RemoveMask(value);
    }

    public bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(Value))
            return false;

        if (Value.Length != CPF_LENGTH)
            return false;

        if (AreAllDigitsTheSame(Value))
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

    private static bool AreAllDigitsTheSame(string cpf)
    {
        var firstDigit = cpf[..1];
        return cpf.Split("").All(digit => digit == firstDigit);
    }

    private static int MultiplyDigitByWeight(int currentDigit, int factor, int highestWeight)
    {
        int currentCheckerDigitWeight = highestWeight - factor;
        return currentDigit * currentCheckerDigitWeight;
    }

    private static int FindCheckerDigit(int checkerDigitWeight)
    {
        int checkerDigitReminder = checkerDigitWeight % CPF.CPF_LENGTH;
        return checkerDigitReminder < 2 ? 0 : CPF.CPF_LENGTH - checkerDigitReminder;
    }

    public override string ToString() => Value;
}