namespace PersonalityTest.Shared.Model
{
    using System.Collections.Generic;
    
    public class QuestionAnswerDto
    {
        public QuestionAnswerDto(
            int id,
            string question,
            IEnumerable<AnswerDto> answers,
            string chosenAnswerId)
        {
            Id = id;
            Question = question;
            Answers = answers;
            ChosenAnswerId = chosenAnswerId;
        }
        
        public int Id { get; set; }

        public string Question { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }
        
        public string ChosenAnswerId { get; set; }
    }
}