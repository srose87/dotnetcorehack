namespace Dotnetcorehack.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Dotnetcorehack.Factories;
    using Dotnetcorehack.Models;
    using Dotnetcorehack.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;

    [Route("/[controller]")]
    public class SurveysController : Controller
    {
        private ISurveyRepository surveyRepository;

        public SurveysController(ISurveyRepository surveyRepository)
        {
            this.surveyRepository = surveyRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var survey = this.surveyRepository.GetSurveyById(id);
            if (survey == null)
            {
                return this.NotFound();
            }

            return this.Json(survey);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Survey survey)
        {
            this.surveyRepository.CreateSurvey(survey);

            return this.Created("/surveys/" + survey.Id.ToString(), survey);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Survey survey)
        {
            var surveyUpdated = this.surveyRepository.UpdateSurvey(id, survey);

            if (surveyUpdated == null)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.surveyRepository.DeleteSurvey(id);
        }
    }
}
