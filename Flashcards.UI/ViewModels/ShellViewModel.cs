using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Caliburn.Micro;
using FlashCards;

namespace Flashcards.UI.ViewModels
{
    class ShellViewModel : Screen
    {
        private string resultText = "";
        private readonly FlashcardsService flashcardsService;
        public IFlashcard? Flashcard { get; private set; }
        public string Answer { get; set; } = "";
        public ICommand EnterCommand { get; }; //TODO: Zrobić

        public string ResultText
        {
            get => resultText;
            private set
            {
                resultText = value;
                NotifyOfPropertyChange(() => ResultText);
            }
        }

        public ShellViewModel(FlashcardsService flashcardsService)
        {
            this.flashcardsService = flashcardsService;
            flashcardsService.Load(100);
            Flashcard = flashcardsService.Current;
        }

        public void Enter(string answer)
        {
            var results = flashcardsService.AnswerCurrentFlashcard(answer);
            if (results[0].IsCorrect) //TODO: Add handling of many results
                ResultText = "Correct!";
            else
                ResultText = $"Wrong! Correct answer: {results[0].CorrectAnswer} ";
        }

        private class EnterAnswerCommand : ICommand
        {
            public bool CanExecute(object parameter)
            {
                throw new NotImplementedException();
            }

            public void Execute(object parameter)
            {
                throw new NotImplementedException();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}
