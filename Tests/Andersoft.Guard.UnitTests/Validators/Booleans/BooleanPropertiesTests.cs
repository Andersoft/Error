using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Booleans;
using FluentAssertions;
using LanguageExt;

namespace Andersoft.Guard.valueTests.Validators.Booleans;

public class BooleanPropertiesTests
{
  [Test]
  public void WhenCheckingIfPropertyTrue_WhenPropertyIsTrue_ThenShouldError()
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
  public void WhenCheckingIfPropertyTrue_WhenPropertyIsFalse_ThenShouldNotError()
  {
    // Arrange
    var person = new { Id = 1 };

    // Act
    var result = person.Error().IfTrue(p => p.Id == 2)
      .Match(value => value, error => default(bool?));

    // Assert
    result.Should().HaveValue().And.BeFalse();
  }

  [Test]
  public void WhenCheckingIfPropertyFalse_WhenPropertyIsFalse_ThenShouldError()
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
  public void WhenCheckingIfPropertyFalse_WhenPropertyIsTrue_ThenShouldNotError()
  {
    // Arrange
    var person = new { Id = 1 };

    // Act
    var result = person.Error().IfFalse(p => p.Id == 1)
      .Match(value => value, error => default(bool?));

    // Assert
    result.Should().HaveValue().And.BeTrue();
  }
}