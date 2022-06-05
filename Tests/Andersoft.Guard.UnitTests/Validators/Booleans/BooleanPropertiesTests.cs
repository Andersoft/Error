using Andersoft.Error.Validators;
using Andersoft.Error.Validators.Booleans;
using FluentAssertions;

namespace Andersoft.Error.UnitTests.Validators.Booleans;

public class BooleanPropertiesTests
{
  [Test]
  public void WhenCheckingIfPropertyTrue_GivenPropertyIsIsTrue_ThenShouldError()
  {
    // Arrange
    var person = new { Id = 1 };

    // Act
    var result = person.Error().IfTrue(p => p.Id == 1)
      .Match(_ => null, exception => exception as ArgumentException);

    // Assert
    result!.Message.Should().Be($"Value should not meet condition (condition: 'p => p.Id == 1'). (Parameter '{nameof(person)}')");
  }

  [Test]
  public void WhenCheckingIfPropertyTrue_GivenPropertyIsIsFalse_ThenShouldNotError()
  {
    // Arrange
    var person = new { Id = 1 };

    // Act
    var result = person.Error().IfTrue(p => p.Id == 2)
      .Match(success => success.Value, error => default!);

    // Assert
    result.Should().Be(person);
  }

  [Test]
  public void WhenCheckingIfPropertyFalse_GivenPropertyIsIsFalse_ThenShouldError()
  {
    // Arrange
    var person = new { Id = 1 };

    // Act
    var result = person.Error().IfFalse(p => p.Id == 2)
      .Match(_ => null, exception => exception as ArgumentException);

    // Assert
    result!.Message.Should().Be($"Value should meet condition (condition: 'p => p.Id == 2'). (Parameter '{nameof(person)}')");
  }

  [Test]
  public void WhenCheckingIfPropertyFalse_GivenPropertyIsIsTrue_ThenShouldNotError()
  {
    // Arrange
    var person = new { Id = 1 };

    // Act
    var result = person.Error().IfFalse(p => p.Id == 1)
      .Match(success => success.Value, error => default!);

    // Assert
    result.Should().Be(person);
  }
}