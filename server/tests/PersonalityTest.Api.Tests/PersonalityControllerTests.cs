using System.Collections.Generic;
using PersonalityTest.Shared.Parameters;

namespace PersonalityTest.Api.Tests
{
    using System.Net;
    using System.Threading.Tasks;
    using AutoFixture;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Controllers;
    using Core;
    using Shared.Results;
    
    [TestClass]
    public class PersonalityControllerTests
    {
        private Mock<IQuestionService> _questionServiceMock;
        private PersonalityController _controllerUnderTest;
        private static Fixture _fixture;
        
        [TestInitialize]
        public void Setup()
        {
            _fixture = new Fixture();

            _questionServiceMock = new Mock<IQuestionService>();

            _controllerUnderTest = new PersonalityController(_questionServiceMock.Object);
        }
        
        [TestMethod]
        public async Task GetPersonalityQuestions_ShouldReturnNotFound_WhenResultIsNull()
        {
            // Arrange
            _questionServiceMock
                .Setup(x => x.GetQuestionsAsync())
                .ReturnsAsync((GetQuestionsResult)null);

            // Act
            var response = await _controllerUnderTest
                .GetPersonalityQuestions() as NotFoundResult;

            // Assert
            response
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.NotFound);

            _questionServiceMock.Verify(x => x.GetQuestionsAsync(), Times.Once);
        }
        
        [TestMethod]
        public async Task GetPersonalityQuestions_ShouldReturnOk_WhenResultIsNotNull()
        {
            // Arrange
            var questions = _fixture.Create<GetQuestionsResult>();
            
            _questionServiceMock
                .Setup(x => x.GetQuestionsAsync())
                .ReturnsAsync(questions);

            // Act
            var response = await _controllerUnderTest
                .GetPersonalityQuestions() as ObjectResult;

            // Assert
            response
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.OK);
            
            response
                .Value
                .Should()
                .BeOfType<GetQuestionsResult>();
            
            response
                .Value
                .As<GetQuestionsResult>()
                .Questions
                .Should()
                .NotBeNull();
            
            response
                .Value
                .As<GetQuestionsResult>()
                .Questions
                .Should()
                .BeEquivalentTo(questions.Questions);

            _questionServiceMock.Verify(x => x.GetQuestionsAsync(), Times.Once);
        }
        
        [TestMethod]
        public async Task SubmitPersonalityAnswers_ShouldReturnBadRequest_WhenParamIsWrong()
        {
            // Arrange
            var answers = _fixture.Create<List<SubmitAnswersParameters>>();
            
            _questionServiceMock
                .Setup(x => x.SubmitAnswersAsync(It.IsAny<List<SubmitAnswersParameters>>()))
                .ReturnsAsync((AnsweredQuestionsResult) null);

            // Act
            var response = await _controllerUnderTest
                .SubmitPersonalityAnswers(answers.ToArray()) as BadRequestResult;

            // Assert
            response
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.BadRequest);

            _questionServiceMock.Verify(x =>
                x.SubmitAnswersAsync(It.IsAny<List<SubmitAnswersParameters>>()), Times.Once);
        }
        
        [TestMethod]
        public async Task SubmitPersonalityAnswers_ShouldReturnOk_WhenResultIsNotNull()
        {
            // Arrange
            var answers = _fixture.Create<List<SubmitAnswersParameters>>();
            
            var result = _fixture.Create<AnsweredQuestionsResult>();
            
            _questionServiceMock
                .Setup(x => x.SubmitAnswersAsync(It.IsAny<List<SubmitAnswersParameters>>()))
                .ReturnsAsync(result);

            // Act
            var response = await _controllerUnderTest
                .SubmitPersonalityAnswers(answers.ToArray()) as ObjectResult;

            // Assert
            response
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.OK);
            
            response
                .Value
                .Should()
                .BeOfType<AnsweredQuestionsResult>();
            
            response
                .Value
                .As<AnsweredQuestionsResult>()
                .QuestionAnswers
                .Should()
                .NotBeNull();
            
            response
                .Value
                .As<AnsweredQuestionsResult>()
                .QuestionAnswers
                .Should()
                .BeEquivalentTo(result.QuestionAnswers);

            _questionServiceMock.Verify(x => x.SubmitAnswersAsync(It.IsAny<List<SubmitAnswersParameters>>()),
                Times.Once);
        }
    }
}