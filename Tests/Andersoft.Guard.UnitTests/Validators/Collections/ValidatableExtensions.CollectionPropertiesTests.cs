using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Collections;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Collections;


public class CollectionPropertiesTests
{
    [Test]
    public void WhenCheckingIfCollectionPropertyIsEmpty_GivenCollectionPropertyIsEmpty_ThenShouldError()
    {
        // Arrange
        var person = new { Friends = Array.Empty<string>() };

        // Act
       var result = person.Error().IfEmpty(p => p.Friends)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection should not be empty. (Parameter '{nameof(person)}: p => p.Friends')");
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyIsEmpty_GivenCollectionPropertyIsNotEmpty_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfEmpty(p => p.Friends);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyIsNotEmpty_GivenCollectionPropertyIsNotEmpty_ThenShouldError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's dad" } };

        // Act
       var result = person.Error().IfNotEmpty(p => p.Friends)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection should be empty. (Parameter '{nameof(person)}: p => p.Friends')");
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyIsNotEmpty_GivenCollectionPropertyIsEmpty_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = Array.Empty<string>() };

        // Act
       var result = person.Error().IfNotEmpty(p => p.Friends);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountEquals_GivenCollectionPropertyCountEquals_ThenShouldError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountEquals(p => p.Friends, 1)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection count should not be equal to 1. (Parameter '{nameof(person)}: p => p.Friends')");
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountEquals_GivenCollectionPropertyCountNotEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountEquals(p => p.Friends, 2);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountNotEquals_GivenCollectionPropertyCountNotEquals_ThenShouldError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountNotEquals(p => p.Friends, 2)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection count should be equal to 2. (Parameter '{nameof(person)}: p => p.Friends')");
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountNotEquals_GivenCollectionPropertyCountEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountNotEquals(p => p.Friends, 1);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountGreaterThan_GivenCollectionPropertyCountGreaterThan_ThenShouldError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountGreaterThan(p => p.Friends, 0)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection count should not be greater than 0. (Parameter '{nameof(person)}: p => p.Friends')");
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountGreaterThan_GivenCollectionPropertyCountNotGreaterThan_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountGreaterThan(p => p.Friends, 2);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountGreaterThan_GivenCollectionPropertyCountEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountGreaterThan(p => p.Friends, 1);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountLessThan_GivenCollectionPropertyCountLessThan_ThenShouldError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountLessThan(p => p.Friends, 2)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection count should not be less than 2. (Parameter '{nameof(person)}: p => p.Friends')");
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountLessThan_GivenCollectionPropertyCountNotLessThan_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountLessThan(p => p.Friends, 0);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyCountLessThan_GivenCollectionPropertyCountEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfCountLessThan(p => p.Friends, 1);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyHasNullElements_GivenCollectionPropertyHasNullElements_ThenShouldError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom", null } };

        // Act
       var result = person.Error().IfHasNullElements(p => p.Friends)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection should not have null elements. (Parameter '{nameof(person)}: p => p.Friends')");
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyHasNullElements_GivenCollectionPropertyHasNoNullElements_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfHasNullElements(p => p.Friends);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyContainsElement_GivenCollectionPropertyContainsElement_ThenShouldError()
    {
        // Arrange
        var person = new { Friends = new[] { "Amichai's mom" } };

        // Act
       var result = person.Error().IfContains(p => p.Friends, "Amichai's mom")
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection should not contain element. (Parameter '{nameof(person)}: p => p.Friends')");
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyContainsElement_GivenCollectionPropertyNotContainsElement_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { null, "Amichai's mom" } };

        // Act
       var result = person.Error().IfContains(p => p.Friends, "Amichai's dad");

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyNotContainsElement_GivenCollectionPropertyNotContainsElement_ThenShouldError()
    {
        // Arrange
        var person = new { Friends = new[] { null, "Amichai's mom" } };

        // Act
       var result = person.Error().IfNotContains(p => p.Friends, "Amichai's dad")
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection should contain element. (Parameter '{nameof(person)}: p => p.Friends')");
    }

    [Test]
    public void WhenCheckingIfCollectionPropertyNotContainsElement_GivenCollectionPropertyContainsElement_ThenShouldNotError()
    {
        // Arrange
        var person = new { Friends = new[] { null, "Amichai's mom" } };

        // Act
       var result = person.Error().IfNotContains(p => p.Friends, "Amichai's mom");

        // Assert
        result.Should().Be(person);
    }
}
