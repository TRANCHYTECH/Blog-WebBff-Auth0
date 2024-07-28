using MongoDB.Entities;
using Tranchy.Common.Data;

namespace Tranchy.Common;

public static class QueryExtensions
{
    public static Find<T, TProjection> Mine<T, TProjection>(this Find<T, TProjection> find, ITenant tenant)
        where T : class, IOwnEntity => find.Match(e => e.CreatedBy == tenant.UserId);

    public static Find<T, TProjection> Other<T, TProjection>(this Find<T, TProjection> find, ITenant tenant)
        where T : class, IOwnEntity => find.Match(e => e.CreatedBy != tenant.UserId);

    public static Find<T, TProjection> Other<T, TProjection>(this Find<T, TProjection> find, string id, ITenant tenant)
        where T : Entity, IOwnEntity => find.Match(e => e.ID == id && e.CreatedBy != tenant.UserId);
}
