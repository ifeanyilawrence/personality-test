namespace PersonalityTest.Core.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoFixture;
    using Data;
    using FluentAssertions;
    using Microsoft.Extensions.Options;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Shared.Config;
    using Shared.Model;
    using Shared.Parameters;
    
    [TestClass]
    public class QuestionServiceTests
    {
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<IOptions<QuestionConfig>> _questionConfigMock;
        private static Fixture _fixture;

        private QuestionService _sut;
            
        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();
            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _questionConfigMock = new Mock<IOptions<QuestionConfig>>();
            _questionConfigMock
                .Setup(x => x.Value)
                .Returns(new QuestionConfig {MaxCount = 5});
            
            _sut = new QuestionService(_questionRepositoryMock.Object, _questionConfigMock.Object);
        }
        
        [TestMethod]
        public async Task GetQuestionsAsync_ShouldReturnNull_WhenNoQuestionIsFound()
        {
            // Arrange
            _questionRepositoryMock
                .Setup(x => x.GetQuestions())
                .ReturnsAsync((IEnumerable<QuestionObject>) null);
            
            // Act
            var result = await _sut.GetQuestionsAsync();
            
            // Assert
            result
                .Should()
                .BeNull();

            _questionRepositoryMock.Verify(x => x.GetQuestions(), Times.Once);
        }
        
        [TestMethod]
        public async Task GetQuestionsAsync_ShouldReturnSetQuestionCount()
        {
            // Arrange
            var questions = _fixture.CreateMany<QuestionObject>(10);

            _questionRepositoryMock.Setup(x => x.GetQuestions()).ReturnsAsync(questions);
            
            // Act
            var result = await _sut.GetQuestionsAsync();
            
            // Assert
            result
                .Should()
                .NotBeNull();
            
            result
                .Questions
                .Should()
                .HaveCount(5);

            _questionRepositoryMock.Verify(x => x.GetQuestions(), Times.Once);
        }
        
        [TestMethod]
        public async Task SubmitAnswersAsync_ShouldReturnNull_WhenNoAnswersArePassed()
        {
            // Arrange Act
            var result = await _sut.SubmitAnswersAsync(null);
            
            // Assert
            result
                .Should()
                .BeNull();
            
            _questionRepositoryMock.Verify(x => x.GetQuestions(), Times.Never);
        }
        
        [TestMethod]
        public async Task SubmitAnswersAsync_ShouldReturnCorrectPersonality_WhenAnswersArePassed()
        {
            // Arrange
            var parameters = new List<SubmitAnswersParameters>
            {
                new SubmitAnswersParameters
                {
                    QuestionId = 1,
                    ChosenAnswerId = "D"
                },
                new SubmitAnswersParameters
                {
                    QuestionId = 2,
                    ChosenAnswerId = "D"
                },
                new SubmitAnswersParameters
                {
                    QuestionId = 3,
                    ChosenAnswerId = "A"
                },
                new SubmitAnswersParameters
                {
                    QuestionId = 4,
                    ChosenAnswerId = "C"
                }
            };
            
            var questions = _fixture.CreateMany<QuestionObject>(10);

            _questionRepositoryMock.Setup(x => x.GetQuestions()).ReturnsAsync(questions);
            
            // Act
            var result = await _sut.SubmitAnswersAsync(parameters);
            
            // Assert
            result
                .Should()
                .NotBeNull();

            result
                .Personality
                .Should()
                .Be("You are more of a public extrovert and private introvert.");

            _questionRepositoryMock.Verify(x => x.GetQuestions(), Times.Once);
        }
    }
}