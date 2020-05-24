using FlashCards.DataAccess.Models;

namespace Flashcards.DA.InMemory
{
    class UseCase : IUseCase
    {
        public string Sentence { get; }
        public string Translation { get; }

        public UseCase(string sentence, string translation)
        {
            Sentence = sentence;
            Translation = translation;
        }
    }
}