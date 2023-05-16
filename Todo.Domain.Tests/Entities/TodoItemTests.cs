using Todo.Domain.Entities;

namespace Todo.Domain.Tests.Entities
{
    public class TodoItemTests
    {
        [Fact]
        public void ShouldNotCreateOneTodoIemAlreadyFinished()
        {
            var todoItem = new TodoItem("Some title", "Rafaela", DateTime.Now);
            Assert.False(todoItem.Done);
        }
    }
}
