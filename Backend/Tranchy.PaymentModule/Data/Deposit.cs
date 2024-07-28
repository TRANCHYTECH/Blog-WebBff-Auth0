using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tranchy.Common.HotChocolate;

namespace Tranchy.PaymentModule.Data;

public class Deposit
{
    public int Id { get; set; }

    public required string QuestionId { get; set; }

    [Web]
    public required double Amount { get; set; }

    public required string Status { get; set; }
}

public class DepositEntityTypeConfiguration : IEntityTypeConfiguration<Deposit>
{
    public void Configure(EntityTypeBuilder<Deposit> builder) => builder.HasKey(a => a.Id);
}
