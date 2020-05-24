using FlashCards.DataAccess.Models;

namespace FlashCardsTests.TestHelpers
{
    class QuestionWithAnswer : IUseCase, FlashCards.IUseCase
    {
        public string Sentence { get; }
        public string Translation { get; }

        public QuestionWithAnswer(string s, string t)
        {
            Sentence = s;
            Translation = t;
        }
    }
}