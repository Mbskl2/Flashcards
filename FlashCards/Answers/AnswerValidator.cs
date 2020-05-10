using System;
using Flashcards.Questions;

namespace Flashcards.Answers
{
    class AnswerValidator : IAnswerValidator
    {
        public ValidationResult Validate(IQuestion question, string userAnswer)
        {
            var qa = question as QuestionWithAnswer;
            if (qa is null)
                throw new WrongTypeException();
            if (qa.Answer.Equals(userAnswer, StringComparison.InvariantCulture))
                return ValidationResult.Correct();
            return ValidationResult.Failed(qa.Answer);
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