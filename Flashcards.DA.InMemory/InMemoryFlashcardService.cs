using System.Collections.Generic;
using Flashcards.DataAccess;
using Flashcards.Questions;

namespace Flashcards.DA.InMemory
{
    public class InMemoryFlashcardService : IFlashcardService
    {
        public IList<IQuestion> Get(int i)
        {
            return new List<IQuestion>
            {

            };
        }
    }
}