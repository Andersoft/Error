using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.EnumValidators;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Enum;


public class EnumsTests
{
    [Test]
    public void WhenCheckingIfEnumOutOfRange_WhenValueIsOutOfRange_ThenShouldError()
    {
        // Arrange
        TestEnum value = (TestEnum)4;

        // Act
       var result = value.Error().IfOutOfRange()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be defined in enum. (Parameter '{nameof(value)}'){Environment.NewLine}Actual value was {value}.");
    }

    [Test]
    public void WhenCheckingIfEnumOutOfRange_WhenValueIsInRange_ThenShouldNotError()
    {
        // Arrange
        TestEnum value = TestEnum.Value1;

        // Act
       var result = value.Error().IfOutOfRange();

        // Assert
        result.Should().Be(value);
    }

    private enum TestEnum
    {
        Value1,
        Value2,
    }
}