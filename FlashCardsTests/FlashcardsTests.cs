using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FlashCards;
using FlashCards.Answers;
using FlashCards.DataAccess;
using FlashCardsTests.TestHelpers;
using FluentAssertions;
using Moq;

namespace FlashCardsTests
{
    [TestClass]
    public class FlashcardsServiceTests
    {
        private Mock<IFlashcardService> service;
        private IList<IUseCase> questions;
        private FlashcardsService _flashcardsService;

        [TestInitialize]
        public void Setup()
        {
            service = new Mock<IFlashcardService>();
            questions = new List<IUseCase> { new QuestionWithAnswer("question", "answer") };
            service.Setup(s => s.Get(It.IsAny<int>())).Returns(questions);

            var validator = new Mock<IAnswerValidator>();
            _flashcardsService = new FlashcardsService(service.Object, validator.Object);
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
        public void ReturnsFalse_When_NoNext()
        {
            // Given
            _flashcardsService.Load(7);
            questions.RemoveAt(0);

            // Then
            _flashcardsService.MoveNext().Should().BeFalse();
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
            public ValidationResult Validate(IUseCase question, string userAnswer)
            {
                return ValidationResult.Failed("Wrong answer !!! 123");
            }
        }

        private class AlwaysCorrectValidator : IAnswerValidator
        {
            public ValidationResult Validate(IUseCase question, string userAnswer)
            {
                return ValidationResult.Correct();
            }
        }
    }
}