using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.DateTimeValidators;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.DateTime;


public class DateTimesTests
{
    [Test]
    public void WhenCheckingIfUtc_GivenDateTimeIsUtc_ThenShouldError()
    {
        // Arrange
        var dateTime = System.DateTime.UtcNow;

        // Act
       var result = dateTime.Error().IfUtc()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be Utc. (Parameter '{nameof(dateTime)}')");
    }

    [Test]
    public void WhenCheckingIfUtc_GivenDateTimeIsNotUtc_ThenShouldNotError()
    {
        // Arrange
        var dateTime = System.DateTime.Now;

        // Act
       var result = dateTime.Error().IfUtc();

        // Assert
        result.Should().Be(dateTime);
    }

    [Test]
    public void WhenCheckingIfNotUtc_GivenDateTimeIsNotUtc_ThenShouldError()
    {
        // Arrange
        var dateTime = System.DateTime.Now;

        // Act
       var result = dateTime.Error().IfNotUtc()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be Utc. (Parameter '{nameof(dateTime)}')");
    }

    [Test]
    public void WhenCheckingIfNotUtc_GivenDateTimeIsUtc_ThenShouldNotError()
    {
        // Arrange
        var dateTime = System.DateTime.UtcNow;

        // Act
       var result = dateTime.Error().IfNotUtc();

        // Assert
        result.Should().Be(dateTime);
    }

    [Test]
    public void WhenCheckingIfDateTimeKind_GivenDateTimeKindEquals_ThenShouldError()
    {
        // Arrange
        var dateTime = System.DateTime.UtcNow;

        // Act
       var result = dateTime.Error().IfDateTimeKind(DateTimeKind.Utc)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be Utc. (Parameter '{nameof(dateTime)}')");
    }

    [Test]
    public void WhenCheckingIfDateTimeKind_GivenDateTimeKindNotEquals_ThenShouldNotError()
    {
        // Arrange
        var dateTime = System.DateTime.Now;

        // Act
       var result = dateTime.Error().IfDateTimeKind(DateTimeKind.Utc);

        // Assert
        result.Should().Be(dateTime);
    }

    [Test]
    public void WhenCheckingIfDateTimeKindNot_GivenDateTimeKindEquals_ThenShouldNotError()
    {
        // Arrange
        var dateTime = System.DateTime.UtcNow;

        // Act
       var result = dateTime.Error().IfDateTimeNotKind(DateTimeKind.Utc);

        // Assert
        result.Should().Be(dateTime);
    }

    [Test]
    public void WhenCheckingIfDateTimeKindNot_GivenDateTimeKindNotEquals_ThenShouldError()
    {
        // Arrange
        var dateTime = System.DateTime.Now;

        // Act
       var result = dateTime.Error().IfDateTimeNotKind(DateTimeKind.Utc)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be Utc. (Parameter '{nameof(dateTime)}')");
    }
}