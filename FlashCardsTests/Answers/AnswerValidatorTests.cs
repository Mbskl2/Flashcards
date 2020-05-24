using System.Collections.Generic;
using FlashCards.Answers;
using FlashCards.DataAccess.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlashCardsTests.Answers
{
    [TestClass]
    public class AnswerValidatorTests
    {
        private IAnswerValidator validator;
        private const string question1 = "UseCase one one two?";
        private const string answer1 = "Answer1 one one two.";

        [TestInitialize]
        public void Setup()
        {
            validator = new AnswerValidator();
        }

        [TestMethod]
        [ExpectedException(typeof(AnswerValidator.WrongTypeException))]
        public void UseCase_Throws_When_GivenWrongType()
        {
            validator.Validate(new UseCase(), answer1);
        }

        [TestMethod]
        public void UseCase_ReturnCorrectResult_When_AnswerIsCorrect()
        {
            dynamic useCase = new UseCaseWithAnswer(question1, answer1);
            ValidateCorrectResultReturned(useCase);
        }

        [TestMethod]
        public void UseCase_ReturnIncorrectResult_When_AnswerIsFalse()
        {
            dynamic useCase = new UseCaseWithAnswer(question1, answer1);
            ValidateFailedResultReturned(useCase, "Wrong answer.");
        }

        [TestMethod]
        public void UseCase_ReturnIncorrectResult_When_AnswerHasWrongCase()
        {
            var useCase = new UseCaseWithAnswer(question1, answer1);
            ValidateFailedResultReturned(useCase, answer1.ToLower());
        }

        [TestMethod]
        [ExpectedException(typeof(AnswerValidator.WrongTypeException))]
        public void Flashcard_Throws_When_GivenWrongType()
        {
            validator.Validate(new Flashcard(), answer1);
        }

        [TestMethod]
        public void Flashcard_ReturnCorrectResult_When_AnswerIsCorrect()
        {
            dynamic flashcard = new FlashcardWithAnswer(question1, answer1);
            ValidateCorrectResultReturned(flashcard);
        }

        [TestMethod]
        public void Flashcard_ReturnIncorrectResult_When_AnswerIsFalse()
        {
            dynamic flashcard = new FlashcardWithAnswer(question1, answer1);
            ValidateFailedResultReturned(flashcard, "Wrong answer.");
        }

        [TestMethod]
        public void Flashcard_ReturnIncorrectResult_When_AnswerHasWrongCase()
        {
            dynamic flashcard = new FlashcardWithAnswer(question1, answer1);
            ValidateFailedResultReturned(flashcard, answer1.ToLower());
        }

        private void ValidateCorrectResultReturned(dynamic useCase)
        {
            var result = validator.Validate(useCase, answer1);
            Assert.IsTrue(result.IsCorrect);
        }

        private void ValidateFailedResultReturned(dynamic useCase, string answer)
        {
            var result = validator.Validate(useCase, answer);
            Assert.IsFalse(result.IsCorrect);
            Assert.AreEqual(answer1, result.CorrectAnswer);
        }

        #region Helper Objects

        class UseCase : FlashCards.IUseCase
        {
            public string Sentence { get; }
        }

        class UseCaseWithAnswer : IUseCase
        {
            public string Sentence { get; }
            public string Translation { get; }

            public UseCaseWithAnswer(string s, string t)
            {
                Sentence = s;
                Translation = t;
            }
        }

        class Flashcard : FlashCards.IFlashcard
        {
            public string Word { get; }
        }

        class FlashcardWithAnswer : IFlashcard
        {
            public string Word { get; }
            public string Translation { get; }
            public IList<IUseCase> UseCases { get; }

            public FlashcardWithAnswer(string w, string t)
            {
                Word = w;
                Translation = t;
            }
        }

        #endregion
    }
}