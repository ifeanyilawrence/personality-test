namespace PersonalityTest.Shared.Model
{
    public class AnswerDto
    {
        public AnswerDto(
            string id,
            string text)
        {
            Id = id;
            Text = text;
        }
        
        public string Id { get; set; }

        public string Text { get; set; }
    }
}