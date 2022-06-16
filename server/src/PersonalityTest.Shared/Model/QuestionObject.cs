namespace PersonalityTest.Shared.Model
{
    using System.Collections.Generic;
    
    public class QuestionObject
    {
        public QuestionObject(
            int id,
            string question,
            IEnumerable<AnswerDto> answers)
        {
            Id = id;
            Question = question;
            Answers = answers;
        }
        
        public int Id { get; set; }

        public string Question { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}