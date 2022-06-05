using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Collections;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Collections;


public class CollectionsTests
{
    [Test]
    public void WhenCheckingIfCollectionEmpty_GivenCollectionIsEmpty_ThenShouldError()
    {
        // Arrange
        var collection = Array.Empty<int>();

        // Act
       var result = collection.Error().IfEmpty()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection should not be empty. (Parameter '{nameof(collection)}')");
    }

    [Test]
    public void WhenCheckingIfCollectionEmpty_GivenCollectionIsNotEmpty_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfEmpty()
         .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
    }

    [Test]
    public void WhenCheckingIfCollectionNotEmpty_GivenCollectionIsNotEmpty_ThenShouldError()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfNotEmpty()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection should be empty. (Parameter '{nameof(collection)}')");
    }

    [Test]
    public void WhenCheckingIfCollectionNotEmpty_GivenCollectionIsEmpty_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = Array.Empty<string>();

        // Act
       var result = collection.Error().IfNotEmpty()
         .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
    }

    [Test]
    public void WhenCheckingIfCollectionCountEquals_GivenCollectionCountEquals_ThenShouldError()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountEquals(1)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection count should not be equal to 1. (Parameter '{nameof(collection)}')");
    }

    [Test]
    public void WhenCheckingIfCollectionCountEquals_GivenCollectionCountNotEquals_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountEquals(2)
         .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
    }

    [Test]
    public void WhenCheckingIfCollectionCountNotEquals_GivenCollectionCountNotEquals_ThenShouldError()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountNotEquals(2)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection count should be equal to 2. (Parameter '{nameof(collection)}')");
    }

    [Test]
    public void WhenCheckingIfCollectionCountNotEquals_GivenCollectionCountEquals_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountNotEquals(1)
         .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
    }

    [Test]
    public void WhenCheckingIfCollectionCountGreaterThan_GivenCollectionCountGreaterThan_ThenShouldError()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountGreaterThan(0)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection count should not be greater than 0. (Parameter '{nameof(collection)}')");
    }

    [Test]
    public void WhenCheckingIfCollectionCountGreaterThan_GivenCollectionCountNotGreaterThan_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountGreaterThan(2)
         .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
    }

    [Test]
    public void WhenCheckingIfCollectionCountGreaterThan_GivenCollectionCountEquals_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountGreaterThan(1)
         .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
    }

    [Test]
    public void WhenCheckingIfCollectionCountLessThan_GivenCollectionCountLessThan_ThenShouldError()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountLessThan(2)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection count should not be less than 2. (Parameter '{nameof(collection)}')");
    }

    [Test]
    public void WhenCheckingIfCollectionCountLessThan_GivenCollectionCountNotLessThan_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountLessThan(0)
         .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
    }

    [Test]
    public void WhenCheckingIfCollectionCountLessThan_GivenCollectionCountEquals_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { 1 };

        // Act
       var result = collection.Error().IfCountLessThan(1)
         .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
    }

    [Test]
    public void WhenCheckingIfCollectionHasNullElements_GivenCollectionHasNullElements_ThenShouldError()
    {
        // Arrange
        var collection = new[] { "hey", null };

        // Act
       var result = collection.Error().IfHasNullElements()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Collection should not have null elements. (Parameter '{nameof(collection)}')");
    }

    [Test]
    public void WhenCheckingIfCollectionHasNullElements_GivenCollectionHasNoNullElements_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { "hey", "ho" };

        // Act
       var result = collection.Error().IfHasNullElements()
         .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
    }

    [Test]
    public void WhenCheckingIfCollectionContains_GivenCollectionContainsElement_ThenShouldError()
    {
        // Arrange
        var collection = new[] { "hey", null, "ho" };
        var collection2 = new[] { 1, 2 };

        // Act
       var result = collection.Error().IfContains("ho")
         .Match(_ => null, exception => exception as ArgumentException);
       var result2 = collection2.Error().IfContains(1)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"Collection should not contain element. (Parameter '{nameof(collection)}')");
        result2!.Message.Should().Be($"Collection should not contain element. (Parameter '{nameof(collection2)}')");
    }

    [Test]
    public void WhenCheckingIfCollectionContains_GivenCollectionNotContainsElement_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { "hey", null, "ho" };
        var collection2 = new[] { 1, 2 };

        // Act
       var result = collection.Error().IfContains("ho1")
         .Match(success => success.Value, error => null!);

        var results2 = collection2.Error().IfContains(3)
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
        results2.Should().BeEquivalentTo(collection2);
    }

    [Test]
    public void WhenCheckingIfCollectionNotContains_GivenCollectionNotContainsElement_ThenShouldError()
    {
        // Arrange
        var collection = new[] { "hey", null, "ho" };
        var collection2 = new[] { 1, 2 };

        // Act
       var result = collection.Error().IfNotContains("ho1")
         .Match(_ => null, exception => exception as ArgumentException);
    
        var result2 = collection2.Error().IfNotContains(3)
          .Match(_ => null, exception => exception as ArgumentException); 

    // Assert
      result!.Message.Should().Be($"Collection should contain element. (Parameter '{nameof(collection)}')");
      result2!.Message.Should().Be($"Collection should contain element. (Parameter '{nameof(collection2)}')");
    }

    [Test]
    public void WhenCheckingIfCollectionNotContains_GivenCollectionContainsElement_ThenShouldBeEquivalentTo()
    {
        // Arrange
        var collection = new[] { "hey", null, "ho" };
        var collection2 = new[] { 1, 2 };

        // Act
       var result = collection.Error().IfNotContains("hey")
         .Match(success => success.Value, error => null!);

        var result2 = collection2.Error().IfNotContains(1)
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().BeEquivalentTo(collection);
        result2.Should().BeEquivalentTo(collection2);
    }
}