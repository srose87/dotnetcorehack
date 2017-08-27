namespace Dotnetcorehack.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Dotnetcorehack.Models;

    public class SurveyRepository : ISurveyRepository
    {
        private DataContext dataSurvey;

        public SurveyRepository(DataContext dataContext)
        {
            this.dataSurvey = dataContext;
        }

        public Survey GetSurveyById(int id)
        {
            return this.dataSurvey.Surveys.FirstOrDefault(t => t.Id == id);
        }

        public List<Survey> GetSurveys()
        {
            return this.dataSurvey.Surveys.ToList();
        }

        public Survey UpdateSurvey(int id, Survey survey)
        {
            var surveyToUpdate = this.dataSurvey.Surveys.FirstOrDefault(n => n.Id == id);

            if (surveyToUpdate == null)
            {
                return null;
            }

            this.dataSurvey.Surveys.Update(surveyToUpdate);
            this.dataSurvey.SaveChanges();

            return surveyToUpdate;
        }

        public Survey CreateSurvey(Survey survey)
        {
            this.dataSurvey.Surveys.Add(survey);
            this.dataSurvey.SaveChanges();

            return survey;
        }

        public void DeleteSurvey(int id)
        {
            this.dataSurvey.Surveys.Remove(this.dataSurvey.Surveys.FirstOrDefault(n => n.Id == id));
            this.dataSurvey.SaveChanges();
        }
    }
}
