using System.Collections.Generic;

namespace FlashCards.DataAccess
{
    public interface IFlashcardService
    {
        IList<IUseCase> Get(int i);
    }
}