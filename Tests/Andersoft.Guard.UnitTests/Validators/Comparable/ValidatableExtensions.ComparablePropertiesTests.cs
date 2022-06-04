using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Comparable;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Comparable;


public class ComparablePropertiesTests
{
    [Test]
    public void WhenCheckingIfPropertyGreaterThan_GivenPropertyIsGreaterThan_ThenShouldError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfGreaterThan(v => v.Property, 4)
         .Match(_ => null, exception => exception as ArgumentOutOfRangeException);

       // Assert
       result!.Message.Should().Be($"Value should not be greater than 4. (Parameter '{nameof(value)}: v => v.Property'){Environment.NewLine}Actual value was {value.Property}.");
    }

    [Test]
    public void WhenCheckingIfPropertyGreaterThan_GivenPropertyIsNotGreaterThan_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfGreaterThan(v => v.Property, 6);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyGreaterThan_GivenPropertyIsEquals_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfGreaterThan(v => v.Property, 5);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyLessThan_GivenPropertyIsLessThan_ThenShouldError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfLessThan(v => v.Property, 6)
         .Match(_ => null, exception => exception as ArgumentOutOfRangeException);

       // Assert
       result!.Message.Should().Be($"Value should not be less than 6. (Parameter '{nameof(value)}: v => v.Property'){Environment.NewLine}Actual value was {value.Property}.");
    }

    [Test]
    public void WhenCheckingIfPropertyLessThan_GivenPropertyIsNotLessThan_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfLessThan(v => v.Property, 5);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyLessThan_GivenPropertyIsEquals_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfLessThan(v => v.Property, 5);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyPositive_GivenPropertyIsPositive_ThenShouldError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfPositive(v => v.Property)
         .Match(_ => null, exception => exception as ArgumentOutOfRangeException);

       // Assert
       result!.Message.Should().Be($"Value should not be greater than 0. (Parameter '{nameof(value)}: v => v.Property'){Environment.NewLine}Actual value was {value.Property}.");
    }

    [Test]
    public void WhenCheckingIfPropertyPositive_GivenPropertyIsNegative_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = -5 };

        // Act
       var result = value.Error().IfPositive(v => v.Property);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyPositive_GivenPropertyIsZero_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 0 };

        // Act
       var result = value.Error().IfPositive(v => v.Property);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyNegative_GivenPropertyIsNegative_ThenShouldError()
    {
        // Arrange
        var value = new { Property = -5 };

        // Act
       var result = value.Error().IfNegative(v => v.Property)
         .Match(_ => null, exception => exception as ArgumentOutOfRangeException);

       // Assert
       result!.Message.Should().Be($"Value should not be less than 0. (Parameter '{nameof(value)}: v => v.Property'){Environment.NewLine}Actual value was {value.Property}.");
    }

    [Test]
    public void WhenCheckingIfPropertyNegative_GivenPropertyIsPositive_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfNegative(v => v.Property);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyNegative_GivenPropertyIsZero_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 0 };

        // Act
       var result = value.Error().IfNegative(v => v.Property);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyOutOfRange_GivenPropertyIsOutOfRange_ThenShouldError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfOutOfRange(v => v.Property, 0, 4)
         .Match(_ => null, exception => exception as ArgumentOutOfRangeException);

       // Assert
       result!.Message.Should().Be($"Value should be between 0 and 4. (Parameter '{nameof(value)}: v => v.Property'){Environment.NewLine}Actual value was {value.Property}.");
    }

    [Test]
    public void WhenCheckingIfPropertyOutOfRange_GivenPropertyIsInRange_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfOutOfRange(v => v.Property, 4, 6);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyOutOfRange_GivenPropertyIsEqualsLowerBound_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfOutOfRange(v => v.Property, 5, 6);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyOutOfRange_GivenPropertyIsEqualsUpperBound_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfOutOfRange(v => v.Property, 4, 5);

        // Assert
        result.Should().Be(value);
    }
}