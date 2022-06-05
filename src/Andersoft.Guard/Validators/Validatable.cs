namespace Andersoft.Error.Validators;

public record class Validatable<TValue>(
  TValue Value,
  string ParamName)
  where TValue : notnull;
