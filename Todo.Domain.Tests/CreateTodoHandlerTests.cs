using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests
{
    public class CreateTodoHandlerTests
    {

        [Fact]
        public void GivenAnInvalidCommandShouldInterruptExecution()
        {
            var command = new CreateTodoCommand("", "", DateTime.Now);
            var handler = new TodoHandler(new FakeTodoRepository());

            var commandResult = (GenericCommandResult)handler.Handle(command);
            Assert.False(commandResult.Success);
        }

        [Fact]
        public void GivenAValidCommandShouldContinueExecution()
        {
            var command = new CreateTodoCommand("Limpar casa", "Rafaela", DateTime.Now);
            var handler = new TodoHandler(new FakeTodoRepository());

            var commandResult = (GenericCommandResult)handler.Handle(command);
            Assert.True(commandResult.Success);
        }
    }
}
