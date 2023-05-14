using FluentValidation.Results;

namespace Application.Dto;

public class ResponseDto<TData>
    where TData : class?
{
    public bool Succeeded { get; set; }
    public List<ValidationFailure> Errors { get; set; }
    public TData? Data { get; set; }

    internal ResponseDto(bool succeeded, List<ValidationFailure> errors, TData? data)
    {
        Succeeded = succeeded;
        Errors = errors;
        Data = data;
    }

    public static ResponseDto<TData> Success(TData data)
    {
        return new ResponseDto<TData>(true, new List<ValidationFailure>(), data);
    }

    public static ResponseDto<TData?> Failure(List<ValidationFailure> errors)
    {
        return new ResponseDto<TData?>(false, errors, null);
    }

    public static ResponseDto<TData?> Failure(ValidationFailure error)
    {
        return new ResponseDto<TData?>(false, new List<ValidationFailure>() {error}, null);
    }

    public static ResponseDto<TData?> Failure()
    {
        return new ResponseDto<TData?>(false, new List<ValidationFailure>() { }, null);
    }
}