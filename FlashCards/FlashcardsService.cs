using System;
using System.Collections.Generic;
using System.Linq;
using FlashCards.Answers;
using FlashCards.DataAccess;

namespace FlashCards
{
    public class FlashcardsService // TODO: Cleverer name?
    {
        public int NumberOfFlashcards { get; }

        private readonly IFlashcardService service;
        public IUseCase? Current { get; private set; }
        private IList<IUseCase> questions = new List<IUseCase>();
        private readonly IAnswerValidator validator;
        private readonly Random random = new Random();

        public FlashcardsService(IFlashcardService service, IAnswerValidator validator)
        {
            this.service = service;
            this.validator = validator;
        }

        public void Load(int numberOfFlashcards)
        {
            if (numberOfFlashcards <= 0)
                throw new ArgumentException($"{numberOfFlashcards} cannot be less than 1.");
            questions = service.Get(numberOfFlashcards);
            Current = questions.First();
        }

        public ValidationResult AnswerCurrentQuestion(string answer)
        {
            if (Current is null)
                throw new FlashcardsNotLoaded();
            var result = validator.Validate(Current, answer);
            if (result.IsCorrect)
                questions.Remove(Current);
            return result;
        }

        public bool MoveNext()
        {
            if (questions.Count == 0)
                return false;
            Current = questions[random.Next(questions.Count)];
            return true;
        }

        public class FlashcardsNotLoaded : Exception
        { 
        }
    }
}