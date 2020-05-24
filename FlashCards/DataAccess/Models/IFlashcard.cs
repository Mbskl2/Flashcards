using System.Collections.Generic;

namespace FlashCards.DataAccess.Models
{
    public interface IFlashcard : FlashCards.IFlashcard, ITranslatable
    {
        IList<IUseCase> UseCases { get; }
    }
}