namespace PersonalityTest.Shared.Parameters
{
    public class SubmitAnswersParameters
    {
        public SubmitAnswersParameters()
        {
        }
        
        public SubmitAnswersParameters(
            int questionId,
            string chosenAnswerId)
        {
            QuestionId = questionId;
            ChosenAnswerId = chosenAnswerId;
        }

        public int QuestionId { get; set; }

        public string ChosenAnswerId { get; set; }
    }
}