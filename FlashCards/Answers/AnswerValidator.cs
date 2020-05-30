using System;
using FlashCards.DataAccess;

namespace FlashCards.Answers
{
    class AnswerValidator : IAnswerValidator
    {
        public ValidationResult Validate(IUseCase useCase, string userAnswer)
        {
            return ValidateAnswer(useCase, userAnswer);
        }


        public ValidationResult Validate(IFlashcard flashcard, string userAnswer)
        {
            return ValidateAnswer(flashcard, userAnswer);
        }

        private static ValidationResult ValidateAnswer(dynamic toValidate, string userAnswer)
        {
            if (toValidate is ITranslatable t)
            {
                if (t.Translation.Equals(userAnswer, StringComparison.InvariantCulture))
                    return ValidationResult.Correct();
                return ValidationResult.Failed(t.Translation);
            }
            throw new WrongTypeException();
        }

        public class WrongTypeException : Exception
        {
            public WrongTypeException()
            : base("The IQuestion for validation has to be the same object that you got from QuestionProvider")
            {
            }
        }
    }
}