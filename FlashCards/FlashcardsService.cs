using System;
using System.Collections.Generic;
using System.Linq;
using FlashCards.Answers;
using FlashCards.DataAccess;

namespace FlashCards
{
    public class FlashcardsService
    {
        public int NumberOfFlashcards { get; }

        private readonly IFlashcardService service;
        public IFlashcard? Current { get; private set; }
        private IList<IFlashcard> flashcards = new List<IFlashcard>();
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
            flashcards = service.Get(numberOfFlashcards).Select(f => (IFlashcard) f).ToList();
            Current = flashcards.First();
        }

        public ValidationResult AnswerCurrentQuestion(string answer)
        {
            if (Current is null)
                throw new FlashcardsNotLoaded();
            var result = validator.Validate(Current, answer);
            if (result.IsCorrect)
                flashcards.Remove(Current);
            return result;
        }

        public bool MoveNext()
        {
            if (flashcards.Count == 0)
                return false;
            Current = flashcards[random.Next(flashcards.Count)];
            return true;
        }

        public class FlashcardsNotLoaded : Exception
        { 
        }
    }
}