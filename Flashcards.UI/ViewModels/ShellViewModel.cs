using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace Flashcards.UI.ViewModels
{
    class ShellViewModel : Screen
    {
        public string Question { get; set; } = "Co to za pytanie?";
        public string Answer {get; set; } = "Odpowiedź";

        public void Enter(string answer)
        {

        }
    }
}
