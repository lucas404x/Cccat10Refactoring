using Cccat10RefactoringCode.Models;

namespace Cccat10RefactoringCode.Utils;

public static class CPFUtils 
{
    public static string RemoveMask(string cpf) 
        => cpf.Replace(".", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty);

    public static bool IsValid(string value)
    {
        var cpf = RemoveMask(value);

        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        if (cpf.Length != CPF.CPF_LENGTH)
            return false;

        if (AreAllDigitsTheSame(cpf))
            return false;

        int firstCheckerDigitWeight = 0;
        int secondCheckerDigitWeight = 0;
        for (int factor = 1; factor < cpf.Length - 1; factor++)
        {
            int currentDigit = int.Parse(cpf[factor - 1].ToString());
            firstCheckerDigitWeight += MultiplyDigitByWeight(currentDigit, factor, CPF.CPF_LENGTH);
            secondCheckerDigitWeight += MultiplyDigitByWeight(currentDigit, factor, CPF.HIGHEST_WEIGHT);
        }
        int firstCheckerDigit = FindCheckerDigit(firstCheckerDigitWeight); 
        secondCheckerDigitWeight += 2 * firstCheckerDigit;
        int secondCheckerDigit = FindCheckerDigit(secondCheckerDigitWeight);
        
        var actualCheckerDigits = cpf.Substring(cpf.Length - 2);
        var expectedCheckerDigits = $"{firstCheckerDigit}{secondCheckerDigit}";
        return expectedCheckerDigits == actualCheckerDigits;
    }

    private static bool AreAllDigitsTheSame(string cpf)
    {
        var firstDigit = cpf.Substring(0, 1);
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
}