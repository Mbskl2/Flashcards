using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FlashCards;
using FlashCards.Answers;
using FlashCards.DataAccess;
using FluentAssertions;
using Moq;
using IDAFlashcard = FlashCards.DataAccess.Models.IFlashcard;

namespace FlashCardsTests
{
    [TestClass]
    public class FlashcardsServiceTests
    {
        private Mock<IFlashcardService> service;
        private IList<IDAFlashcard> flashcards;
        private FlashcardsService _flashcardsService;

        [TestInitialize]
        public void Setup()
        {
            service = new Mock<IFlashcardService>();
            flashcards = CreateFlashcards();
            service.Setup(s => s.Get(It.IsAny<int>())).Returns(flashcards);

            var validator = new Mock<IAnswerValidator>();
            _flashcardsService = new FlashcardsService(service.Object, validator.Object);
        }

        private static List<IDAFlashcard> CreateFlashcards()
        {
            return new List<IDAFlashcard> { new Flashcard() };
        }

        [TestMethod]
        [ExpectedException(typeof(FlashcardsService.FlashcardsNotLoaded))]
        public void Throw_When_AnswersNotLoadedQuestion()
        {
            _flashcardsService.AnswerCurrentQuestion("Answer");
        }

        [TestMethod]
        public void CurrentIsNotNull_After_LoadedFlashcards()
        {
            _flashcardsService.Load(10);
            _flashcardsService.Current.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_When_NumberToLoadIsLessThan0()
        {
            _flashcardsService.Load(0);
        }

        [TestMethod]
        public void ReturnTrue_When_MoreLeft()
        {
            _flashcardsService.Load(3);
            _flashcardsService.MoveNext().Should().BeTrue();
        }

        [TestMethod]
        public void CanAnswerWrong_Indefinitely()
        {
            // Given
            SetupValidator(new AlwaysFailedValidator());
            _flashcardsService.Load(13);
            
            // When
            for (int i = 0; i < 10; i++)
            {
                _flashcardsService.AnswerCurrentQuestion("Wrong answer!!");
                _flashcardsService.MoveNext();
            }

            // Then
            _flashcardsService.MoveNext().Should().BeTrue();
        }

        [TestMethod]
        public void RemoveQuestion_When_AnswerCorrectly()
        {
            // Given
            SetupValidator(new AlwaysCorrectValidator());
            _flashcardsService.Load(100);

            // When
            _flashcardsService.AnswerCurrentQuestion("Correct answer!!");

            // Then
            _flashcardsService.MoveNext().Should().BeFalse();
        }

        private void SetupValidator(IAnswerValidator validator)
        {
            this._flashcardsService = new FlashcardsService(service.Object, validator);
        }

        private class AlwaysFailedValidator : IAnswerValidator
        {
            public ValidationResult Validate(IUseCase useCase, string userAnswer)
            {
                return ValidationResult.Failed("Wrong answer !!! 123");
            }

            public ValidationResult Validate(IFlashcard flashcard, string userAnswer)
            {
                return ValidationResult.Failed("Wrong answer !!! 123");
            }
        }

        private class AlwaysCorrectValidator : IAnswerValidator
        {
            public ValidationResult Validate(IUseCase useCase, string userAnswer)
            {
                return ValidationResult.Correct();
            }

            public ValidationResult Validate(IFlashcard flashcard, string userAnswer)
            {
                return ValidationResult.Correct();
            }
        }

        private class Flashcard : IDAFlashcard
        {
            public string Translation { get; }
            public string Word { get; }
            public IList<FlashCards.DataAccess.Models.IUseCase> UseCases { get; }
        }
    }
}