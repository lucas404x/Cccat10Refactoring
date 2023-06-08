using Cccat10RefactoringDomain.Utils;

namespace Cccat10RefactoringDomain.ValueObjects;

public class CPF : ValueObject
{
    private const int CPF_LENGTH = 11;
    private const int HIGHEST_WEIGHT = 12;

    private string _value;
    public string Value
    {
        get => _value;
        set => _value = CPFUtils.RemoveMask(value);
    }

    public CPF(string value)
    {
        _value = CPFUtils.RemoveMask(value);
    }

    public override bool IsValid()
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
