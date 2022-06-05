using LanguageExt.Common;

namespace Andersoft.Guard.Validators.UriValidators;

public static class ValidatableUriExtensions
{
  public static Result<Validatable<Uri>> IfScheme(this Result<Validatable<Uri>> result, string scheme)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<Uri>> Validate(Validatable<Uri> validatable)
    {
      if (validatable.Value.Scheme == scheme)
      {
        return new Result<Validatable<Uri>>(new ArgumentException($"Uri scheme should not be {scheme}.",
          validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<Uri>> IfNotScheme(this Result<Validatable<Uri>> result, string scheme)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<Uri>> Validate(Validatable<Uri> validatable)
    {
      if (validatable.Value.Scheme != scheme)
      {
        return new Result<Validatable<Uri>>(new ArgumentException($"Uri scheme should be {scheme}.", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<Uri>> IfAbsolute(this Result<Validatable<Uri>> result)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<Uri>> Validate(Validatable<Uri> validatable)
    {
      if (validatable.Value.IsAbsoluteUri)
      {
        return new Result<Validatable<Uri>>(new ArgumentException("Uri should be relative.", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<Uri>> IfNotAbsolute(this Result<Validatable<Uri>> result)
  {
    return IfRelative(result);
  }

  public static Result<Validatable<Uri>> IfRelative(this Result<Validatable<Uri>> result)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<Uri>> Validate(Validatable<Uri> validatable)
    {
      if (!validatable.Value.IsAbsoluteUri)
      {
        return new Result<Validatable<Uri>>(new ArgumentException("Uri should be absolute.", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<Uri>> IfNotRelative(this Result<Validatable<Uri>> result)
  {
    return IfAbsolute(result);
  }

  public static Result<Validatable<Uri>> IfHttp(this Result<Validatable<Uri>> result)
  {
    return IfScheme(result, Uri.UriSchemeHttp);
  }

  public static Result<Validatable<Uri>> IfNotHttp(this Result<Validatable<Uri>> result)
  {
    return IfNotScheme(result, Uri.UriSchemeHttp);
  }

  public static Result<Validatable<Uri>> IfHttps(this Result<Validatable<Uri>> result)
  {
    return IfScheme(result, Uri.UriSchemeHttps);
  }

  public static Result<Validatable<Uri>> IfNotHttps(this Result<Validatable<Uri>> result)
  {
    return IfNotScheme(result, Uri.UriSchemeHttps);
  }
  public static Result<Validatable<Uri>> IfPort(this Result<Validatable<Uri>> result, int port)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<Uri>> Validate(Validatable<Uri> validatable)
    {
      if (validatable.Value.Port == port)
      {
        return new Result<Validatable<Uri>>(new ArgumentException($"Uri port should not be {port}.", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<Uri>> IfNotPort(this Result<Validatable<Uri>> result, int port)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<Uri>> Validate(Validatable<Uri> validatable)
    {
      if (validatable.Value.Port != port)
      {
        return new Result<Validatable<Uri>>(new ArgumentException($"Uri port should be {port}.", validatable.ParamName));
      }

      return validatable;
    }
  }
}