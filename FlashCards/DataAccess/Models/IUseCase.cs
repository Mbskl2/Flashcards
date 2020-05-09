namespace FlashCards.DataAccess.Models
{
    public interface IUseCase
    {
        string Sentence { get; }
        string Translation { get; }
    }
}