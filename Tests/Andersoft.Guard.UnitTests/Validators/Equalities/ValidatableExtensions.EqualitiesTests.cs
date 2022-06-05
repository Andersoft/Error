using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Equalities;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Equalities;


public class EqualitiesTests
{
  [Test]
  public void WhenCheckingIfDefault_GivenValueIsDefault_ThenShouldError()
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
  public void WhenCheckingIfDefault_GivenValueIsNotDefault_ThenShouldNotError()
  {
    // Arrange
    System.DateTime value = System.DateTime.Now;

    // Act
    var result = value.Error().IfDefault();

    // Assert
    result.Should().Be(value);
  }

  [Test]
  public void WhenCheckingIfNotDefault_GivenValueIsNotDefault_ThenShouldError()
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
  public void WhenCheckingIfNotDefault_GivenValueIsDefault_ThenShouldNotError()
  {
    // Arrange
    System.DateTime value = default;

    // Act
    var result = value.Error().IfNotDefault();

    // Assert
    result.Should().Be(value);
  }

  [Test]
  public void WhenCheckingIfEquals_GivenValueEquals_ThenShouldError()
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
  public void WhenCheckingIfEquals_GivenValueIsNotEqual_ThenShouldNotError()
  {
    // Arrange
    int value = 5;

    // Act
    var result = value.Error().IfEquals(6);

    // Assert
    result.Should().Be(value);
  }

  [Test]
  public void WhenCheckingIfNotEquals_GivenNotEquals_ThenShouldError()
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
  public void WhenCheckingIfNotEquals_GivenEquals_ThenShouldNotError()
  {
    // Arrange
    int value = 5;

    // Act
    var result = value.Error().IfNotEquals(5);

    // Assert
    result.Should().Be(value);
  }

  [Test]
  public void WhenCheckingIfEquals_GivenObjectReferenceEquals_ThenShouldError()
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
  public void WhenCheckingIfEquals_GivenObjectReferenceIsNotEqual_ThenShouldNotError()
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
  public void WhenCheckingIfNotEquals_GivenObjectReferenceIsNotEqual_ThenShouldError()
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
  public void WhenCheckingIfNotEquals_GivenObjectReferenceEquals_ThenShouldNotError()
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
  public void WhenCheckingIfEquals_GivenOverrideEqualsTypeEquals_ThenShouldError()
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
  public void WhenCheckingIfEquals_GivenOverridequalsTypeIsNotEqual_ThenShouldNotError()
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
  public void WhenCheckingIfNotEquals_GivenOverridequalsTypeIsNotEqual_ThenShouldError()
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
  public void WhenCheckingIfNotEquals_GivenOverridequalsTypeEquals_ThenShouldNotError()
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