﻿using EcommerceProject.Domain.AggregatesModel.CartAggregate;
using EcommerceProject.Domain.AggregatesModel.CustomerAggregate;
using EcommerceProject.Domain.AggregatesModel.OrderAggregate;
using EcommerceProject.Infrastructure.CQRS.Command;

namespace EcommerceProject.Application.Commands.Carts.PlaceOrder
{
    internal class PlaceOrderCommandHandler : ICommandHandler<PlaceOrderCommand>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public PlaceOrderCommandHandler(ICartRepository cartRepository, IUserRepository customerRepository,
                              IOrderRepository orderRepository)
        {
            _cartRepository = cartRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }
        public async Task<CommandResult> Handle(PlaceOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindOneAsync(command.CustomerId, cancellationToken);
            if (customer == null) return CommandResult<int>.Error("You do not have permission to execute this command.");

            var cart = await _cartRepository.FindOneAsync(command.CartId, cancellationToken);
            if (cart == null) return CommandResult<int>.Error("Your cart is not exist.");

            var orderProducts = new List<OrderProduct>();
            var cartProducts = cart.CartProducts;
            foreach(var cartProduct in cartProducts)
            {
                OrderProduct orderProduct = new OrderProduct(cartProduct.ProductId, cartProduct.Quantity, cartProduct.Value);
                orderProducts.Add(orderProduct);
            }
            
            var order = new Order(cart.UserId, command.ShippingAddress, command.ShippingPhoneNumber, cart.Value, orderProducts);
            await _orderRepository.AddAsync(order, cancellationToken);

            cart.RemoveAllCartProduct();
            await _cartRepository.SaveAsync(cart);

            return CommandResult.Success();
        }
    }
}
