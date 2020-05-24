using System;

namespace FlashCards.Answers
{
    class AnswerValidator : IAnswerValidator
    {
        public ValidationResult Validate(IUseCase question, string userAnswer)
        {
            var qa = question as DataAccess.Models.IUseCase;
            if (qa is null)
                throw new WrongTypeException();
            if (qa.Translation.Equals(userAnswer, StringComparison.InvariantCulture))
                return ValidationResult.Correct();
            return ValidationResult.Failed(qa.Translation);
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