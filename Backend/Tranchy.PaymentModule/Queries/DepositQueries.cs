using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Tranchy.Common.HotChocolate;
using Tranchy.PaymentModule.Data;

namespace Tranchy.PaymentModule.Queries;

[QueryType]
public static class DepositQueries
{
    public static async Task<Deposit?> GetDeposit(string questionId, [Service(ServiceKind.Synchronized)] PaymentDbContext dbContext, CancellationToken cancellation)
        => await dbContext.Deposits.AsNoTracking().FirstOrDefaultAsync(c => c.QuestionId == questionId, cancellation);
    
    [UsePaging(MaxPageSize = 100)]
    [UseSorting]
    [UseFiltering]
    [Web]
    [Authorize(Roles = ["admin"])]
    public static IQueryable<Deposit> GetDeposits([Service(ServiceKind.Synchronized)] PaymentDbContext dbContext)
        => dbContext.Deposits.AsNoTracking().AsQueryable();
}