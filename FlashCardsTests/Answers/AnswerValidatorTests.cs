using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlashCards.Answers;
using System;
using System.Collections.Generic;
using System.Text;
using FlashCards.Questions;
using FluentAssertions;

namespace FlashCardsTests.Answers
{
    [TestClass]
    public class AnswerValidatorTests
    {
        private AnswerValidator validator;
        private const string question1 = "Question one one two?";
        private const string answer1 = "Answer1 one one two.";

        [TestInitialize]
        public void Setup()
        {
            validator = new AnswerValidator();
        }

        [TestMethod]
        [ExpectedException(typeof(AnswerValidator.WrongTypeException))]
        public void Throws_When_GivenNotQuestionWithAnswer()
        {
            validator.Validate(new Question(), answer1);
        }

        [TestMethod]
        public void ReturnCorrectResult_When_AnswerIsCorrect()
        {
            var question = new QuestionWithAnswer(question1, answer1);
            var result = validator.Validate(question, answer1);
            result.IsCorrect.Should().BeTrue();
        }

        [TestMethod]
        public void ReturnIncorrectResult_When_AnswerIsFalse()
        {
            var question = new QuestionWithAnswer(question1, answer1);
            var result = validator.Validate(question, "Wrong answer.");
            result.IsCorrect.Should().BeFalse();
            result.CorrectAnswer.Should().Be(answer1);
        }

        [TestMethod]
        public void ReturnIncorrectResult_When_AnswerHasWrongCase()
        {
            var question = new QuestionWithAnswer(question1, answer1);
            var result = validator.Validate(question, answer1.ToLower());
            result.IsCorrect.Should().BeFalse();
            result.CorrectAnswer.Should().Be(answer1);
        }

        private class Question : IQuestion
        {
            public string Text { get; }
        }
    }
}