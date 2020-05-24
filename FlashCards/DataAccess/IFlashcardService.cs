using System.Collections.Generic;

namespace FlashCards.DataAccess
{
    public interface IFlashcardService
    {
        IList<Models.IFlashcard> Get(int i);
    }
}