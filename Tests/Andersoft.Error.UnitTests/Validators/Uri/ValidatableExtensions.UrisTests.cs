using Andersoft.Error.Validators;
using Andersoft.Error.Validators.UriValidators;
using FluentAssertions;

namespace Andersoft.Error.UnitTests.Validators.Uri;


public class UrisTests
{
    [Test]
    public void WhenCheckingIfHttp_GivenHttp_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfHttp()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should not be http. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfHttp_GivenHttps_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("https://www.google.com");

        // Act
       var result = uri.Error().IfHttp().Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfNotHttp_GivenNotHttp_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("https://www.google.com");

        // Act
       var result = uri.Error().IfNotHttp()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should be http. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfNotHttp_GivenHttp_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfNotHttp().Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfHttps_GivenHttps_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("https://www.google.com");

        // Act
       var result = uri.Error().IfHttps()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should not be https. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfHttps_GivenHttp_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfHttps().Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfNotHttps_GivenNotHttps_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfNotHttps()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should be https. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfNotHttps_GivenHttps_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("https://www.google.com");

        // Act
       var result = uri.Error().IfNotHttps().Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfSchemeEquals_GivenEquals_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri($"{System.Uri.UriSchemeFtp}://www.google.com");

        // Act
       var result = uri.Error().IfScheme(System.Uri.UriSchemeFtp)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should not be {System.Uri.UriSchemeFtp}. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfSchemeEquals_GivenNotEquals_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri($"{System.Uri.UriSchemeHttp}://www.google.com");

        // Act
       var result = uri.Error().IfScheme(System.Uri.UriSchemeFtp).Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfSchemeNotEquals_GivenNotEquals_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri($"{System.Uri.UriSchemeHttp}://www.google.com");

        // Act
       var result = uri.Error().IfNotScheme(System.Uri.UriSchemeFtp)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri scheme should be {System.Uri.UriSchemeFtp}. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfSchemeNotEquals_GivenEquals_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri($"{System.Uri.UriSchemeFtp}://www.google.com");

        // Act
       var result = uri.Error().IfNotScheme(System.Uri.UriSchemeFtp).Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfAbsolute_GivenAbsolute_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfAbsolute()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri should be relative. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfAbsolute_GivenNotAbsolute_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("/path/to/file", UriKind.Relative);

        // Act
       var result = uri.Error().IfAbsolute().Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfRelative_GivenRelative_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("/path/to/file", UriKind.Relative);

        // Act
       var result = uri.Error().IfRelative()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri should be absolute. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfRelative_GivenNotRelative_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfRelative().Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfNotAbsolute_GivenNotAbsolute_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("/path/to/file", UriKind.Relative);

        // Act
       var result = uri.Error().IfNotAbsolute()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri should be absolute. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfNotAbsolute_GivenAbsolute_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfNotAbsolute().Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfNotRelative_GivenNotRelative_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfNotRelative()
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri should be relative. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfNotRelative_GivenRelative_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("/path/to/file", UriKind.Relative);

        // Act
       var result = uri.Error().IfNotRelative().Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfPortEquals_GivenPortEquals_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com:80");

        // Act
       var result = uri.Error().IfPort(80)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri port should not be 80. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfPortEquals_GivenNotEquals_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com:8080");

        // Act
       var result = uri.Error().IfPort(80).Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfPortNotEquals_GivenNotEquals_ThenShouldError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com:8080");

        // Act
       var result = uri.Error().IfNotPort(80)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Uri port should be 80. (Parameter '{nameof(uri)}')");
    }

    [Test]
    public void WhenCheckingIfPortNotEquals_GivenEquals_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com:80");

        // Act
       var result = uri.Error().IfNotPort(80).Match(success => success.Value, error => default!);

        // Assert
        result.Should().Be(uri);
    }
}