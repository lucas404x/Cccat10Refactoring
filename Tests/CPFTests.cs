using Cccat10RefactoringCode.Models;
using Cccat10RefactoringCode.Utils;

namespace Cccat10RefactoringTests;

public class CPFTests
{
    [TestCase("96886806483")]
    [TestCase("389.163.174-01")]
    [TestCase("132224.65053")]
    public void CreateCPF_ValidValue_ReturnsCPFWithIsValidAsTrue(string cpf)
    {
        var actual = new CPF(cpf);
        Assert.That(actual.Value, Is.EqualTo(CPFUtils.RemoveMask(cpf)));
        Assert.That(actual.IsValid, Is.True);
    }

    [TestCase("")]
    [TestCase("102.212-21")]
    [TestCase("9686806483")]
    [TestCase("32916317401")]
    [TestCase("132224.65013")]
    public void CreateCPF_InvalidValue_ReturnsCPFWithIsValidAsFalse(string cpf)
    {
        var actual = new CPF(cpf);
        Assert.That(actual.Value, Is.EqualTo(CPFUtils.RemoveMask(cpf)));
        Assert.That(actual.IsValid, Is.False);
    }
}