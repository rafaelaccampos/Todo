using System.ComponentModel.DataAnnotations;
using Flunt.Validations;

namespace Todo.Domain.Commands.Contracts
{
    public interface ICommand : IValidatable
    {
    }
}
