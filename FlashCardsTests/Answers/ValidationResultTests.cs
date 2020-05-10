using Flashcards.Answers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace FlashCardsTests.Answers
{
    [TestClass]
    public class ValidationResultTests
    {
        [TestMethod]
        public void NotCorrectWithMessage_When_CreatedFailed()
        {
            string message = "Correct answer";
            var result = ValidationResult.Failed(message);

            result.IsCorrect.Should().BeFalse();
            result.CorrectAnswer.Should().Be(message);
        }

        [TestMethod]
        public void CorrectWithoutMessage_When_CreatedCorrect()
        {
            var result = ValidationResult.Correct();

            result.IsCorrect.Should().BeTrue();
            result.CorrectAnswer.Should().BeNull();
        }
    }
}