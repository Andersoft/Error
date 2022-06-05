using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.UriValidators;

public static class ValidatableUriPropertyExtensions
{
  public static Result<TValue> IfScheme<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func,
    string scheme,
    [CallerArgumentExpression("func")]string? funcName = null) where TValue : notnull
  {
    if (func(validatable.Value).Scheme == scheme)
    {
      return new Result<TValue>(new ArgumentException($"Uri scheme should not be {scheme}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotScheme<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    string scheme, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    if (func(validatable.Value).Scheme != scheme)
    {
      return new Result<TValue>(new ArgumentException($"Uri scheme should be {scheme}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfAbsolute<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    if (func(validatable.Value).IsAbsoluteUri)
    {
      return new Result<TValue>(new ArgumentException("Uri should be relative.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotAbsolute<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfRelative(validatable, func, funcName);
  }

  public static Result<TValue> IfRelative<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    if (!func(validatable.Value).IsAbsoluteUri)
    {
      return new Result<TValue>(new ArgumentException("Uri should be absolute.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotRelative<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfAbsolute(validatable, func, funcName);
  }

  public static Result<TValue> IfHttp<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfScheme(validatable, func, Uri.UriSchemeHttp, funcName);
  }

  public static Result<TValue> IfNotHttp<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfNotScheme(validatable,func, Uri.UriSchemeHttp, funcName);
  }

  public static Result<TValue> IfHttps<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfScheme(validatable, func, Uri.UriSchemeHttps, funcName);
  }

  public static Result<TValue> IfNotHttps<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    return IfNotScheme(validatable, func, Uri.UriSchemeHttps, funcName);
  }
  public static Result<TValue> IfPort<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    int port, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    if (func(validatable.Value).Port == port)
    {
      return new Result<TValue>(new ArgumentException($"Uri port should not be {port}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotPort<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, Uri> func, 
    int port, 
    [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
  {
    if (func(validatable.Value).Port != port)
    {
      return new Result<TValue>(new ArgumentException($"Uri port should be {port}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }
}