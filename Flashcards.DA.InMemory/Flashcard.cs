using System.Collections.Generic;
using FlashCards.DataAccess.Models;

namespace Flashcards.DA.InMemory
{
    class Flashcard : IFlashcard
    {
        public string Word { get; }
        public string Translation { get; }
        public IList<IUseCase> UseCases { get; }

        public Flashcard(string word, string translation, IList<IUseCase> useCases)
        {
            Word = word;
            Translation = translation;
            UseCases = useCases;
        }
    }
}