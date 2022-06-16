namespace PersonalityTest.Shared.Results
{
    using System.Collections.Generic;
    using Model;
    
    public class AnsweredQuestionsResult
    {
        public AnsweredQuestionsResult(
            string personality,
            IEnumerable<QuestionAnswerDto> questionAnswers)
        {
            Personality = personality;
            QuestionAnswers = questionAnswers;
        }

        public string Personality { get; set; }
        
        public IEnumerable<QuestionAnswerDto> QuestionAnswers { get; set; }
    }
}