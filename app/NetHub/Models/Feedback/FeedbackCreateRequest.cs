using FluentValidation;

namespace NetHub.Models.Feedback;

public class FeedbackCreateRequest
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Message { get; set; } = default!;
}

public class FeedbackCreateValidator : AbstractValidator<FeedbackCreateRequest>
{
    public FeedbackCreateValidator()
    {
        RuleFor(r => r.Name).NotNull().NotEmpty();
        RuleFor(r => r.Email).NotNull().NotEmpty();
        RuleFor(r => r.Message).NotNull().NotEmpty();
    }
}