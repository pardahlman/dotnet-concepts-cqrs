using System;
using System.Collections.Generic;

namespace Shop.Messages.V1
{
  public class CalculatePrice
  {
    public List<Guid> ProductIds { get; set; }
  }

  public class PriceCalculated
  {
    public decimal Total { get; set; }
    public bool DiscountApplied { get; set; }
  }
}
