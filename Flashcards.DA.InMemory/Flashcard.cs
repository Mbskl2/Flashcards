using System.Collections.Generic;
using System.Linq;
using FlashCards.DataAccess.Models;

namespace Flashcards.DA.InMemory
{
    class Flashcard : IFlashcard
    {
        public string Word { get; }
        public string Translation { get; }
        public IList<FlashCards.IUseCase> UseCases { get; }

        public Flashcard(string word, string translation, IList<IUseCase> useCases)
        {
            Word = word;
            Translation = translation;
            UseCases = useCases.Select(s => (FlashCards.IUseCase)s).ToList();
        }
    }
}