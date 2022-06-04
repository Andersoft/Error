using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.UriValidators;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Uri;


public class UrisTests
{
    [Test]
    public void WhenCheckingIfHttp_WhenHttp_ThenShouldError()
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
    public void WhenCheckingIfHttp_WhenHttps_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("https://www.google.com");

        // Act
       var result = uri.Error().IfHttp();

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfNotHttp_WhenNotHttp_ThenShouldError()
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
    public void WhenCheckingIfNotHttp_WhenHttp_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfNotHttp();

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfHttps_WhenHttps_ThenShouldError()
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
    public void WhenCheckingIfHttps_WhenHttp_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfHttps();

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfNotHttps_WhenNotHttps_ThenShouldError()
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
    public void WhenCheckingIfNotHttps_WhenHttps_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("https://www.google.com");

        // Act
       var result = uri.Error().IfNotHttps();

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfSchemeEquals_WhenEquals_ThenShouldError()
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
    public void WhenCheckingIfSchemeEquals_WhenNotEquals_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri($"{System.Uri.UriSchemeHttp}://www.google.com");

        // Act
       var result = uri.Error().IfScheme(System.Uri.UriSchemeFtp);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfSchemeNotEquals_WhenNotEquals_ThenShouldError()
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
    public void WhenCheckingIfSchemeNotEquals_WhenEquals_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri($"{System.Uri.UriSchemeFtp}://www.google.com");

        // Act
       var result = uri.Error().IfNotScheme(System.Uri.UriSchemeFtp);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfAbsolute_WhenAbsolute_ThenShouldError()
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
    public void WhenCheckingIfAbsolute_WhenNotAbsolute_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("/path/to/file", UriKind.Relative);

        // Act
       var result = uri.Error().IfAbsolute();

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfRelative_WhenRelative_ThenShouldError()
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
    public void WhenCheckingIfRelative_WhenNotRelative_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfRelative();

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfNotAbsolute_WhenNotAbsolute_ThenShouldError()
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
    public void WhenCheckingIfNotAbsolute_WhenAbsolute_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com");

        // Act
       var result = uri.Error().IfNotAbsolute();

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfNotRelative_WhenNotRelative_ThenShouldError()
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
    public void WhenCheckingIfNotRelative_WhenRelative_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("/path/to/file", UriKind.Relative);

        // Act
       var result = uri.Error().IfNotRelative();

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfPortEquals_WhenPortEquals_ThenShouldError()
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
    public void WhenCheckingIfPortEquals_WhenNotEquals_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com:8080");

        // Act
       var result = uri.Error().IfPort(80);

        // Assert
        result.Should().Be(uri);
    }

    [Test]
    public void WhenCheckingIfPortNotEquals_WhenNotEquals_ThenShouldError()
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
    public void WhenCheckingIfPortNotEquals_WhenEquals_ThenShouldNotError()
    {
        // Arrange
        var uri = new System.Uri("http://www.google.com:80");

        // Act
       var result = uri.Error().IfNotPort(80);

        // Assert
        result.Should().Be(uri);
    }
}