namespace Dotnetcorehack.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Dotnetcorehack.Models;

    public interface IQuestionRepository
    {
        Question GetQuestionById(int id);

        Question CreateQuestion(Question question);

        Question UpdateQuestion(int id, Question question);

        List<Question> GetQuestionsFromSurveyId(int surveyId);

        void DeleteQuestion(int id);
    }
}