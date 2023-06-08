using Cccat10RefactoringDomain.Models;
using Cccat10RefactoringDomain.ValueObjects;

namespace Cccat10RefactoringTests;

public abstract class SharedState
{
    // https://www.geradorcpf.com/
    protected readonly CPF validCPF = new("68205913226");
    protected readonly CPF invalidCPF = new("68005913226");

    protected readonly Coupon validCoupon = new("VALE20", 10, DateTime.UtcNow.AddDays(10));
    protected readonly Coupon invalidCoupon = new("VALE20", 10, DateTime.UtcNow.AddDays(-1));
}
