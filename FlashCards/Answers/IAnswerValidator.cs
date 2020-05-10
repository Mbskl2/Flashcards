using Flashcards.Questions;

namespace Flashcards.Answers
{
    public interface IAnswerValidator
    {
        ValidationResult Validate(IQuestion question, string userAnswer);
    }
}