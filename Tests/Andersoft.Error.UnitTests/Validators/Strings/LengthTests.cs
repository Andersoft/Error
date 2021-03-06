using Andersoft.Error.Validators;
using Andersoft.Error.Validators.Strings;
using FluentAssertions;

namespace Andersoft.Error.UnitTests.Validators.Strings
{
  public class LengthTests
  {
    [Test]
    public void GivenValueExceedsLength_WhenCheckingIfLongerThan_ThenShouldReturnError()
    {
      // Arrange
      string value = "any value";

      // Act
      var result = value.Error().IfLongerThan(2)
                        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not be longer than 2 characters. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void GivenValueDoesNotExceedLength_WhenCheckingIfLongerThan_ThenShouldReturnUnit()
    {
      // Arrange
      string value = "any value";

      // Act
      var result = value.Error()
        .IfLongerThan(100)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }

    [Test]
    public void GivenValueUnderLength_WhenCheckingIfShorterThan_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result = value.Error().IfShorterThan(100)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not be shorter than 100 characters. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void GivenValueNotUnderLength_WhenCheckingIfShorterThan_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result = value.Error()
        .IfShorterThan(2)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }

    [Test]
    public void GivenValueEqualsLength_WhenCheckingForLengthEquals_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result = value.Error().IfLengthEquals(5)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String length should not be equal to 5. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void GivenValueNotEqualsLength_WhenCheckingForLengthEquals_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result = value.Error().IfLengthEquals(100)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }

    [Test]
    public void GivenValueLengthNotEquals_WhenLengthNotEquals_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result = value.Error().IfLengthNotEquals(100)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String length should be equal to 100. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void GivenValueLengthEquals_WhenLengthNotEquals_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result = value.Error().IfLengthNotEquals(5)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }
  }
}

