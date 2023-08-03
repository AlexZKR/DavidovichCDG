using Ardalis.GuardClauses;
using CDG.BLL.Entities.BasketAggregate;
using CDG.BLL.Exceptions;

namespace CDG.BLL.Extensions;

public static class BasketGuards
{
    public static void EmptyBasketOnCheckout(this IGuardClause guardClause, IReadOnlyCollection<BasketItem> basketItems)
    {
        if (!basketItems.Any())
            throw new EmptyBasketOnCheckoutException();
    }
}
