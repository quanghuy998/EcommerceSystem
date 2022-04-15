﻿using EcommerceProject.Domain.AggregatesModel.OrderAggregate;
using EcommerceProject.Domain.SharedKermel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceProject.Domain.Test
{
    public class OrderAggregateTest
    {

        [Fact]
        public void GivenInformation_WhenCreatingAnOrderProduct_ThenItShouldBeCreated()
        {
            var productId = 1;
            var quantity = 3;
            var value = new MoneyValue(100, "USA");

            var orderProduct = new OrderProduct(productId, quantity, value);

            Assert.Equal(productId, orderProduct.ProductId);
            Assert.Equal(quantity, orderProduct.Quantity);
            Assert.Equal(value, orderProduct.Value);
        }

        [Fact]
        public void GivenInformation_WhenCreatingAnOrder_ThenItShouldBeCreated()
        {
            var userId = Guid.NewGuid();
            var shippingAddress = "01 Nguyen Huu Tho, Da Nang city.";
            var shippingPhoneNumber = "0000-000-000";
            var orderProduct1 = GivenSampleOrderProduct1();
            var orderProducts = new List<OrderProduct>();
            orderProducts.Add(orderProduct1);

            var order = new Order(userId, shippingAddress, shippingPhoneNumber, orderProducts);

            Assert.Equal(userId, order.UserId);
            Assert.Equal(shippingAddress, order.ShippingAddress);
            Assert.Equal(shippingPhoneNumber, order.ShippingPhoneNumber);
            Assert.Single(order.OrderProducts);
            Assert.Equal(orderProduct1.Id, order.OrderProducts[0].Id);
            Assert.Equal(orderProduct1.Quantity, order.OrderProducts[0].Quantity);
            Assert.Equal(orderProduct1.Value, order.OrderProducts[0].Value);
        }

        [Fact]
        public void GivenValue_WhenCalculatingOrderValue_ThenItShouldBeExactly()
        {
            var userId = Guid.NewGuid();
            var shippingAddress = "01 Nguyen Huu Tho, Da Nang city.";
            var shippingPhoneNumber = "0000-000-000";
            var orderProduct1 = GivenSampleOrderProduct1();
            var orderProduct2 = GivenSampleOrderProduct2();
            var orderProducts = new List<OrderProduct>();
            orderProducts.Add(orderProduct1);
            orderProducts.Add(orderProduct2);

            var order = new Order(userId, shippingAddress, shippingPhoneNumber, orderProducts);

            Assert.Equal(orderProduct1.Value + orderProduct2.Value, order.Value);
        }

        [Fact]
        public void GivenAnOrder_WhenChangingOrderStatus_ThenItShouldBeChanged()
        {
            var order = GivenSampleOrder();
            OrderStatus orderStatus = OrderStatus.Canceled;

            order.ChangeOrderStatus(orderStatus);

            Assert.Equal(orderStatus, order.OrderStatus);
        }

        private OrderProduct GivenSampleOrderProduct1()
        {
            var productId = 1;
            var quantity = 3;
            var value = new MoneyValue(100, "USA");

            return new OrderProduct(productId, quantity, value);
        }
        
        private OrderProduct GivenSampleOrderProduct2()
        {
            var productId = 2;
            var quantity = 2;
            var value = new MoneyValue(1000, "USA");

            return new OrderProduct(productId, quantity, value);
        }

        private Order GivenSampleOrder()
        {
            var userId = Guid.NewGuid();
            var shippingAddress = "01 Nguyen Huu Tho, Da Nang city.";
            var shippingPhoneNumber = "0000-000-000";
            var orderProduct1 = GivenSampleOrderProduct1();
            var orderProducts = new List<OrderProduct>();
            orderProducts.Add(orderProduct1);

            return new Order(userId, shippingAddress, shippingPhoneNumber, orderProducts);
        }
    }
}