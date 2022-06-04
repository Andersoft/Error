using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.DateTimeValidators;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.DateTime;


public class DateTimesTests
{
    [Test]
    public void WhenCheckingIfUtc_WhenDateTimeIsUtc_ThenShouldError()
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
    public void WhenCheckingIfUtc_WhenDateTimeIsNotUtc_ThenShouldNotError()
    {
        // Arrange
        var dateTime = System.DateTime.Now;

        // Act
       var result = dateTime.Error().IfUtc();

        // Assert
        result.Should().Be(dateTime);
    }

    [Test]
    public void WhenCheckingIfNotUtc_WhenDateTimeIsNotUtc_ThenShouldError()
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
    public void WhenCheckingIfNotUtc_WhenDateTimeIsUtc_ThenShouldNotError()
    {
        // Arrange
        var dateTime = System.DateTime.UtcNow;

        // Act
       var result = dateTime.Error().IfNotUtc();

        // Assert
        result.Should().Be(dateTime);
    }

    [Test]
    public void WhenCheckingIfDateTimeKind_WhenDateTimeKindEquals_ThenShouldError()
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
    public void WhenCheckingIfDateTimeKind_WhenDateTimeKindNotEquals_ThenShouldNotError()
    {
        // Arrange
        var dateTime = System.DateTime.Now;

        // Act
       var result = dateTime.Error().IfDateTimeKind(DateTimeKind.Utc);

        // Assert
        result.Should().Be(dateTime);
    }

    [Test]
    public void WhenCheckingIfDateTimeKindNot_WhenDateTimeKindEquals_ThenShouldNotError()
    {
        // Arrange
        var dateTime = System.DateTime.UtcNow;

        // Act
       var result = dateTime.Error().IfDateTimeNotKind(DateTimeKind.Utc);

        // Assert
        result.Should().Be(dateTime);
    }

    [Test]
    public void WhenCheckingIfDateTimeKindNot_WhenDateTimeKindNotEquals_ThenShouldError()
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