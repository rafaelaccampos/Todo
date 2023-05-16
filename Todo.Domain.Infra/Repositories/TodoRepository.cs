using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Queries;
using Todo.Domain.Repositories;

namespace Todo.Domain.Infra.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _todoContext;

        public TodoRepository(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public void Create(TodoItem todo)
        {
            _todoContext.Todos.Add(todo);
            _todoContext.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAll(string user)
        {
            return _todoContext.Todos
                .AsNoTracking()
                .Where(TodoQueries.GetAll(user))
                .OrderBy(t => t.Date);
        }

        public IEnumerable<TodoItem> GetAllDone(string user)
        {
            return _todoContext.Todos
                .AsNoTracking()
                .Where(TodoQueries.GetAllDone(user))
                .OrderBy(x => x.Date);
        }

        public IEnumerable<TodoItem> GetAllUndone(string user)
        {
            return _todoContext.Todos
                 .AsNoTracking()
                 .Where(TodoQueries.GetAllUndone(user))
                 .OrderBy(x => x.Date);
        }

        public TodoItem? GetById(Guid id, string user)
        {
            return _todoContext.Todos
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id && x.User == user);
        }

        public IEnumerable<TodoItem> GetByPeriod(string user, DateTime date, bool done)
        {
            return _todoContext.Todos
                .AsNoTracking()
                .Where(TodoQueries.GetByPeriod(user, date, done))
                .OrderBy(x => x.Date);
        }

        public void Update(TodoItem todo)
        {
            /*
                segunda versão com as modificações e o ef 
                sabe qual é o campo que muda e só escreve uma query (update) com o que mudou
             */ 
            _todoContext.Entry(todo).State = EntityState.Modified;
            _todoContext.SaveChanges();
        }
    }
}
