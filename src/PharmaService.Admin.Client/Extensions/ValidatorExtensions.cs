using FluentValidation;

namespace PharmaService.Admin.Client.Extensions;

public static class ValidatorExtensions
{
    public static Func<object, string, Task<IEnumerable<string>>> ValidateValue<T>(this AbstractValidator<T> validator) =>
        async (model, propertyName) =>
        {
            var result = await validator.ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));

            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };

    public static Func<object, string, Task<IEnumerable<string>>> ValidateInnerCollectionValue<T>(this AbstractValidator<T> validator,
        string collectionName, string propertyName) =>
        async (model, _) =>
        {
            var result = await validator.ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(collectionName)));

            return result.IsValid
                ? Array.Empty<string>()
                : result.Errors.Where(e => e.PropertyName == propertyName).Select(e => e.ErrorMessage);
        };
}
