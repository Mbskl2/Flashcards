namespace FlashCards.Questions
{
    class QuestionWithAnswer : IQuestion
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