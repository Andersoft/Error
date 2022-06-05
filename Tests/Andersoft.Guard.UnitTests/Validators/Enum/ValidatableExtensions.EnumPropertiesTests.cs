using Andersoft.Error.Validators;
using Andersoft.Error.Validators.EnumValidators;
using FluentAssertions;

namespace Andersoft.Error.UnitTests.Validators.Enum;


public class EnumPropertiesTests
{
    [Test]
    public void WhenCheckingIfEnumPropertyOutOfRange_GivenValueIsOutOfRange_ThenShouldError()
    {
        // Arrange
        var person = new { PersonType = (PersonType)10 };

        // Act
       var result = person.Error().IfOutOfRange(p => p.PersonType)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be defined in enum. (Parameter '{nameof(person)}: p => p.PersonType'){Environment.NewLine}Actual value was {person.PersonType}.");
    }

    [Test]
    public void WhenCheckingIfEnumPropertyOutOfRange_GivenValueIsInRange_ThenShouldNotError()
    {
        // Arrange
        var person = new { PersonType = PersonType.NotFunny };

        // Act
       var result = person.Error().IfOutOfRange(p => p.PersonType).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(person);
    }

    private enum PersonType
    {
        Funny,
        NotFunny,
    }
}