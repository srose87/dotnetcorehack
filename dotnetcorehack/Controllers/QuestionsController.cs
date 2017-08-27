namespace Dotnetcorehack.Controllers
{
    using Dotnetcorehack.Models;
    using Dotnetcorehack.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("/[controller]")]
    public class QuestionsController : Controller
    {
        private IQuestionRepository questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = this.questionRepository.GetQuestionById(id);

            if (question != null)
            {
                return this.Json(question);
            }

            return this.NotFound();
        }

        [HttpPost]
        public IActionResult Post(Question question)
        {
            var createdQuestion = this.questionRepository.CreateQuestion(question);

            return this.Created("/questions/" + createdQuestion.Id, createdQuestion);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Question question)
        {
            var updatedQuestion = this.questionRepository.UpdateQuestion(id, question);

            if (updatedQuestion == null)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.questionRepository.DeleteQuestion(id);

            return this.NoContent();
        }
    }
}