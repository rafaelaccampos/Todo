using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests
{
    public class TodoQueriesTests
    {
        [Fact]
        public void GivenTheQueryShouldReturnOnlyTasksDone()
        {
            var items = new List<TodoItem>()
            {
                new TodoItem("Tarefa 1", "usuarioA", DateTime.Now),
                new TodoItem("Tarefa 2", "usuarioA", DateTime.Now),
                new TodoItem("Tarefa 3", "usuarioB", DateTime.Now),
                new TodoItem("Tarefa 4", "usuarioA", DateTime.Now),
                new TodoItem("Tarefa 5", "usuarioB", DateTime.Now)
            };

            var queries = items.AsQueryable().Where(TodoQueries.GetAllDone("usuarioB"));

            Assert.Equal(2, queries.Count());
        }
    }
}
