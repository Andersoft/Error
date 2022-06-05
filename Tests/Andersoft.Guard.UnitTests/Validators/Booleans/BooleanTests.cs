using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Booleans;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Booleans
{
  public class BooleansTests
  {
    [Test]
    public void WhenCheckingIfTrue_GivenValueIsTrue_ThenShouldError()
    {
      // Arrange
      bool value = true;

      // Act
      var result = value.Error().IfTrue()
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"Value should be false. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfTrue_GivenValueIsFalse_ThenShouldNotError()
    {
      // Arrange
      bool value = false;

      // Act
      var result = value.Error().IfTrue()
        .Match(success => success.Value, error => default(bool?));

      // Assert
      result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfFalse_GivenValueIsTrue_ThenShouldNotError()
    {
      // Arrange
      bool value = true;

      // Act
      var result = value.Error().IfFalse()
        .Match(success => success.Value, error => default(bool?));

      // Assert
      result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfFalse_GivenValueIsFalse_ThenShouldError()
    {
      // Arrange
      bool value = false;

      // Act
      var result = value.Error().IfFalse()
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"Value should be true. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfConditionTrue_GivenConditionIsTrue_ThenShouldError()
    {
      // Arrange
      var value = "value";

      // Act
      var result = value.Error().IfTrue(x => x.Length > 0)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"Value should not meet condition (condition: 'x => x.Length > 0'). (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfConditionTrue_GivenConditionIsFalse_ThenShouldNotError()
    {
      // Arrange
      var value = "value";

      // Act
      var result = value.Error().IfTrue(x => x.Length == 0)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfConditionFalse_GivenConditionIsTrue_ThenShouldNotError()
    {
      // Arrange
      var value = "value";

      // Act
      var result = value.Error().IfFalse(x => x.Length > 0)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfConditionFalse_GivenConditionIsFalse_ThenShouldError()
    {
      // Arrange
      var value = "value";

      // Act
      var result = value.Error().IfFalse(x => x.Length == 0)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"Value should meet condition (condition: 'x => x.Length == 0'). (Parameter '{nameof(value)}')");
    }
  }
}
