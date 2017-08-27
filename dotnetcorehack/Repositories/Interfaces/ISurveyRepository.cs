namespace Dotnetcorehack.Repositories.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using Dotnetcorehack.Models;

    public interface ISurveyRepository
    {
        Survey GetSurveyById(int id);

        List<Survey> GetSurveys();

        Survey UpdateSurvey(int id, Survey survey);

        Survey CreateSurvey(Survey survey);

        void DeleteSurvey(int id);
    }
}