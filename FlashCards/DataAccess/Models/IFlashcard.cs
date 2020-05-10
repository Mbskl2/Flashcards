using System.Collections;
using System.Collections.Generic;

namespace Flashcards.DataAccess.Models
{
    public interface IFlashcard
    {
        string Word { get; }
        string Translation { get; }
        IList<IUseCase> UseCases { get; }
    }
}