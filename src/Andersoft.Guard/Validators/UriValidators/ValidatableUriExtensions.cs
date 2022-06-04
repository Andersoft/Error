using LanguageExt.Common;

namespace Andersoft.Guard.Validators.UriValidators;

public static class ValidatableUriExtensions
{
  public static Result<Uri> IfScheme(this Validatable<Uri> validatable, string scheme)
  {
    if (validatable.Value.Scheme == scheme)
    {
      return new Result<Uri>(new ArgumentException($"Uri scheme should not be {scheme}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<Uri> IfNotScheme(this Validatable<Uri> validatable, string scheme)
  {
    if (validatable.Value.Scheme != scheme)
    {
      return new Result<Uri>(new ArgumentException($"Uri scheme should be {scheme}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<Uri> IfAbsolute(this Validatable<Uri> validatable)
  {
    if (validatable.Value.IsAbsoluteUri)
    {
      return new Result<Uri>(new ArgumentException("Uri should be relative.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<Uri> IfNotAbsolute(this Validatable<Uri> validatable)
  {
    return IfRelative(validatable);
  }

  public static Result<Uri> IfRelative(this Validatable<Uri> validatable)
  {
    if (!validatable.Value.IsAbsoluteUri)
    {
      return new Result<Uri>(new ArgumentException("Uri should be absolute.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<Uri> IfNotRelative(this Validatable<Uri> validatable)
  {
    return IfAbsolute(validatable);
  }

  public static Result<Uri> IfHttp(this Validatable<Uri> validatable)
  {
    return IfScheme(validatable, Uri.UriSchemeHttp);
  }

  public static Result<Uri> IfNotHttp(this Validatable<Uri> validatable)
  {
    return IfNotScheme(validatable, Uri.UriSchemeHttp);
  }

  public static Result<Uri> IfHttps(this Validatable<Uri> validatable)
  {
    return IfScheme(validatable, Uri.UriSchemeHttps);
  }

  public static Result<Uri> IfNotHttps(this Validatable<Uri> validatable)
  {
    return IfNotScheme(validatable, Uri.UriSchemeHttps);
  }
  public static Result<Uri> IfPort(this Validatable<Uri> validatable, int port)
  {
    if (validatable.Value.Port == port)
    {
      return new Result<Uri>(new ArgumentException($"Uri port should not be {port}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<Uri> IfNotPort(this Validatable<Uri> validatable, int port)
  {
    if (validatable.Value.Port != port)
    {
      return new Result<Uri>(new ArgumentException($"Uri port should be {port}.", validatable.ParamName));
    }

    return validatable.Value;
  }
}