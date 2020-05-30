using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using FlashCards;
using FlashCards.Answers;
using Flashcards.DA.InMemory;
using FlashCards.DataAccess;
using Flashcards.UI.ViewModels;

namespace Flashcards.UI
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        #region IoC
        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void Configure()
        {
            container.Singleton<IEventAggregator, EventAggregator>();
            container.Singleton<IWindowManager, WindowManager>();
            container.PerRequest<IFlashcardService, InMemoryFlashcardService>();
            container.PerRequest<IAnswerValidator, AnswerValidator>();
            container.PerRequest<FlashcardsService>();
            container.PerRequest<ShellViewModel>();
        }
        #endregion
    }
}
