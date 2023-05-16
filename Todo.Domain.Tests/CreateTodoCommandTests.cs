using Todo.Domain.Commands;

namespace Todo.Domain.Tests
{
    public class CreateTodoCommandTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
        private readonly CreateTodoCommand _commandValid = new CreateTodoCommand("Título da tarefa", "Rafaela", DateTime.Now);

        public CreateTodoCommandTests()
        {
            _invalidCommand.Validate();
            _commandValid.Validate();
        }

        [Fact]
        public void ShouldBeAbleToDoAInvalidCommand()
        {
            Assert.False(_invalidCommand.Valid);
        }

        [Fact]
        public void ShouldBeAbleToDoAValidCommand()
        {
            Assert.True(_commandValid.Valid);
        }
    }
}