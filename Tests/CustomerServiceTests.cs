using Bogus.DataSets;
using Castle.Core.Resource;
using FluentAssertions;
using Moq;
using ProvaPub.Contracts;
using ProvaPub.Models;
using ProvaPub.Services;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private Mock<ICustomerRepository> _customerRepository;
        private Mock<IOrderRepository> _orderRepository;
        private Customer? _customer;
        private Order _order;
        private CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _orderRepository = new Mock<IOrderRepository>();
            _customerService = new CustomerService(_customerRepository.Object, _orderRepository.Object);
            _order = new Order
            {
                CustomerId = 2,
                OrderDate = DateTime.UtcNow.AddMonths(-1),
                Value = 10
            };

            _customer = new Customer
            {
                Id = 2,
                Name = "Teste",
                Orders = new List<Order> { _order }
            };
        }
        

        [Fact]
        public async Task CustomerService_CanPurchase_DeveRetornarTrue()
        {
            // Arrange  
            _customerRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(_customer);
            _orderRepository.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(0);
            _customerRepository.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(0);

            // Act
            var resultado = await _customerService.CanPurchase(_customer.Id, 50);

            // Assert 
            resultado.Should().BeTrue();
            _customerRepository.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            _orderRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            _customerRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        }

        [Fact]
        public void CustomerService_CanPurchase_ComIdClienteInvalido_DeveRetornarException()
        {
            // Arrange
            _customer = new Customer
            {
                Id = 0,
                Name = "Teste",
                Orders = new List<Order>()
            };

            // Act
            var resultado = _customerService.CanPurchase(_customer.Id, 50);

            // Assert
            resultado.Exception!.Message.Should().Contain("Specified argument was out of the range of valid values");
            _customerRepository.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Never);
            _orderRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Never);
            _customerRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Never);
        }

        [Fact]
        public void CustomerService_CanPurchase_ComValorCompraInvalido_DeveRetornarException()
        {
            // Act
            var resultado = _customerService.CanPurchase(_customer!.Id, 0);

            // Assert
            resultado.Exception!.Message.Should().Contain("Specified argument was out of the range of valid values");
            _customerRepository.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Never);
            _orderRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Never);
            _customerRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Never);
        }

        [Fact]
        public void CustomerService_CanPurchase_ComClienteNulo_DeveRetornarException()
        {
            // Arrange
            var customerId = 1;            
            _customer = null;

            _customerRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(_customer);

            // Act
            var resultado = _customerService.CanPurchase(1, 50);

            // Assert
            resultado.Exception!.Message.Should().Contain($"Customer Id {customerId} does not exists");
            _customerRepository.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            _orderRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Never);
            _customerRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Never);
        }

        [Fact]
        public async Task CustomerService_CanPurchase_ComMaisDeUmaCompraMes_DeveRetornarFalse()
        {
            // Arrange           
            
            _customerRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(_customer);
            _orderRepository.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(2);            

            // Act
            var resultado = await _customerService.CanPurchase(_customer!.Id, 50);

            // Assert 
            resultado.Should().BeFalse();
            _customerRepository.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            _orderRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            _customerRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Never);
        }

        [Fact]
        public async Task CustomerService_CanPurchase_ComValorMaiorNaPrimeiraCompra_DeveRetornarFalse()
        {
            // Arrange            

            _customerRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(_customer);
            _orderRepository.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(0);
            _customerRepository.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(0);

            // Act
            var resultado = await _customerService.CanPurchase(_customer!.Id, 200);

            // Assert 
            resultado.Should().BeFalse();
            _customerRepository.Verify(x => x.FindByIdAsync(It.IsAny<int>()), Times.Once);
            _orderRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            _customerRepository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()), Times.Once);
        }
    }
}
