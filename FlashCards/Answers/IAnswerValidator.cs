namespace FlashCards.Answers
{
    public interface IAnswerValidator
    {
        ValidationResult Validate(IUseCase question, string userAnswer);
    }
}