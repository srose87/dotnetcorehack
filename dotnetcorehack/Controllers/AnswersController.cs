namespace Dotnetcorehack.Controllers
{
    using Dotnetcorehack.Models;
    using Dotnetcorehack.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("/[controller]")]
    public class AnswersController : Controller
    {
        private IAnswerRepository answerRepository;

        public AnswersController(IAnswerRepository answerRepository)
        {
            this.answerRepository = answerRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = this.answerRepository.GetAnswerById(id);

            if (question != null)
            {
                return this.Json(question);
            }

            return this.NotFound();
        }

        [HttpPost]
        public IActionResult Post(Answer answer)
        {
            var createdAnswer = this.answerRepository.CreateAnswer(answer);

            return this.Created("/answers/" + createdAnswer.Id, createdAnswer);
        }
    }
}