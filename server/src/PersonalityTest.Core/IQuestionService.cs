namespace PersonalityTest.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared.Parameters;
    using Shared.Results;
    
    public interface IQuestionService
    {
        Task<GetQuestionsResult> GetQuestionsAsync();

        Task<AnsweredQuestionsResult> SubmitAnswersAsync(IEnumerable<SubmitAnswersParameters> parameters);
    }
}