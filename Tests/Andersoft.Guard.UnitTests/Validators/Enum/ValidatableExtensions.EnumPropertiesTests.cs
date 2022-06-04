using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.EnumValidators;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Enum;


public class EnumPropertiesTests
{
    [Test]
    public void WhenCheckingIfEnumPropertyOutOfRange_WhenValueIsOutOfRange_ThenShouldError()
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
    public void WhenCheckingIfEnumPropertyOutOfRange_WhenValueIsInRange_ThenShouldNotError()
    {
        // Arrange
        var person = new { PersonType = PersonType.NotFunny };

        // Act
       var result = person.Error().IfOutOfRange(p => p.PersonType);

        // Assert
        result.Should().Be(person);
    }

    private enum PersonType
    {
        Funny,
        NotFunny,
    }
}