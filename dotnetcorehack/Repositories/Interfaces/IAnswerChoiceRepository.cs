namespace Dotnetcorehack.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Dotnetcorehack.Models;

    public interface IAnswerChoiceRepository
    {
        AnswerChoice GetAnswerChoiceById(int id);

        AnswerChoice CreateAnswerChoice(AnswerChoice answerChoice);

        AnswerChoice UpdateAnswerChoice(int id, AnswerChoice answerChoice);

        void DeleteAnswerChoice(int id);
    }
}