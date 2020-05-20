using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace Flashcards.UI.ViewModels
{
    class ShellViewModel : Screen
    {
        private readonly FlashcardsService _flashcardsService;
        public string Question { get; private set; } = "";
        public string Answer { get; set; } = "";
        public string ResultText { get; private set; } = "";

        public ShellViewModel(FlashcardsService flashcardsService)
        {
            this._flashcardsService = flashcardsService;
            flashcardsService.Load(100);
            if (flashcardsService.Current != null)
                Question = flashcardsService.Current.Text;
        }

        public void Enter(string answer)
        {
            var result = _flashcardsService.AnswerCurrentQuestion(answer);
            if (result.IsCorrect)
                ResultText = "Correct!";
            else
                ResultText = $"Wrong! The answer should be {result.CorrectAnswer} ";
        }
    }
}
