using System.Collections.Generic;
using FlashCards;
using FlashCards.DataAccess;

namespace Flashcards.DA.InMemory
{
    public class InMemoryFlashcardService : IFlashcardService
    {
        public IList<IUseCase> Get(int i)
        {
            return new List<IUseCase>
            {

            };
        }
    }
}