namespace PersonalityTest.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Shared.Model;
    
    public class QuestionRepository : IQuestionRepository
    {
        public async Task<IEnumerable<QuestionObject>> GetQuestions()
        {
            Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ??
                                        string.Empty, "questions.json");
            
            using var streamReader = new StreamReader(filePath);
            var questions = await streamReader.ReadToEndAsync();
                
            return JsonConvert.DeserializeObject<List<QuestionObject>>(questions);
        }
    }
}