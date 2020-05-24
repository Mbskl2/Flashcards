using System;
using FlashCards.DataAccess;

namespace FlashCards.Answers
{
    class AnswerValidator : IAnswerValidator
    {
        public ValidationResult Validate(IUseCase useCase, string userAnswer)
        {
            var t = useCase as DataAccess.ITranslatable;
            return Validate(t, userAnswer);
        }


        public ValidationResult Validate(IFlashcard flashcard, string userAnswer)
        {
            var t = flashcard as DataAccess.ITranslatable;
            return Validate(t, userAnswer);
        }

        private static ValidationResult Validate(ITranslatable t, string userAnswer)
        {
            if (t is null)
                throw new WrongTypeException();
            if (t.Translation.Equals(userAnswer, StringComparison.InvariantCulture))
                return ValidationResult.Correct();
            return ValidationResult.Failed(t.Translation);
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