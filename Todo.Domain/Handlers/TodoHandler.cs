using Flunt.Notifications;
using System.Runtime.InteropServices;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler :
        Notifiable,
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>
    {
        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericCommandResult(
                    false,
                    "Ops, parece que sua tarefa está errada!",
                    command.Notifications
                );
            }

            var todo = new TodoItem(command.Title, command.User, command.Date);

            _repository.Create(todo);

            return new GenericCommandResult(
                true,
                "Tarefa salva!",
                todo
            );
            //Sempre parte do domínio para o banco
            //Quando inclui um todo já retorna o id dele (não precisa dar um get)
        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericCommandResult(
                    false,
                    "Ops, parece que sua tarefa está errada!",
                    command.Notifications
                );
            }

            var todo = _repository.GetById(command.Id, command.User);

            todo.UpdateTitle(command.Title);

            _repository.Update(todo);

            return new GenericCommandResult(
                true,
                "Tarefa salva!",
                todo
            );
        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericCommandResult(
                    false,
                    "Ops, parece que sua tarefa está errada!",
                    command.Notifications);
            }

            var todo = _repository.GetById(command.Id, command.User);

            todo.MarkAsDone();

            _repository.Update(todo);

            return new GenericCommandResult(
                true,
                "Tarefa salva!",
                todo);
        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                return new GenericCommandResult(
                    false,
                    "Ops, parece que sua tarefa está errada!",
                    command.Notifications);
            }

            var todo = _repository.GetById(command.Id, command.User);

            todo.MarkAsUndone();

            _repository.Update(todo);

            return new GenericCommandResult(
                true,
                "Tarefa salva!",
                todo);
        }
    }
}
