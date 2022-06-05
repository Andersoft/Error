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
  public class SubstringTests
  {
    [Test]
    public void WhenCheckingIfEndsWith_GivenValueDoesNotEndsWith_ThenShouldNotErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfEndsWith("Jo")
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(name);
    }

    [Test]
    public void WhenCheckingIfEndsWith_GivenValueEndsWith_ThenShouldErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfEndsWith("hn")
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not end with 'hn' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(name)}')");
    }

    [Test]
    public void WhenCheckingIfEndsWith_GivenValueEndsWithCustomComparisonType_ThenShouldErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfEndsWith("HN", StringComparison.OrdinalIgnoreCase)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not end with 'HN' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(name)}')");
    }

    [Test]
    public void WhenCheckingIfNotEndsWith_GivenValueDoesNotEndsWith_ThenShouldErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfNotEndsWith("Jo")
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should end with 'Jo' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(name)}')");
    }

    [Test]
    public void WhenCheckingIfNotEndsWith_GivenValueEndsWith_ThenShouldNotErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfNotEndsWith("hn")
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(name);
    }

    [Test]
    public void WhenCheckingIfNotEndsWith_GivenValueEndsWithCustomComparisonType_ThenShouldNotErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfNotEndsWith("HN", StringComparison.OrdinalIgnoreCase)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(name);
    }

    [Test]
    public void WhenCheckingIfStartsWith_GivenValueDoesNotStartsWith_ThenShouldNotErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfStartsWith("hn")
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(name);
    }

    [Test]
    public void WhenCheckingIfStartsWith_GivenValueStartsWith_ThenShouldErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfStartsWith("Jo")
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not start with 'Jo' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(name)}')");
    }

    [Test]
    public void WhenCheckingIfStartsWith_GivenValueStartsWithCustomComparisonType_ThenShouldErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfStartsWith("JO", StringComparison.OrdinalIgnoreCase)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not start with 'JO' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(name)}')");
    }

    [Test]
    public void WhenCheckingIfNotStartsWith_GivenValueDoesNotStartsWith_ThenShouldErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfNotStartsWith("hn")
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should start with 'hn' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(name)}')");
    }

    [Test]
    public void WhenCheckingIfNotStartsWith_GivenValueStartsWith_ThenShouldNotErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfNotStartsWith("Jo")
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(name);
    }

    [Test]
    public void WhenCheckingIfNotStartsWith_GivenValueStartsWithCustomComparisonType_ThenShouldNotErrorWhenChecking()
    {
      // Arrange
      string name = "John";

      // Act
      var result = name.Error().IfNotStartsWith("JO", StringComparison.OrdinalIgnoreCase)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(name);
    }

    [Test]
    public void WhenCheckingIfContains_GivenValueContains_ThenShouldErrorWhenChecking()
    {
      // Arrange
      string value = "the quick brown fox jumps over the lazy dog.";
      string otherValue = "quick";

      // Act
      var result = value.Error().IfContains(otherValue)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not contain '{otherValue}' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfContains_GivenValueDoesNotContains_ThenShouldNotErrorWhenChecking()
    {
      // Arrange
      string value = "the quick brown fox jumps over the lazy dog.";
      string otherValue = "horse";

      // Act
      var result = value.Error().IfContains(otherValue)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfNotContains_GivenValueDoesNotContains_ThenShouldErrorWhenChecking()
    {
      // Arrange
      string value = "the quick brown fox jumps over the lazy dog.";
      string otherValue = "horse";

      // Act
      var result = value.Error().IfNotContains(otherValue)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should contain '{otherValue}' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(value)}')");
    }

    [Test]
    public void WhenCheckingIfNotContains_GivenValueContains_ThenShouldNotErrorWhenChecking()
    {
      // Arrange
      string value = "the quick brown fox jumps over the lazy dog.";
      string otherValue = "jumps";

      // Act
      var result = value.Error().IfNotContains(otherValue)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }

    
    [TestCase("value", "AL", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0068\u0065\u006c\u006c\u006f", "\u0065\u006c", StringComparison.InvariantCulture)]
    [TestCase("\u0041\u0041", "\u0061", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfContains_GivenValueContainsUsingCustomComparisonType_ThenShouldErrorWhenChecking(string value, string otherValue, StringComparison comparisonType)
    {
      // Act
      var result = value.Error().IfContains(otherValue, comparisonType)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should not contain '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(value)}')");
    }

    
    [TestCase("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [TestCase("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfContains_GivenValueDoesNotContainsUsingCustomComparisonType_ThenShouldNotErrorWhenChecking(string value, string otherValue, StringComparison comparisonType)
    {
      // Act
      var result = value.Error().IfContains(otherValue, comparisonType)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }

    
    [TestCase("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [TestCase("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfNotContains_GivenValueDoesNotContainsUsingCustomComparisonType_ThenShouldErrorWhenChecking(string value, string otherValue, StringComparison comparisonType)
    {
      // Act
      var result = value.Error().IfNotContains(otherValue, comparisonType)
        .Match(_ => null, exception => exception as ArgumentException);

      // Assert
      result!.Message.Should().Be($"String should contain '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(value)}')");
    }

    
    [TestCase("value", "AL", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0068\u0065\u006c\u006c\u006f", "\u0065\u006c", StringComparison.InvariantCulture)]
    [TestCase("\u0041\u0041", "\u0061", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfNotContains_GivenValueContainsUsingCustomComparisonType_ThenShouldNotErrorWhenChecking(string value, string otherValue, StringComparison comparisonType)
    {
      // Act
      var result = value.Error().IfNotContains(otherValue, comparisonType)
        .Match(success => success.Value, error => default!);

      // Assert
      result.Should().Be(value);
    }
  }
}
