namespace PersonalityTest.Api.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Shared.Parameters;
    using Shared.Results;

    [ApiController]
    [Route("api/personality")]
    public class PersonalityController : Controller
    {
        private readonly IQuestionService _questionService;

        public PersonalityController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetQuestionsResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPersonalityQuestions()
        {
            var result = await _questionService.GetQuestionsAsync();

            if (result == null)
                return NotFound();
            
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AnsweredQuestionsResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SubmitPersonalityAnswers(SubmitAnswersParameters[] parameters)
        {
            var result = await _questionService.SubmitAnswersAsync(parameters.ToList());

            if (result == null)
                return BadRequest();
            
            return Ok(result);
        }
    }
}