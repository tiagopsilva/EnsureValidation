using System.Reflection;

namespace EnsureValidation.Tests;

public class MessagesConsistencyTests
{
    public static IEnumerable<object[]> ValidatorMethodNames()
    {
        var validatorTypes = Assembly.GetAssembly(typeof(IntValidator))!
            .GetTypes()
            .Where(t => t.IsClass && t.Namespace == "EnsureValidation.Validators" && t.Name.EndsWith("Validator"))
            .ToArray();

        foreach (var type in validatorTypes)
        {
            // Gets public instance methods declared directly on the class (not inherited)
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                // Only consider methods that return the validator type itself (fluent pattern)
                if (method.ReturnType == type)
                    yield return new object[] { type.Name, method.Name };
            }
        }
    }

    [Theory]
    [MemberData(nameof(ValidatorMethodNames))]
    public void AllValidatorMethods_ShouldHaveDefaultMessage(string validatorName, string methodName)
    {
        // Gets the default message dictionary via reflection
        var messagesField = typeof(Messages).GetField("DefaultMessages", BindingFlags.NonPublic | BindingFlags.Static);

        var messages = messagesField?.GetValue(null) as IDictionary<string, string>;
        Assert.NotNull(messages);

        // The expected key is the method name
        Assert.True(messages!.ContainsKey(methodName),
            $"Method '{validatorName}.{methodName}' has no default message registered in Messages.cs");
    }
}