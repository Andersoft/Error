using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Strings;
using FluentAssertions;
using LanguageExt;

namespace Andersoft.Guard.UnitTests.Validators.Strings
{
  internal class StringEqualityTests
  {
    [Test]
    public void WhenCheckingIfWhiteSpace_GivenValueContainsWhiteSpace_ThenShouldError()
    {
      // Arrange
      string value = " ";

      // Act
      var result =  value.Error().IfWhiteSpace()
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not be white space only. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfWhiteSpace_GivenValueDoesNotContainWhiteSpace_ThenShouldNotError()
    {
      // Arrange
      string value = "not white space";

      // Act
      var result =  value.Error().IfWhiteSpace()
        .Match(unit => unit, error => default(Unit?));

      // Assert
      result.Should().BeOfType<Unit>();
    }

    [Test]
    public void WhenCheckingIfEmpty_GivenValueContainsEmpty_ThenShouldError()
    {
      // Arrange
      string value = "";

      // Act
      var result =  value.Error().IfEmpty()
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not be empty. (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfEmpty_GivenValueDoesNotContainEmpty_ThenShouldNotError()
    {
      // Arrange
      string value = "not empty";

      // Act
      var result =  value.Error().IfEmpty()
        .Match(unit => unit, error => default(Unit?));

      // Assert
      result.Should().BeOfType<Unit>();
    }

    [Test]
    public void WhenCheckingIfEquals_GivenValueContainsEquals_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfEquals("value")
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not be equal to 'value' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfEquals_GivenValueDoesNotContainEquals_ThenShouldNotError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfEquals("VALUE")
        .Match(unit => unit, error => default(Unit?));

      // Assert
      result.Should().BeOfType<Unit>();
    }

    [TestCase("value", "VALUE", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "\u00e5", StringComparison.InvariantCulture)]
    [TestCase("AA", "A\u0000\u0000\u0000a", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfEquals_GivenValueContainsEqualsUsingCustomComparisonType_ThenShouldWhenChecking(string value, string otherValue, StringComparison comparisonType)
    {
      // Act
      var result =  value.Error().IfEquals(otherValue, comparisonType)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not be equal to '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(value)}')");
    }

    
    [TestCase("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [TestCase("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfEquals_GivenValueDoesNotContainEqualsUsingCustomComparisonType_ThenShouldNotWhenChecking(string value, string otherValue, StringComparison comparisonType)
    {
      // Act
      var result =  value.Error().IfEquals(otherValue, comparisonType)
        .Match(unit => unit, error => default(Unit?));

      // Assert
      result.Should().BeOfType<Unit>();
    }

    [Test]
    public void WhenCheckingIfEqualsIgnoreCase_GivenValueContainsEqualsSameCase_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfEqualsIgnoreCase("value")
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not be equal to 'value' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfEqualsIgnoreCase_GivenValueContainsEqualsDifferentCase_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfEqualsIgnoreCase("VALUE")
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not be equal to 'VALUE' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfEqualsIgnoreCase_GivenValueDoesNotContainEquals_ThenShouldNotError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfEqualsIgnoreCase("different value")
        .Match(unit => unit, error => default(Unit?));

      // Assert
      result.Should().BeOfType<Unit>();
    }

    [Test]
    public void WhenCheckingIfNotEquals_GivenValueDoesNotContainEquals_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfNotEquals("different value")
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should be equal to 'different value' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(value)}')");
    }

    
    [TestCase("value", "VALUE", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "\u00e5", StringComparison.InvariantCulture)]
    [TestCase("AA", "A\u0000\u0000\u0000a", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfNotEquals_GivenValueContainsEqualsUsingCustomComparisonType_ThenShouldNotWhenChecking(string value, string otherValue, StringComparison comparisonType)
    {
      // Act
      var result =  value.Error().IfNotEquals(otherValue, comparisonType)
        .Match(unit => unit, error => default(Unit?));

      // Assert
      result.Should().BeOfType<Unit>();
    }

    
    [TestCase("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [TestCase("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfNotEquals_GivenValueDoesNotContainEqualsUsingCustomComparisonType_ThenShouldWhenChecking(string value, string otherValue, StringComparison comparisonType)
    {
      // Act
      var result =  value.Error().IfNotEquals(otherValue, comparisonType)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should be equal to '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfNotEquals_GivenValueContainsEquals_ThenShouldNotError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfNotEquals("value")
        .Match(unit => unit, error => default(Unit?));

      // Assert
      result.Should().BeOfType<Unit>();
    }

    [Test]
    public void WhenCheckingIfNotEqualsIgnoreCase_GivenValueDoesNotContainEquals_ThenShouldError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfNotEqualsIgnoreCase("different value")
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should be equal to 'different value' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfNotEqualsIgnoreCase_GivenValueContainsEquals_ThenShouldNotError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfNotEqualsIgnoreCase("value")
        .Match(unit => unit, error => default(Unit?));

      // Assert
      result.Should().BeOfType<Unit>();
    }

    [Test]
    public void WhenCheckingIfNotEqualsIgnoreCase_GivenValueContainsEqualsDifferentCase_ThenShouldNotError()
    {
      // Arrange
      string value = "value";

      // Act
      var result =  value.Error().IfNotEqualsIgnoreCase("VALUE")
        .Match(unit => unit, error => default(Unit?));

      // Assert
      result.Should().BeOfType<Unit>();
    }
  }
}
