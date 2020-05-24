namespace FlashCards.Answers
{
    public class ValidationResult
    {
        public bool IsCorrect => CorrectAnswer == null;
        public string? CorrectAnswer { get; }
        private ValidationResult(string? correctAnswer = null)
        {
            CorrectAnswer = correctAnswer;
        }

        public static ValidationResult Correct() => new ValidationResult();
        public static ValidationResult Failed(string correctAnswer) => new ValidationResult(correctAnswer);
    }
}