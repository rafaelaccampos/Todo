using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Handlers.Contracts
{
    //O método IHandler pode ter qualquer tipo desde que esse tipo seja do tipo ICommand
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
