namespace FlashCards.Answers
{
    public interface IAnswerValidator
    {
        ValidationResult Validate(IUseCase useCase, string userAnswer);
        ValidationResult Validate(IFlashcard flashcard, string userAnswer);
    }
}