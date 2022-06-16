namespace PersonalityTest.Shared.Results
{
    using System.Collections.Generic;
    using Model;
    
    public class GetQuestionsResult
    {
        public GetQuestionsResult(IEnumerable<QuestionObject> questions)
        {
            Questions = questions;
        }
        
        public IEnumerable<QuestionObject> Questions { get; set; }
    }
}