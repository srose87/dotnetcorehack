namespace Dotnetcorehack.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Dotnetcorehack.Models;

    public interface IAnswerRepository
    {
        Answer GetAnswerById(int id);

        Answer CreateAnswer(Answer answer);
    }
}