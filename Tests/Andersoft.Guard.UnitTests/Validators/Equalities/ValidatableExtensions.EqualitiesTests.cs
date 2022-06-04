using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Equalities;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Equalities;


public class EqualitiesTests
{
    [Test]
    public void WhenCheckingIfDefault_WhenValueIsDefault_ThenShouldError()
    {
        // Arrange
        System.DateTime value = default;

        // Act
       var result = value.Error().IfDefault()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be default. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfDefault_WhenValueIsNotDefault_ThenShouldNotError()
    {
        // Arrange
        System.DateTime value = System.DateTime.Now;

        // Act
       var result = value.Error().IfDefault();

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfNotDefault_WhenValueIsNotDefault_ThenShouldError()
    {
        // Arrange
        System.DateTime value = System.DateTime.Now;

        // Act
       var result = value.Error().IfNotDefault()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be default. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfNotDefault_WhenValueIsDefault_ThenShouldNotError()
    {
        // Arrange
        System.DateTime value = default;

        // Act
       var result = value.Error().IfNotDefault();

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfEquals_WhenValueEquals_ThenShouldError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfEquals(5)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be equal to 5. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfEquals_WhenValueIsNotEqual_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfEquals(6);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfNotEquals_WhenNotEquals_ThenShouldError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfNotEquals(6)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be equal to 6. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfNotEquals_WhenEquals_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfNotEquals(5);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfEquals_WhenObjectReferenceEquals_ThenShouldError()
    {
        // Arrange
        var value1 = new object();
        var value2 = value1;

        // Act
       var result = value1.Error().IfEquals(value2)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be equal to {value2}. (Parameter '{nameof(value1)}')");
    }

    [Test]
    public void WhenCheckingIfEquals_WhenObjectReferenceIsNotEqual_ThenShouldNotError()
    {
        // Arrange
        var value1 = new object();
        var value2 = new object();

        // Act
       var result = value1.Error().IfEquals(value2);

        // Assert
        result.Should().Be(value1);
    }

    [Test]
    public void WhenCheckingIfNotEquals_WhenObjectReferenceIsNotEqual_ThenShouldError()
    {
        // Arrange
        var value1 = new object();
        var value2 = new object();

        // Act
       var result = value1.Error().IfNotEquals(value2)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be equal to {value2}. (Parameter '{nameof(value1)}')");
    }

    [Test]
    public void WhenCheckingIfNotEquals_WhenObjectReferenceEquals_ThenShouldNotError()
    {
        // Arrange
        var value1 = new object();
        var value2 = value1;

        // Act
       var result = value1.Error().IfNotEquals(value2);

        // Assert
        result.Should().Be(value1);
    }

    [Test]
    public void WhenCheckingIfEquals_WhenOverrideEqualsTypeEquals_ThenShouldError()
    {
        // Arrange
        var value1 = new OverrideEqualsType(1);
        var value2 = new OverrideEqualsType(1);

        // Act
       var result = value1.Error().IfEquals(value2)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be equal to {value2}. (Parameter '{nameof(value1)}')");
    }

    [Test]
    public void WhenCheckingIfEquals_WhenOverridequalsTypeIsNotEqual_ThenShouldNotError()
    {
        // Arrange
        var value1 = new OverrideEqualsType(1);
        var value2 = new OverrideEqualsType(2);

        // Act
       var result = value1.Error().IfEquals(value2);

        // Assert
        result.Should().Be(value1);
    }

    [Test]
    public void WhenCheckingIfNotEquals_WhenOverridequalsTypeIsNotEqual_ThenShouldError()
    {
        // Arrange
        var value1 = new OverrideEqualsType(1);
        var value2 = new OverrideEqualsType(2);

        // Act
       var result = value1.Error().IfNotEquals(value2)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be equal to {value2}. (Parameter '{nameof(value1)}')");
    }

    [Test]
    public void WhenCheckingIfNotEquals_WhenOverridequalsTypeEquals_ThenShouldNotError()
    {
        // Arrange
        var value1 = new OverrideEqualsType(1);
        var value2 = new OverrideEqualsType(1);

        // Act
       var result = value1.Error().IfNotEquals(value2);

        // Assert
        result.Should().Be(value1);
    }

    private class OverrideEqualsType
    {
        public OverrideEqualsType(int id) => this.Id = id;

        public int Id { get; }

        public override bool Equals(object? obj)
        {
            if (obj is not OverrideEqualsType other)
            {
                return false;
            }

            return this.Id == other.Id;
        }

        public override int GetHashCode() => this.Id.GetHashCode();
    }
}