using System.Collections.Generic;
using Flashcards.Questions;

namespace Flashcards.DataAccess
{
    public interface IFlashcardService
    {
        IList<IQuestion> Get(int i);
    }
}