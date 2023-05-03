using FluentValidation.Results;

namespace Application.Dto
{
    public class GenericResponseDto
    {
        public bool Succeeded { get; set; } = false;
        public bool IsValid { get; set; } = false;
        public List<ValidationFailure> Errors { get; set; } = new();

        public object? Data { get; set; } = null;
    }
}
