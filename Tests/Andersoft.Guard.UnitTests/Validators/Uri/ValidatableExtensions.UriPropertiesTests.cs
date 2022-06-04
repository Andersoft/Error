using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.UriValidators;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Uri;


public class UriPropertiesTests
{
    [Test]
    public void WhenCheckingIfUriPropertyHttp_GivenUriPropertyIsHttp_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com") };

        // Act
       var result = person.Error().IfHttp(p => p.Uri)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should not be http. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyHttp_GivenUriPropertyIsHttps_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("https://www.google.com") };

        // Act
       var result = person.Error().IfHttp(p => p.Uri);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertyNotHttp_GivenUriPropertyIsNotHttp_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("https://www.google.com") };

        // Act
       var result = person.Error().IfNotHttp(p => p.Uri)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should be http. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyNotHttp_GivenUriPropertyIsHttp_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com") };

        // Act
       var result = person.Error().IfNotHttp(p => p.Uri);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertyHttps_GivenUriPropertyIsHttps_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("https://www.google.com") };

        // Act
       var result = person.Error().IfHttps(p => p.Uri)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should not be https. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyHttps_GivenUriPropertyIsHttp_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com") };

        // Act
       var result = person.Error().IfHttps(p => p.Uri);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertyNotHttps_GivenUriPropertyIsNotHttps_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com") };

        // Act
       var result = person.Error().IfNotHttps(p => p.Uri)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should be https. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyNotHttps_GivenUriPropertyIsHttps_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("https://www.google.com") };

        // Act
       var result = person.Error().IfNotHttps(p => p.Uri);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertySchemeEquals_GivenUriPropertySchemeEquals_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri($"{System.Uri.UriSchemeGopher}://www.google.com") };

        // Act
       var result = person.Error().IfScheme(p => p.Uri, System.Uri.UriSchemeGopher)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should not be {System.Uri.UriSchemeGopher}. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertySchemeEquals_GivenUriPropertySchemeNotEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri($"{System.Uri.UriSchemeHttp}://www.google.com") };

        // Act
       var result = person.Error().IfScheme(p => p.Uri, System.Uri.UriSchemeGopher);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertySchemeNotEquals_GivenUriPropertySchemeNotEquals_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri($"{System.Uri.UriSchemeHttp}://www.google.com") };

        // Act
       var result = person.Error().IfNotScheme(p => p.Uri, System.Uri.UriSchemeGopher)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should be {System.Uri.UriSchemeGopher}. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertySchemeNotEquals_GivenUriPropertySchemeEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri($"{System.Uri.UriSchemeGopher}://www.google.com") };

        // Act
       var result = person.Error().IfNotScheme(p => p.Uri, System.Uri.UriSchemeGopher);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertyAbsolute_GivenAbsolute_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com") };

        // Act
       var result = person.Error().IfAbsolute(p => p.Uri)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri should be relative. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyAbsolute_GivenRelative_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("/path/to/file", UriKind.Relative) };

        // Act
       var result = person.Error().IfAbsolute(p => p.Uri);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertyRelative_GivenRelative_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("/path/to/file", UriKind.Relative) };

        // Act
       var result = person.Error().IfRelative(p => p.Uri)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri should be absolute. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyRelative_GivenAbsolute_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com") };

        // Act
       var result = person.Error().IfRelative(p => p.Uri);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertyNotAbsolute_GivenNotAbsolute_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("/path/to/file", UriKind.Relative) };

        // Act
       var result = person.Error().IfNotAbsolute(p => p.Uri)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri should be absolute. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyNotAbsolute_GivenAbsolute_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com") };

        // Act
       var result = person.Error().IfNotAbsolute(p => p.Uri);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertyNotRelative_GivenNotRelative_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com") };

        // Act
       var result = person.Error().IfNotRelative(p => p.Uri)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri should be relative. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyNotRelative_GivenRelative_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("/path/to/file", UriKind.Relative) };

        // Act
       var result = person.Error().IfNotRelative(p => p.Uri);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertyPortEquals_GivenUriPropertyPortEquals_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com:80") };

        // Act
       var result = person.Error().IfPort(p => p.Uri, 80)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri port should not be 80. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyPortEquals_GivenUriPropertyPortNotEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com:8080") };

        // Act
       var result = person.Error().IfPort(p => p.Uri, 80);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfUriPropertyPortNotEquals_GivenUriPropertyPortNotEquals_ThenShouldError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com:8080") };

        // Act
       var result = person.Error().IfNotPort(p => p.Uri, 80)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri port should be 80. (Parameter '{nameof(person)}: p => p.Uri')");
    }

    [Test]
    public void WhenCheckingIfUriPropertyPortNotEquals_GivenUriPropertyPortEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Uri = new System.Uri("http://www.google.com:80") };

        // Act
       var result = person.Error().IfNotPort(p => p.Uri, 80);

        // Assert
        result.Should().Be(person);
    }
}