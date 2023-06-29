using dotnet_rpg.Application.Exceptions;
using FluentAssertions;
using FluentValidation.Results;

namespace dotnet_rpg.Application.UnitTests.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        var actual = new ValidationException(Array.Empty<ValidationFailure>()).Errors;

        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Fact]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var failures = new List<ValidationFailure>
        {
            new("Name", "must be between 1 and 50 characters"),
        };

        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "Name" });
        actual["Name"].Should().BeEquivalentTo(new string[] { "must be between 1 and 50 characters" });
    }

    [Fact]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        var failures = new List<ValidationFailure>
        {
            new("Name", "must be between 1 and 50 characters"),
            new("Name", "must not be empty"),
            new("Hit Points", "must be greater than '0'"),
            new("Hit Points", "must not be empty"),
        };

        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "Name", "Hit Points" });

        actual["Name"].Should().BeEquivalentTo(new string[]
        {
            "must be between 1 and 50 characters",
            "must not be empty",
        });

        actual["Hit Points"].Should().BeEquivalentTo(new string[]
        {
            "must be greater than '0'",
            "must not be empty",
        });
    }
}