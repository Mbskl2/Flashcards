namespace Flashcards.Questions
{
    class QuestionWithAnswer : IQuestion // TODO: Shouldn't this be implemented in DataAccess module?
    {
        public string Text { get; }
        public string Answer { get; }
        public QuestionWithAnswer(string text, string answer)
        {
            Text = text;
            Answer = answer;
        }
    }
}