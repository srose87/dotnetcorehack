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
    using Dotnetcorehack.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;

    public class SurveysQuestionsController : Controller
    {
        private IQuestionRepository questionRepository;

        public SurveysQuestionsController(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        [HttpGet]
        [Route("/surveys/{id}/questions")]
        public IActionResult Get(int id)
        {
            var questions = this.questionRepository.GetQuestionsFromSurveyId(id);
            if (questions == null)
            {
                return this.NotFound();
            }

            return this.Json(questions);
        }
    }
}
