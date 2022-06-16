namespace PersonalityTest.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.Extensions.Options;
    using Shared.Config;
    using Shared.Model;
    using Shared.Parameters;
    using Shared.Results;
    
    public class QuestionService: IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly QuestionConfig _questionConfig;

        private static readonly string[] IntrovertAnswers = {"A", "B"};
        private static readonly string[] PublicIntrovertAnswers = {"C"};
        private static readonly string[] PublicExtrovertAnswers = {"D"};

        public QuestionService(
            IQuestionRepository questionRepository,
            IOptions<QuestionConfig> questionConfig)
        {
            _questionRepository = questionRepository;
            _questionConfig = questionConfig.Value;
        }

        public async Task<GetQuestionsResult> GetQuestionsAsync()
        {
            var questions = await _questionRepository.GetQuestions();

            var rnd = new Random();

            return questions == null
                ? null
                : new GetQuestionsResult(questions.OrderBy(x => rnd.Next()).Take(_questionConfig.MaxCount).ToList());
        }

        public async Task<AnsweredQuestionsResult> SubmitAnswersAsync(IEnumerable<SubmitAnswersParameters> parameters)
        {
            var submitAnswers = parameters?.ToList();
            if (submitAnswers == null || !submitAnswers.Any())
            {
                return null;
            }

            var introvertAnswerCount = submitAnswers
                .Count(x => IntrovertAnswers.Contains(x.ChosenAnswerId));

            var publicIntrovertAnswerCount = submitAnswers
                .Count(x => PublicIntrovertAnswers.Contains(x.ChosenAnswerId));
            
            var publicExtrovertAnswerCount = submitAnswers
                .Count(x => PublicExtrovertAnswers.Contains(x.ChosenAnswerId));

            var maxPersonalityCount = Math.Max(introvertAnswerCount,
                Math.Max(publicIntrovertAnswerCount, publicExtrovertAnswerCount));
            
            var questions = await _questionRepository.GetQuestions();
            var submittedAnswersIdList = submitAnswers.Select(x => x.QuestionId).ToList();
            var questionObjects = questions.ToList();
            var answeredQuestions = questionObjects
                .Where(x => submittedAnswersIdList.Contains(x.Id))
                .Select(x => new QuestionAnswerDto(
                    id: x.Id,
                    question: x.Question,
                    answers: x.Answers,
                    chosenAnswerId: submitAnswers.First(s => s.QuestionId == x.Id).ChosenAnswerId));
            
            if (maxPersonalityCount.Equals(introvertAnswerCount))
            {
                return new AnsweredQuestionsResult(
                    personality: "You are more of an introvert.",
                    questionAnswers: answeredQuestions
                );
            } 
            else if (maxPersonalityCount.Equals(publicIntrovertAnswerCount))
            {
                return new AnsweredQuestionsResult(
                    personality: "You are more of a public introvert and public extrovert.",
                    questionAnswers: answeredQuestions
                );
            }
            else
            {
                return new AnsweredQuestionsResult(
                    personality: "You are more of a public extrovert and private introvert.",
                    questionAnswers: answeredQuestions
                );
            }
        }
    }
}