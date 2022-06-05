using Andersoft.Error.Validators;
using Andersoft.Error.Validators.Comparable;
using FluentAssertions;

namespace Andersoft.Error.UnitTests.Validators.Comparable;


public class ComparableTests
{
    [Test]
    public void WhenCheckingIfGreaterThan_GivenValueIsGreaterThan_ThenShouldError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfGreaterThan(4)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be greater than 4. (Parameter '{nameof(value)}'){Environment.NewLine}Actual value was {value}.");
    }

    [Test]
    public void WhenCheckingIfGreaterThan_GivenValueIsNotGreaterThan_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfGreaterThan(6).Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfGreaterThan_GivenValueEquals_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfGreaterThan(5).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfLessThan_GivenValueIsLessThan_ThenShouldError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfLessThan(6)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be less than 6. (Parameter '{nameof(value)}'){Environment.NewLine}Actual value was {value}.");
    }

    [Test]
    public void WhenCheckingIfLessThan_GivenValueIsNotLessThan_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfLessThan(4).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfLessThan_GivenValueEquals_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfLessThan(5).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPositive_GivenPositive_ThenShouldError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfPositive()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be greater than 0. (Parameter '{nameof(value)}'){Environment.NewLine}Actual value was {value}.");
    }

    [Test]
    public void WhenCheckingIfPositive_GivenNegative_ThenShouldNotError()
    {
        // Arrange
        int value = -5;

        // Act
       var result = value.Error().IfPositive().Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPositive_GivenZero_ThenShouldNotError()
    {
        // Arrange
        int value = 0;

        // Act
       var result = value.Error().IfPositive().Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfNegative_GivenNegative_ThenShouldError()
    {
        // Arrange
        int value = -5;

        // Act
       var result = value.Error().IfNegative()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be less than 0. (Parameter '{nameof(value)}'){Environment.NewLine}Actual value was {value}.");
    }

    [Test]
    public void WhenCheckingIfNegative_GivenPositive_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfNegative().Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfNegative_GivenZero_ThenShouldNotError()
    {
        // Arrange
        int value = 0;

        // Act
       var result = value.Error().IfNegative().Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfOutOfRange_GivenOutOfRange_ThenShouldError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfOutOfRange(0, 4)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be between 0 and 4. (Parameter '{nameof(value)}'){Environment.NewLine}Actual value was {value}.");
    }

    [Test]
    public void WhenCheckingIfOutOfRange_GivenInRange_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfOutOfRange(4, 6).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfOutOfRange_GivenEqualsLowerBound_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfOutOfRange(5, 6).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfOutOfRange_GivenEqualsUpperBound_ThenShouldNotError()
    {
        // Arrange
        int value = 5;

        // Act
       var result = value.Error().IfOutOfRange(4, 5).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }
}