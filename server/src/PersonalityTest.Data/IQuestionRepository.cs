namespace PersonalityTest.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared.Model;
    
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionObject>> GetQuestions();
    }
}