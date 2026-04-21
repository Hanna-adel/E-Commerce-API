
using FluentValidation.Results;
using Project.Common;

namespace Project.BLL
{
    public interface IErrorMapper
    {
        Dictionary<string, List<Error>> MapError(ValidationResult validationResult);
    }
}
