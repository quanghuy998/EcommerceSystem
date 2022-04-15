﻿using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;

namespace EcommerceProject.Domain.AggregatesModel.CartAggregate
{
    public class CartProduct : Entity<int>
    {
        public int ProductId { get; }
        public MoneyValue Price { get; }
        public int Quantity { get; private set; }
        public MoneyValue Value { get; private set; }

        private CartProduct()
        {
        }

        public CartProduct(int productId, int quantity, MoneyValue price)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
            this.Price = price;
            CalculateValue();
        }

        public void ChangeQuantity(int quantity)
        {
            this.Quantity = quantity;
            CalculateValue();
        }
        public void CalculateValue()
        {
            this.Value = this.Quantity * this.Price;
        }
    }
}
