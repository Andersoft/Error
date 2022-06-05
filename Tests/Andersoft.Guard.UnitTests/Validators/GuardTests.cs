using Andersoft.Error.UnitTests.Validators.Strings;
using Andersoft.Error.Validators;
using Andersoft.Error.Validators.DateTimeValidators;
using Andersoft.Error.Validators.Strings;
using FluentAssertions;
using LanguageExt;
using LanguageExt.Common;

namespace Andersoft.Error.UnitTests.Validators
{
  public class GuardTests
  {
    [Test]
    public void WhenInspectingVariables_GivenVariablesAreValid_ThenReturnsSuccessfulResult()
    {
      // Arrange
      var firstname = "any name";
      var lastname = "any name";
      var birthday = System.DateTime.UtcNow;

      // Act
      var result = Guard.Inspect(firstname.Error().IfEmpty())
                        .And(lastname.Error().IfEmpty())
                        .And(birthday.Error().IfNotUtc())
                        .AggregateErrors()
                        .Match(_ => Unit.Default, error => new Result<Unit>(error));

      // Assert
      result.IsSuccess.Should().BeTrue();
    }

    [Test]
    public void WhenInspectingVariables_GivenVariablesAreValid_ThenReturnsFaultedAsFalse()
    {
      // Arrange
      var firstname = "any name";
      var lastname = "any name";
      var birthday = System.DateTime.UtcNow;

      // Act
      var result = Guard.Inspect(firstname.Error().IfEmpty())
        .And(lastname.Error().IfEmpty())
        .And(birthday.Error().IfNotUtc())
        .AggregateErrors()
        .Match(_ => Unit.Default, error => new Result<Unit>(error));

      // Assert
      result.IsFaulted.Should().BeFalse();
    }

    [Test]
    public void WhenInspectingVariables_GivenFirstnameIsInvalid_ThenReturnsFailureResult()
    {
      // Arrange
      var firstname = "";
      var lastname = "any name";
      var birthday = System.DateTime.UtcNow;

      // Act
      var result = Guard.Inspect(firstname.Error().IfEmpty())
        .And(lastname.Error().IfEmpty())
        .And(birthday.Error().IfNotUtc())
        .AggregateErrors()
        .Match(_ => Unit.Default, error => new Result<Unit>(error));

      // Assert
      result.IsFaulted.Should().BeTrue();
    }

    [Test]
    public void WhenInspectingVariables_GivenFirstnameIsInvalid_ThenReturnsSuccessfulAsFalse()
    {
      // Arrange
      var firstname = "";
      var lastname = "any name";
      var birthday = System.DateTime.UtcNow;

      // Act
      var result = Guard.Inspect(firstname.Error().IfEmpty())
        .And(lastname.Error().IfEmpty())
        .And(birthday.Error().IfNotUtc())
        .AggregateErrors()
        .Match(_ => Unit.Default, error => new Result<Unit>(error));

      // Assert
      result.IsSuccess.Should().BeFalse();
    }

    [Test]
    public void WhenInspectingVariables_GivenFirstnameIsInvalid_ThenReturnsError()
    {
      // Arrange
      var firstname = "";
      var lastname = "any name";
      var birthday = System.DateTime.UtcNow;

      // Act
      var result = Guard.Inspect(firstname.Error().IfEmpty())
        .And(lastname.Error().IfEmpty())
        .And(birthday.Error().IfNotUtc())
        .AggregateErrors()
        .Match(_ => default!, error => (AggregateException)error);

      // Assert
      result.Message.Should().Be($"One or more errors occurred. (String should not be empty. (Parameter '{nameof(firstname)}'))");
    }

    [Test]
    public void WhenInspectingVariables_GivenFirstnameAndLastnameIsInvalid_ThenReturnsError()
    {
      // Arrange
      var firstname = "";
      var lastname = "";
      var birthday = System.DateTime.UtcNow;

      // Act
      var result = Guard.Inspect(firstname.Error().IfEmpty())
        .And(lastname.Error().IfEmpty())
        .And(birthday.Error().IfNotUtc())
        .AggregateErrors()
        .Match(_ => default!, error => (AggregateException)error);

      // Assert
      result.Message.Should().Be($"One or more errors occurred. (String should not be empty. (Parameter '{nameof(firstname)}')) (String should not be empty. (Parameter '{nameof(lastname)}'))");
    }

    [Test]
    public void WhenInspectingVariables_GivenFirstnameAndLastnameIsInvalid_ThenThrowsException()
    {
      // Arrange
      var firstname = "";
      var lastname = "";
      var birthday = System.DateTime.UtcNow;

      // Act
      Action result = () => Guard.Inspect(firstname.Error().IfEmpty())
        .And(lastname.Error().IfEmpty())
        .And(birthday.Error().IfNotUtc())
        .ThrowOnError();

      // Assert
      result.Should()
        .Throw<AggregateException>()
        .WithMessage($"One or more errors occurred. (String should not be empty. (Parameter '{nameof(firstname)}')) (String should not be empty. (Parameter '{nameof(lastname)}'))");
    }

    [Test]
    public void WhenInspectingVariables_GivenFirstnameAndLastnameIsInvalid_ThenReturnsListOfErrors()
    {
      // Arrange
      var firstname = "";
      var lastname = "";
      var birthday = System.DateTime.UtcNow;

      // Act
      var result = Guard.Inspect(firstname.Error().IfEmpty())
        .And(lastname.Error().IfEmpty())
        .And(birthday.Error().IfNotUtc());

      // Assert
      result.Should().HaveCount(2);
    }
  }
}
