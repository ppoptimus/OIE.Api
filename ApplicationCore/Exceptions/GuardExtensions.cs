using ApplicationCore.Exceptions;
using ApplicationCore.Entities;

namespace Ardalis.GuardClauses
{
    public static class FooGuard
    {
        public static void Null(this IGuardClause guardClause, int entityId, BaseEntity entity)
        {
            if (entity == null)
                throw new DataNotFoundException(entityId);
        }

        public static void Null(this IGuardClause guardClause, int count, string message)
        {
            if (count == 0)
                throw new DataNotFoundException(message);
        }

    }
}