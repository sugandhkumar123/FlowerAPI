using System;
using System.Collections.Generic;

#nullable disable

namespace FlowerAPI.FlowerModel
{
    public partial class Cart
    {
        public int CustomerId { get; set; }
        public int FlowerId { get; set; }
        public int? Quantity { get; set; }
        public double? ItemPrice { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Flower Flower { get; set; }
    }
}
