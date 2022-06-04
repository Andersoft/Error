namespace Andersoft.Guard.Validators;

public record class Validatable<TValue>(TValue Value, string ParamName);