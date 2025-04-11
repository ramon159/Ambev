using Ambev.Domain.Entities.Sales;

namespace Ambev.UnitTests.Entities.Sales
{
    public class SaleTests
    {
        private Sale AddSaleItem(decimal productPrice, int quantity)
        {
            var sale = new Sale();
            sale.Items.Add(new SaleItem
            {
                ProductPrice = productPrice,
                Quantity = quantity,
                ProductId = Guid.NewGuid()
            });
            return sale;
        }

        [Theory]
        [InlineData(100.00, 3, 300.00)]
        [InlineData(100.00, 4, 360.00)]
        [InlineData(100.00, 9, 810.00)]
        [InlineData(100.00, 10, 800.00)]
        [InlineData(100.00, 20, 1600.00)]
        public void CalculateTotalAmount_AppliesCorrectDiscountTiers(
            decimal productPrice,
            int quantity,
            decimal expectedTotal)
        {
            // Arrange
            var sale = AddSaleItem(productPrice, quantity);

            // Act
            sale.CalculateTotalAmount();

            // Assert
            Assert.Equal(expectedTotal, sale.TotalAmount);
        }

        [Fact]
        public void CalculateTotalAmount_WithMultipleProducts_CalculatesCorrectTotal()
        {
            // Arrange
            var sale = new Sale();

            sale.Items.Add(new SaleItem
            {
                Quantity = 5,
                ProductPrice = 100.00m,
                ProductId = Guid.NewGuid()
            });

            sale.Items.Add(new SaleItem
            {
                Quantity = 12,
                ProductPrice = 50.00m,
                ProductId = Guid.NewGuid()
            });

            // Expected: (5*90) + (12*40) = 450 + 480 = 930
            decimal expectedTotal = 930.00m;

            // Act
            sale.CalculateTotalAmount();

            // Assert
            Assert.Equal(expectedTotal, sale.TotalAmount);
        }

        [Fact]
        public void ApplyQuantityDiscount_WhenQuantityBelow4_AppliesNoDiscount()
        {
            // Arrange
            var item = new SaleItem
            {
                ProductPrice = 100.00m,
                Quantity = 3
            };

            // Act
            item.ApplyQuantityDiscount();

            // Assert
            Assert.Equal(0m, item.Discount);
        }

        [Fact]
        public void ApplyQuantityDiscount_WhenQuantityBetween4And9_Applies10Percent()
        {
            // Arrange
            var item = new SaleItem
            {
                ProductPrice = 100.00m,
                Quantity = 5
            };

            // Act
            item.ApplyQuantityDiscount();

            // Assert
            Assert.Equal(10.00m, item.Discount); // 10% of 100
        }

        [Fact]
        public void ApplyQuantityDiscount_WhenQuantity10OrMore_Applies20Percent()
        {
            // Arrange
            var item = new SaleItem
            {
                ProductPrice = 100.00m,
                Quantity = 12
            };

            // Act
            item.ApplyQuantityDiscount();

            // Assert
            Assert.Equal(20.00m, item.Discount); // 20% of 100
        }
    }
}