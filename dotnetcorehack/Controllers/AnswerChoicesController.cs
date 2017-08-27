namespace Dotnetcorehack.Controllers
{
    using Dotnetcorehack.Models;
    using Dotnetcorehack.Repositories.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("/[controller]")]
    public class AnswerChoicesController : Controller
    {
        private IAnswerChoiceRepository answerChoiceRepository;

        public AnswerChoicesController(IAnswerChoiceRepository answerChoiceRepository)
        {
            this.answerChoiceRepository = answerChoiceRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var answerChoice = this.answerChoiceRepository.GetAnswerChoiceById(id);

            if (answerChoice != null)
            {
                return this.Json(answerChoice);
            }

            return this.NotFound();
        }

        [HttpPost]
        public IActionResult Post(AnswerChoice answerChoice)
        {
            var createdAnswerChoice = this.answerChoiceRepository.CreateAnswerChoice(answerChoice);

            return this.Created("/answerChoices/" + createdAnswerChoice.Id, createdAnswerChoice);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AnswerChoice answerChoice)
        {
            var updatedAnswerChoice = this.answerChoiceRepository.UpdateAnswerChoice(id, answerChoice);

            if (updatedAnswerChoice == null)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.answerChoiceRepository.DeleteAnswerChoice(id);

            return this.NoContent();
        }
    }
}