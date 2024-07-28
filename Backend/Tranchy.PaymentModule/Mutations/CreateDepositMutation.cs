using HotChocolate;
using HotChocolate.Types;
using Tranchy.PaymentModule.Data;
using Tranchy.PaymentModule.Requests;

namespace Tranchy.PaymentModule.Mutations;

[MutationType]
public static class CreateDepositMutation
{
    public static async Task<Deposit> CreateDeposit(CreateDepositInput input, [Service(ServiceKind.Synchronized)] PaymentDbContext dbContext, CancellationToken cancellation)
    {
        var newDeposit = new Deposit { QuestionId = input.QuestionId, Amount = input.Amount, Status = "Init" };
        dbContext.Deposits.Add(newDeposit);
        await dbContext.SaveChangesAsync(cancellation);

        return newDeposit;
    }
}