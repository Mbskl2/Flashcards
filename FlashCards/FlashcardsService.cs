using System;
using System.Collections.Generic;
using System.Linq;
using FlashCards.Answers;
using FlashCards.DataAccess;

namespace FlashCards
{
    public class FlashcardsService
    {
        public int NumberOfFlashcards { get; } //TODO: Użyć?

        private readonly IFlashcardService service;
        public IFlashcard? Current { get; private set; }
        private IList<IFlashcard> flashcards = new List<IFlashcard>();
        private readonly IAnswerValidator validator;
        private readonly Random random = new Random();
        private const string WRONG_ANSWER = "!@#$%%^&*(){}|:?><PKNJOV3452:";

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

        public IList<ValidationResult> AnswerCurrentFlashcard(string answer, params string[] useCaseAnswers)
        {
            if (Current is null)
                throw new FlashcardsNotLoaded();
            var allResults = new List<ValidationResult> {validator.Validate(Current, answer)};
            allResults.AddRange(ValidateUseCaseAnswers(Current, useCaseAnswers));

            if (allResults.All(r => r.IsCorrect))
                flashcards.Remove(Current);
            return allResults;
        }

        private List<ValidationResult> ValidateUseCaseAnswers(IFlashcard current, string[] useCaseAnswers)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (current.UseCases == null)
                return results;

            for (int i = 0; i < current.UseCases.Count; i++)
            {
                var useCase = current.UseCases[i];
                if (useCaseAnswers.Length > i)
                    results.Add(validator.Validate(useCase, useCaseAnswers[i]));
                else
                    results.Add(validator.Validate(useCase, WRONG_ANSWER));
            }
            return results;
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