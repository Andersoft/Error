<div align="center">

---
[![Nuget](https://img.shields.io/nuget/v/Andersoft.Error)](https://www.nuget.org/packages/Andersoft.Error)

[![Build Status](https://dev.azure.com/magnoxium/Andersoft/_apis/build/status/Andersoft.Error?branchName=main)](https://dev.azure.com/magnoxium/Andersoft/_build/latest?definitionId=55&branchName=main)
### A simple, fluent, extensible, and fully customizable library for returning exceptions using .NET 6+

`dotnet add package Andersoft.Error`
---
</div>


# Credits
This project is based on the awesome project called [Throw](https://github.com/amantinband/throw). Instead of throwing exceptions i wanted to return either an error or the successful result of an operation. If you like this project Give them both a ⭐.

# Common Scenarios
Inspect input parameters using fluent syntax to either throw or return any validation errors. The static `Guard` class provides additional readability when checking parameters but is not needed as you can also chain directly off a previous validation check using `.And`.

```csharp
public static Result<Person> CreatePersonResult(string firstname, string lastname, System.DateTime birthday)
{
  return Guard
    .Inspect(firstname.Error().IfEmpty())
    .And(lastname.Error().IfEmpty())
    .And(birthday.Error().IfNotUtc())
    .AggregateErrors()
    .Match(_ => new Person(firstname, lastname, birthday), error => new Result<Person>(error));
}    
    
public static Person CreatePerson(string firstname, string lastname, System.DateTime birthday)
{
  Guard
    .Inspect(firstname.Error().IfEmpty())
    .And(lastname.Error().IfEmpty())
    .And(birthday.Error().IfNotUtc())
    .ThrowOnError();

    return new Person(firstname, lastname, birthday);
}

public static Person CreateAnotherPerson(string firstname, string lastname, System.DateTime birthday)
{
  firstname.Error().IfEmpty()
    .And(lastname.Error().IfEmpty())
    .And(birthday.Error().IfNotUtc())
    .ThrowOnError();

  return new Person(firstname, lastname, birthday);
}
```

# Extensibility

You can easily extend the library by adding your own rules.

Here is a simple example:

```csharp
"foo".Error().IfFoo(); // System.ArgumentException: String shouldn't equal 'foo' (Parameter '"foo"')
```

```csharp
namespace Andersoft.Guard
{
  public static class ValidatableStringExtensions
  {                      
    public static Result<Validatable<string>> IfFoo(this Result<Validatable<string>> result)
    {
      const string foo = "Foo";
      return result.Match(Validate, error => new Result<Validatable<string>>(error));

      Result<Validatable<string>> Validate(Validatable<string> validatable)
      {
        if (validatable.Value == foo)
        {
          return new Result<Validatable<string>>(
            new ArgumentException($"String should not be equal to {foo}.", validatable.ParamName)
            );
        }
        return validatable;
      }
    }
  }
}
```

This will behave as following:

```csharp
string example = "Foo"
// IActionResult will be BadRequest: String should not be equal to Foo.
IActionResult response = example.Error()
                                .IfFoo()
                                .Match(success => new Ok(success.Value), 
                                       error => new BadRequest(error.Message)); 
```
# Nullable vs non-nullable types

This library is designed to work best with [nullable reference types feature](https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references) enabled.

The `Error()` method is the entry method for all non-nullable types:

```csharp
string name = "hello";
name.Error().IfLongerThan(10);
```

And `ErrorIfNull()` for any nullable type:

```csharp
string? name = "hello";
name.ErrorIfNull();
```

Trying to use `Error()` on a nullable type will give a warning

```csharp
string? name = null;
name.Error() // Warning	CS8714	The type 'string?' cannot be used as type parameter 'TValue' in the generic type or method 'ValidatableExtensions.Error<TValue>(TValue, string?)'. Nullability of type argument 'string?' doesn't match 'notnull' constraint.
.IfEmpty();
```

After validating the nullable type isn't null, all the regular non-nullable rules can be used

```csharp
name.ErrorIfNull()
    .IfEmpty()
    .IfLongerThan(3);
```

The expression can be implicitly cast to the non-nullable type of the original nullable type


```csharp
string? name = "Jordan";
string nonNullableName = name.ErrorIfNull()
    .IfEmpty()
    .IfLongerThan(10);
```

or

```csharp
int? a = 5;
int b = a.ErrorIfNull();
```

# Usage

## Common types

### Booleans

```csharp
value.Error().IfTrue(); // ArgumentException: Value should not be true (Parameter 'value')
value.Error().IfFalse(); // ArgumentException: Value should be true (Parameter 'value')

// Any method which returns bool can inline it's error logic.
Enum.TryParse("Unexpected value", out EmployeeType value)
    .Error()
    .IfFalse(); // System.ArgumentException: Value should be true. (Parameter 'Enum.TryParse("Unexpected value", out EmployeeType value)')
```

### Nullable value types (`bool?`, `int?`, `double?`, `DateTime?` etc.)

```csharp
bool? value = null;

value.ErrorIfNull(); // ArgumentNullException: Value cannot be null. (Parameter 'value')

// After validating `ErrorIfNull`, any of the regular value type extensions can be used.
value.ErrorIfNull() // ArgumentNullException: Value cannot be null. (Parameter 'value')
    .IfTrue(); // ArgumentException: Value should not be true (Parameter 'value')

// The returned value from `ErrorIfNull` can be implicitly cast to the original non-nullable type.
bool nonNullableValue = value.ErrorIfNull(); // ArgumentNullException: Value cannot be null. (Parameter 'value')

```

### Strings

```csharp
name.Error().IfEmpty(); // System.ArgumentException: String should not be empty. (Parameter 'name')
name.Error().IfWhiteSpace(); // System.ArgumentException: String should not be white space only. (Parameter 'name')
name.Error().IfLengthEquals(7); // System.ArgumentException: String length should not be equal to 7. (Parameter 'name')
name.Error().IfLengthNotEquals(10); // System.ArgumentException: String length should be equal to 10. (Parameter 'name')
name.Error().IfShorterThan(10); // System.ArgumentException: String should not be shorter than 10 characters. (Parameter 'name')
name.Error().IfLongerThan(3); // System.ArgumentException: String should not be longer than 3 characters. (Parameter 'name')
name.Error().IfEquals("Jordan"); // System.ArgumentException: String should not be equal to 'Jordan' (comparison type: 'Ordinal'). (Parameter 'name')
name.Error().IfEquals("Jordan", StringComparison.InvariantCulture); // System.ArgumentException: String should not be equal to 'Jordan' (comparison type: 'InvariantCulture'). (Parameter 'name')
name.Error().IfEqualsIgnoreCase("Jordan"); // System.ArgumentException: String should not be equal to 'Jordan' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'name')
name.Error().IfNotEquals("Dan"); // System.ArgumentException: String should be equal to 'Dan' (comparison type: 'Ordinal'). (Parameter 'name')
name.Error().IfNotEquals("Dan", StringComparison.InvariantCultureIgnoreCase); // System.ArgumentException: String should be equal to 'Dan' (comparison type: 'InvariantCultureIgnoreCase'). (Parameter 'name')
name.Error().IfNotEqualsIgnoreCase("Dan"); // System.ArgumentException: String should be equal to 'Dan' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'name')
name.Error().IfContains("substring"); // System.ArgumentException: String should not contain 'substring' (comparison type: 'Ordinal'). (Parameter 'name')
name.Error().IfContains("substring", ComparisonType.InvariantCulture); // System.ArgumentException: String should contain 'substring' (comparison type: 'InvariantCulture'). (Parameter 'name')
name.Error().IfNotContains("substring"); // System.ArgumentException: String should contain 'substring' (comparison type: 'Ordinal'). (Parameter 'name')
name.Error().IfNotContains("substring", ComparisonType.InvariantCultureIgnoreCase); // System.ArgumentException: String should contain 'substring' (comparison type: 'InvariantCultureIgnoreCase'). (Parameter 'name')
name.Error().IfStartsWith("Jer"); // System.ArgumentException: String should not start with 'Jer' (comparison type: 'Ordinal'). (Parameter 'name')
name.Error().IfStartsWith("JER", StringComparison.OrdinalIgnoreCase); // System.ArgumentException: String should not start with 'JER' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'name')
name.Error().IfNotStartsWith("dan"); // System.ArgumentException: String should start with 'dan' (comparison type: 'Ordinal'). (Parameter 'name')
name.Error().IfNotStartsWith("dan", StringComparison.InvariantCultureIgnoreCase); // System.ArgumentException: String should start with 'dan' (comparison type: 'InvariantCultureIgnoreCase'). (Parameter 'name')
name.Error().IfEndsWith("emy"); // System.ArgumentException: String should not end with 'emy' (comparison type: 'Ordinal'). (Parameter 'name')
name.Error().IfEndsWith("EMY", StringComparison.OrdinalIgnoreCase); // System.ArgumentException: String should not end with 'EMY' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'name')
name.Error().IfNotEndsWith("dan"); // System.ArgumentException: String should end with 'dan' (comparison type: 'Ordinal'). (Parameter 'name')
name.Error().IfNotEndsWith("dan", StringComparison.OrdinalIgnoreCase); // System.ArgumentException: String should end with 'dan' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'name')
name.Error().IfMatches("J.*y"); // System.ArgumentException: String should not match RegEx pattern 'J.*y' (Parameter 'name')
name.Error().IfMatches("[a-z]{0,10}", RegexOptions.IgnoreCase); // System.ArgumentException: String should not match RegEx pattern '[a-z]{0,10}' (Parameter 'name')
name.Error().IfNotMatches("^[0-9]+$"); // System.ArgumentException: String should match RegEx pattern '^[0-9]+$' (Parameter 'name')
name.Error().IfNotMatches("abc ", RegexOptions.IgnorePatternWhitespace); // System.ArgumentException: String should match RegEx pattern '^[0-9]+$' (Parameter 'name')
```

### Collections (`IEnumerable`, `IEnumerable<T>`, `ICollection`, `ICollection<T>`, `IList`, etc.)

*Important note: if the collection is a non-evaluated expression, the expression will be evaluated.*

```csharp
collection.Error().IfHasNullElements(); // System.ArgumentException: Collection should not have null elements. (Parameter 'collection')
collection.Error().IfEmpty(); // System.ArgumentException: Collection should not be empty. (Parameter 'collection')
collection.Error().IfNotEmpty(); // System.ArgumentException: Collection should be empty. (Parameter 'collection')
collection.Error().IfCountLessThan(5); // System.ArgumentException: Collection count should not be less than 5. (Parameter 'collection')
collection.Error().IfCountGreaterThan(1); // System.ArgumentException: Collection count should not be greater than 1. (Parameter 'collection')
collection.Error().IfCountEquals(0); // System.ArgumentException: Collection count should not be equal to 0. (Parameter 'collection')
collection.Error().IfCountNotEquals(0); // System.ArgumentException: Collection count should be equal to 0. (Parameter 'collection')
collection.Error().IfContains("value"); // System.ArgumentException: Collection should not contain element. (Parameter 'person: p => p.Friends')
collection.Error().IfNotContains("value"); // System.ArgumentException: Collection should contain element. (Parameter 'person: p => p.Friends')
```

### DateTime

```csharp
dateTime.Error().IfUtc(); // System.ArgumentException: Value should not be Utc. (Parameter 'dateTime')
dateTime.Error().IfNotUtc(); // System.ArgumentException: Value should be Utc. (Parameter 'dateTime')
dateTime.Error().IfDateTimeKind(DateTimeKind.Unspecified); // System.ArgumentException: Value should not be Unspecified. (Parameter 'dateTime')
dateTime.Error().IfDateTimeKindNot(DateTimeKind.Local); // System.ArgumentException: Value should be Local. (Parameter 'dateTime')
dateTime.Error().IfGreaterThan(DateTime.Now.AddYears(-20)); // System.ArgumentOutOfRangeException: Value should not be greater than 2/28/2002 4:41:19 PM. (Parameter 'dateTime')
dateTime.Error().IfLessThan(DateTime.Now.AddYears(20)); // System.ArgumentOutOfRangeException: Value should not be less than 2/28/2042 4:41:46 PM. (Parameter 'dateTime')
dateTime.Error().IfEquals(other); // System.ArgumentException: Value should not be equal to 2/28/2022 4:44:39 PM. (Parameter 'dateTime')
```

### Enums

```csharp
employeeType.Error().IfOutOfRange(); // System.ArgumentOutOfRangeException: Value should be defined in enum. (Parameter 'employeeType')
employeeType.Error().IfEquals(EmployeeType.FullTime); // System.ArgumentException: Value should not be equal to FullTime. (Parameter 'employeeType')
```

### Equalities (non-nullables)

```csharp
dateTime.Error().IfDefault(); // System.ArgumentException: Value should not be default. (Parameter 'dateTime')
dateTime.Error().IfNotDefault(); // System.ArgumentException: Value should be default. (Parameter 'dateTime')
number.Error().IfEquals(5); // System.ArgumentException: Value should not be not be equal to 5. (Parameter 'number')
number.Error().IfNotEquals(3); // System.ArgumentException: Value should be equal to 3. (Parameter 'number')
```

### Uris

```csharp
uri.Error().IfHttps(); // System.ArgumentException: Uri scheme should not be https. (Parameter 'uri')
uri.Error().IfNotHttps(); // System.ArgumentException: Uri scheme should be https. (Parameter 'uri')
uri.Error().IfHttp(); // System.ArgumentException: Uri scheme should not be http. (Parameter 'uri')
uri.Error().IfNotHttp(); // System.ArgumentException: Uri scheme should be http. (Parameter 'uri')
uri.Error().IfScheme(Uri.UriSchemeHttp); // System.ArgumentException: Uri scheme should not be http. (Parameter 'uri')
uri.Error().IfSchemeNot(Uri.UriSchemeFtp); // System.ArgumentException: Uri scheme should be ftp. (Parameter 'uri')
uri.Error().IfPort(800); // System.ArgumentException: Uri port should not be 80. (Parameter 'uri')
uri.Error().IfPortNot(8080); // System.ArgumentException: Uri port should be 8080. (Parameter 'uri')
uri.Error().IfAbsolute(); // System.ArgumentException: Uri should be relative. (Parameter 'uri')
uri.Error().IfRelative(); // System.ArgumentException: Uri should be absolute. (Parameter 'uri')
uri.Error().IfNotAbsolute(); // System.ArgumentException: Uri should be absolute. (Parameter 'uri')
uri.Error().IfNotRelative(); // System.ArgumentException: Uri should be relative. (Parameter 'uri')
```

### Comparable (`int`, `double`, `decimal`, `long`, `float`, `short`, `DateTime`, `DateOnly`, `TimeOnly` etc.)

```csharp
number.Error().IfPositive(); // System.ArgumentOutOfRangeException: Value should not be greater than 0. (Parameter 'number')\n Actual value was 5.
number.Error().IfNegative(); // System.ArgumentOutOfRangeException: Value should not be less than 0. (Parameter 'number')\n Actual value was -5.
number.Error().IfLessThan(10); // System.ArgumentOutOfRangeException: Value should not be less than 10. (Parameter 'number')\n Actual value was 5.
number.Error().IfGreaterThan(3); // System.ArgumentOutOfRangeException: Value should not be greater than 3. (Parameter 'number')\n Actual value was 5.
number.Error().IfOutOfRange(0, 5); // System.ArgumentOutOfRangeException: Value should be between 0 and 5. (Parameter 'number')\n Actual value was -5.
```

## Nested properties

### Boolean properties

```csharp
person.Error().IfTrue(p => p.IsFunny); // System.ArgumentException: Value should not meet condition (condition: 'person => person.IsFunny'). (Parameter 'person')
person.Error().IfFalse(p => p.IsFunny); // System.ArgumentException: Value should meet condition (condition: 'person => person.IsFunny'). (Parameter 'person')

// We can inline the error logic with the method call.
Person person = GetPerson().Error().IfTrue(person => person.Age < 18); // System.ArgumentException: Value should not meet condition (condition: 'person => person.Age < 18'). (Parameter 'GetPerson()')
```

### String properties

```csharp
person.Error().IfEmpty(p => p.Name); // System.ArgumentException: String should not be empty. (Parameter 'person: p => p.Name')
person.Error().IfWhiteSpace(p => p.Name); // System.ArgumentException: String should not be white space only. (Parameter 'person: p => p.Name')
person.Error().IfNullOrWhiteSpace(p => p.Name); // System.ArgumentException: String should not be null or whitespace. (Parameter 'person: p => p.Name')
person.Error().IfNullOrEmpty(p => p.Name); // System.ArgumentException: String should not be null or empty. (Parameter 'person: p => p.Name')
person.Error().IfLengthEquals(p => p.Name, 7); // System.ArgumentException: String length should not be equal to 7. (Parameter 'person: p => p.Name')
person.Error().IfLengthNotEquals(p => p.Name, 10); // System.ArgumentException: String length should be equal to 10. (Parameter 'person: p => p.Name')
person.Error().IfShorterThan(p => p.Name, 10); // System.ArgumentException: String should not be shorter than 10 characters. (Parameter 'person: p => p.Name')
person.Error().IfLongerThan(p => p.Name, 3); // System.ArgumentException: String should not be longer than 3 characters. (Parameter 'person: p => p.Name')
person.Error().IfEquals(p => p.Name, "Jordan"); // System.ArgumentException: String should not be equal to 'Jordan' (comparison type: 'Ordinal'). (Parameter 'person: p => p.Name')
person.Error().IfEquals(p => p.Name, "Jordan", StringComparison.InvariantCulture); // System.ArgumentException: String should not be equal to 'Jordan' (comparison type: 'InvariantCulture'). (Parameter 'person: p => p.Name')
person.Error().IfEqualsIgnoreCase(p => p.Name, "Jordan"); // System.ArgumentException: String should not be equal to 'Jordan' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'person: p => p.Name')
person.Error().IfNotEquals(p => p.Name, "Dan"); // System.ArgumentException: String should be equal to 'Dan' (comparison type: 'Ordinal'). (Parameter 'person: p => p.Name')
person.Error().IfNotEquals(p => p.Name, "Dan", StringComparison.InvariantCultureIgnoreCase); // System.ArgumentException: String should be equal to 'Dan' (comparison type: 'InvariantCultureIgnoreCase'). (Parameter 'person: p => p.Name')
person.Error().IfNotEqualsIgnoreCase(p => p.Name, "Dan"); // System.ArgumentException: String should be equal to 'Dan' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'person: p => p.Name')
person.Error().IfContains(p => p.Name, "substring"); // System.ArgumentException: String should not contain 'substring' (comparison type: 'Ordinal'). (Parameter 'person: p => p.Name')
person.Error().IfContains(p => p.Name, "substring", ComparisonType.InvariantCulture); // System.ArgumentException: String should contain 'substring' (comparison type: 'InvariantCulture'). (Parameter 'person: p => p.Name')
person.Error().IfNotContains(p => p.Name, "substring"); // System.ArgumentException: String should contain 'substring' (comparison type: 'Ordinal'). (Parameter 'person: p => p.Name')
person.Error().IfNotContains(p => p.Name, "substring", ComparisonType.InvariantCultureIgnoreCase); // System.ArgumentException: String should contain 'substring' (comparison type: 'InvariantCultureIgnoreCase'). (Parameter 'person: p => p.Name')
person.Error().IfStartsWith(p => p.Name, "Jer"); // System.ArgumentException: String should not start with 'Jer' (comparison type: 'Ordinal'). (Parameter 'person: p => p.Name')
person.Error().IfStartsWith(p => p.Name, "JER", StringComparison.OrdinalIgnoreCase); // System.ArgumentException: String should not start with 'JER' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'person: p => p.Name')
person.Error().IfNotStartsWith(p => p.Name, "dan"); // System.ArgumentException: String should start with 'dan' (comparison type: 'Ordinal'). (Parameter 'person: p => p.Name')
person.Error().IfNotStartsWith(p => p.Name, "dan", StringComparison.InvariantCultureIgnoreCase); // System.ArgumentException: String should start with 'dan' (comparison type: 'InvariantCultureIgnoreCase'). (Parameter 'person: p => p.Name')
person.Error().IfEndsWith(p => p.Name, "emy"); // System.ArgumentException: String should not end with 'emy' (comparison type: 'Ordinal'). (Parameter 'person: p => p.Name')
person.Error().IfEndsWith(p => p.Name, "EMY", StringComparison.OrdinalIgnoreCase); // System.ArgumentException: String should not end with 'EMY' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'person: p => p.Name')
person.Error().IfNotEndsWith(p => p.Name, "dan"); // System.ArgumentException: String should end with 'dan' (comparison type: 'Ordinal'). (Parameter 'person: p => p.Name')
person.Error().IfNotEndsWith(p => p.Name, "dan", StringComparison.OrdinalIgnoreCase); // System.ArgumentException: String should end with 'dan' (comparison type: 'OrdinalIgnoreCase'). (Parameter 'person: p => p.Name')
person.Error().IfMatches(p => p.Name, "J.*y"); // System.ArgumentException: String should not match RegEx pattern 'J.*y' (Parameter 'person: p => p.Name')
person.Error().IfMatches(p => p.Name, "[a-z]{0,10}", RegexOptions.IgnoreCase); // System.ArgumentException: String should not match RegEx pattern '[a-z]{0,10}' (Parameter 'person: p => p.Name')
person.Error().IfNotMatches(p => p.Name, "^[0-9]+$"); // System.ArgumentException: String should match RegEx pattern '^[0-9]+$' (Parameter 'person: p => p.Name')
person.Error().IfNotMatches(p => p.Name, "abc ", RegexOptions.IgnorePatternWhitespace); // System.ArgumentException: String should match RegEx pattern '^[0-9]+$' (Parameter 'person: p => p.Name')
```

### Collection properties

```csharp
person.Error().IfHasNullElements(p => p.Friends); // System.ArgumentException: Collection should not have null elements. (Parameter 'person: p => p.Friends')
person.Error().IfEmpty(p => p.Friends); // System.ArgumentException: Collection should not be empty. (Parameter 'person: p => p.Friends')
person.Error().IfNotEmpty(p => p.Friends); // System.ArgumentException: Collection should be empty. (Parameter 'person: p => p.Friends')
person.Error().IfCountLessThan(p => p.Friends, 5); // System.ArgumentException: Collection count should not be less than 5. (Parameter 'person: p => p.Friends')
person.Error().IfCountGreaterThan(p => p.Friends, 1); // System.ArgumentException: Collection count should not be greater than 1. (Parameter 'person: p => p.Friends')
person.Error().IfCountEquals(p => p.Friends, 0); // System.ArgumentException: Collection count should not be equal to 0. (Parameter 'person: p => p.Friends')
person.Error().IfCountNotEquals(p => p.Friends, 0); // System.ArgumentException: Collection count should be equal to 0. (Parameter 'person: p => p.Friends')
person.Error().IfContains(p => p.Friends, "Jordan"); // System.ArgumentException: Collection should not contain element. (Parameter 'person: p => p.Friends')
person.Error().IfNotContains(p => p.Friends, "Jordan"); // System.ArgumentException: Collection should contain element. (Parameter 'person: p => p.Friends')
```

### DateTime properties

```csharp
person.Error().IfUtc(p => p.DateOfBirth); // System.ArgumentException: Value should not be Utc. (Parameter 'person: p => p.DateOfBirth')
person.Error().IfNotUtc(p => p.DateOfBirth); // System.ArgumentException: Value should be Utc. (Parameter 'person: p => p.DateOfBirth')
person.Error().IfDateTimeKind(p => p.DateOfBirth, DateTimeKind.Unspecified); // System.ArgumentException: Value should not be Unspecified. (Parameter 'person: p => p.DateOfBirth')
person.Error().IfDateTimeKindNot(p => p.DateOfBirth, DateTimeKind.Local); // System.ArgumentException: Value should be Local. (Parameter 'person: p => p.DateOfBirth')
person.Error().IfGreaterThan(p => p.DateOfBirth, DateTime.Now.AddYears(-20)); // System.ArgumentOutOfRangeException: Value should not be greater than 2/28/2002 4:41:19 PM. (Parameter 'person: p => p.DateOfBirth')
person.Error().IfLessThan(p => p.DateOfBirth, DateTime.Now.AddYears(20)); // System.ArgumentOutOfRangeException: Value should not be less than 2/28/2042 4:41:46 PM. (Parameter 'person: p => p.DateOfBirth')
person.Error().IfEquals(p => p.DateOfBirth, other); // System.ArgumentException: Value should not be equal to 2/28/2022 4:45:12 PM. (Parameter 'person: p => p.DateOfBirth')
```

### Enum properties

```csharp
person.Error().IfOutOfRange(p => p.EmployeeType); // System.ArgumentOutOfRangeException: Value should be defined in enum. (Parameter 'person: p => p.EmployeeType')
person.Error().IfEquals(p => p.EmployeeType, EmployeeType.FullTime); // System.ArgumentException: Value should not be equal to FullTime. (Parameter 'person: p => p.EmployeeType')
```

### property equalities

```csharp
person.Error().IfDefault(p => p.DateOfBirth); // System.ArgumentException: Value should not be default. (Parameter 'person: p => p.DateOfBirth')
person.Error().IfNotDefault(p => p.DateOfBirth); // System.ArgumentException: Value should be default. (Parameter 'person: p => p.DateOfBirth')
person.Error().IfNull(p => p.MiddleName); // System.ArgumentNullException: Value cannot be null. (Parameter 'person: p => p.MiddleName')
person.Error().IfNotNull(p => p.MiddleName); // System.ArgumentException: Value should be null. (Parameter 'person: p => p.MiddleName')
person.Error().IfEquals(p => p.Age, 5); // System.ArgumentException: Value should not be not be equal to 5. (Parameter 'person: p => p.Age')
person.Error().IfNotEquals(p => p.Age, 3); // System.ArgumentException: Value should be equal to 3. (Parameter 'person: p => p.Age')
```

### Uri properties

```csharp
person.Error().IfHttps(p => p.Website); // System.ArgumentException: Uri scheme should not be https. (Parameter 'person: p => p.Website')
person.Error().IfNotHttps(p => p.Website); // System.ArgumentException: Uri scheme should be https. (Parameter 'person: p => p.Website')
person.Error().IfHttp(p => p.Website); // System.ArgumentException: Uri scheme should not be http. (Parameter 'person: p => p.Website')
person.Error().IfNotHttp(p => p.Website); // System.ArgumentException: Uri scheme should be http. (Parameter 'person: p => p.Website')
person.Error().IfScheme(p => p.Website, Uri.UriSchemeHttp); // System.ArgumentException: Uri scheme should not be http. (Parameter 'person: p => p.Website')
person.Error().IfSchemeNot(p => p.Website, Uri.UriSchemeFtp); // System.ArgumentException: Uri scheme should be ftp. (Parameter 'person: p => p.Website')
person.Error().IfPort(p => p.Website, 800); // System.ArgumentException: Uri port should not be 80. (Parameter 'person: p => p.Website')
person.Error().IfPortNot(p => p.Website, 8080); // System.ArgumentException: Uri port should be 8080. (Parameter 'person: p => p.Website')
person.Error().IfAbsolute(p => p.Website); // System.ArgumentException: Uri should be relative. (Parameter 'person: p => p.Website')
person.Error().IfRelative(p => p.Website); // System.ArgumentException: Uri should be absolute. (Parameter 'person: p => p.Website')
person.Error().IfNotAbsolute(p => p.Website); // System.ArgumentException: Uri should be absolute. (Parameter 'person: p => p.Website')
person.Error().IfNotRelative(p => p.Website); // System.ArgumentException: Uri should be relative. (Parameter 'person: p => p.Website')
```

### Comparable properties

```csharp
person.Error().IfPositive(p => p.Age); // System.ArgumentOutOfRangeException: Value should not be greater than 0. (Parameter 'person: p => p.Age')\n Actual value was 5.
person.Error().IfNegative(p => p.Age); // System.ArgumentOutOfRangeException: Value should not be less than 0. (Parameter 'person: p => p.Age')\n Actual value was -5.
person.Error().IfLessThan(p => p.Age, 10); // System.ArgumentOutOfRangeException: Value should not be less than 10. (Parameter 'person: p => p.Age')\n Actual value was 5.
person.Error().IfGreaterThan(p => p.Age, 3); // System.ArgumentOutOfRangeException: Value should not be greater than 3. (Parameter 'person: p => p.Age')\n Actual value was 5.
person.Error().IfOutOfRange(p => p.Age, 0, 5); // System.ArgumentOutOfRangeException: Value should be between 0 and 5. (Parameter 'person: p => p.Age')\n Actual value was -5.
```


# License

This project is licensed under the terms of the [MIT](https://github.com/Andersoft/Error/blob/main/LICENSE) license.
