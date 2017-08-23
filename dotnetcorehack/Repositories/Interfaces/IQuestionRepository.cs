namespace Dotnetcorehack.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Dotnetcorehack.Models;

    public interface IQuestionRepository
    {
        Question GetQuestionById(int id);

        Question CreateQuestion(Question question);

        Question UpdateQuestion(int id, Question question);

        void DeleteQuestion(int id);
    }
}