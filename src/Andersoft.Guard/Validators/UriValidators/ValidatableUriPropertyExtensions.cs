using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Error.Validators.UriValidators;

public static class ValidatableUriPropertyExtensions
{
  public static Result<Validatable<TValue>> IfScheme<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func,
    string scheme,
    [CallerArgumentExpression("func")]string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Scheme == scheme)
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"Uri scheme should not be {scheme}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotScheme<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    string scheme, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Scheme != scheme)
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"Uri scheme should be {scheme}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfAbsolute<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).IsAbsoluteUri)
      {
        return new Result<Validatable<TValue>>(new ArgumentException("Uri should be relative.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotAbsolute<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfRelative(result, func, funcName);
  }

  public static Result<Validatable<TValue>> IfRelative<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!func(validatable.Value).IsAbsoluteUri)
      {
        return new Result<Validatable<TValue>>(new ArgumentException("Uri should be absolute.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotRelative<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfAbsolute(result, func, funcName);
  }

  public static Result<Validatable<TValue>> IfHttp<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfScheme(result, func, Uri.UriSchemeHttp, funcName);
  }

  public static Result<Validatable<TValue>> IfNotHttp<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfNotScheme(result,func, Uri.UriSchemeHttp, funcName);
  }

  public static Result<Validatable<TValue>> IfHttps<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfScheme(result, func, Uri.UriSchemeHttps, funcName);
  }

  public static Result<Validatable<TValue>> IfNotHttps<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfNotScheme(result, func, Uri.UriSchemeHttps, funcName);
  }
  public static Result<Validatable<TValue>> IfPort<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    int port, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Port == port)
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"Uri port should not be {port}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotPort<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, Uri> func, 
    int port, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Port != port)
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"Uri port should be {port}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }
}