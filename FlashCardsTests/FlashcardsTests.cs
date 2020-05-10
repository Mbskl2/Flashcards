using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Flashcards.Answers;
using Flashcards.DataAccess;
using Flashcards.Questions;
using FluentAssertions;
using Moq;

namespace FlashCardsTests
{
    [TestClass]
    public class FlashcardsTests
    {
        private Mock<IFlashcardService> service;
        private IList<IQuestion> questions;
        private Flashcards.Flashcards flashcards;

        [TestInitialize]
        public void Setup()
        {
            service = new Mock<IFlashcardService>();
            questions = new List<IQuestion> { new QuestionWithAnswer("question", "answer") };
            service.Setup(s => s.Get(It.IsAny<int>())).Returns(questions);

            var validator = new Mock<IAnswerValidator>();
            flashcards = new Flashcards.Flashcards(service.Object, validator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(Flashcards.Flashcards.FlashcardsNotLoaded))]
        public void Throw_When_AnswersNotLoadedQuestion()
        {
            flashcards.AnswerCurrentQuestion("Answer");
        }

        [TestMethod]
        public void CurrentIsNotNull_After_LoadedFlashcards()
        {
            flashcards.Load(10);
            flashcards.Current.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_When_NumberToLoadIsLessThan0()
        {
            flashcards.Load(0);
        }

        [TestMethod]
        public void ReturnsFalse_When_NoNext()
        {
            // Given
            flashcards.Load(7);
            questions.RemoveAt(0);

            // Then
            flashcards.MoveNext().Should().BeFalse();
        }

        [TestMethod]
        public void ReturnTrue_When_MoreLeft()
        {
            flashcards.Load(3);
            flashcards.MoveNext().Should().BeTrue();
        }

        [TestMethod]
        public void CanAnswerWrong_Indefinitely()
        {
            // Given
            SetupValidator(new AlwaysFailedValidator());
            flashcards.Load(13);
            
            // When
            for (int i = 0; i < 10; i++)
            {
                flashcards.AnswerCurrentQuestion("Wrong answer!!");
                flashcards.MoveNext();
            }

            // Then
            flashcards.MoveNext().Should().BeTrue();
        }

        [TestMethod]
        public void RemoveQuestion_When_AnswerCorrectly()
        {
            // Given
            SetupValidator(new AlwaysCorrectValidator());
            flashcards.Load(100);

            // When
            flashcards.AnswerCurrentQuestion("Correct answer!!");

            // Then
            flashcards.MoveNext().Should().BeFalse();
        }

        private void SetupValidator(IAnswerValidator validator)
        {
            this.flashcards = new Flashcards.Flashcards(service.Object, validator);
        }

        private class AlwaysFailedValidator : IAnswerValidator
        {
            public ValidationResult Validate(IQuestion question, string userAnswer)
            {
                return ValidationResult.Failed("Wrong answer !!! 123");
            }
        }

        private class AlwaysCorrectValidator : IAnswerValidator
        {
            public ValidationResult Validate(IQuestion question, string userAnswer)
            {
                return ValidationResult.Correct();
            }
        }
    }
}