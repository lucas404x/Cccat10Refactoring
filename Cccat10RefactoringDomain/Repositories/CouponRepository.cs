﻿using Cccat10RefactoringDomain.Models;

namespace Cccat10RefactoringDomain.Repositories;

public interface ICouponRepository
{
    public Task<Coupon?> GetCouponAsync(Guid id);
    public Task<Coupon> CreateCouponAsync(Coupon coupon);
}
