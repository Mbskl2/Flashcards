using System.Collections.Generic;

namespace FlashCards
{
    public interface IFlashcard
    {
        string Word { get; }
        IList<IUseCase> UseCases { get; }
    }
}