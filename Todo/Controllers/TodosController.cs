using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    [Authorize]
    public class TodosController : ControllerBase
    {
        /* Annotations
         FromBody tenta converter o Json para o objeto (no caso CreateTodoCommand), é o famoso model binding
         FromServices vem do startup/program já com as dependências definidas
        */

        [HttpPost]
        public GenericCommandResult Create(
            [FromBody] CreateTodoCommand command, 
            [FromServices] TodoHandler handler)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("done")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllDone([FromServices] ITodoRepository todoRepository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return todoRepository.GetAllDone(user);
        }

        [Route("done/today")]
        [HttpGet]
        public IEnumerable<TodoItem> GetDoneForToday([FromServices] ITodoRepository todoRepository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return todoRepository.GetByPeriod(user!, DateTime.Now.Date, true);
        }

        [Route("done/tomorrow")]
        [HttpGet]
        public IEnumerable<TodoItem> GetDoneForTomorrow([FromServices] ITodoRepository todoRepository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return todoRepository.GetByPeriod(user!, DateTime.Now.AddDays(1), true);
        }

        [Route("undone")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllUndone([FromServices] ITodoRepository todoRepository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return todoRepository.GetAllUndone(user!);
        }

        [Route("undone/today")]
        [HttpGet]
        public IEnumerable<TodoItem> GetUndoneForToday([FromServices] ITodoRepository todoRepository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return todoRepository.GetByPeriod(user!, DateTime.Now.Date, false);
        }

        [Route("undone/tomorrow")]
        [HttpGet]
        public IEnumerable<TodoItem> GetUndoneForTomorrow([FromServices] ITodoRepository todoRepository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return todoRepository.GetByPeriod(user, DateTime.Now.Date.AddDays(1), false);
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll([FromServices] ITodoRepository todoRepository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return todoRepository.GetAll(user!);
        }

        [HttpPut]
        public GenericCommandResult Update([FromBody] UpdateTodoCommand command, [FromServices] TodoHandler handler)
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
            return (GenericCommandResult) handler.Handle(command);
        }

        [Route("mark-as-done")]
        [HttpPut]
        public GenericCommandResult MarkAsDone([FromBody] MarkTodoAsDoneCommand command, [FromServices] TodoHandler handler)
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("mark-as-undone")]
        [HttpPut]
        public GenericCommandResult MarkAsUndone([FromBody] MarkTodoAsUndoneCommand command, [FromServices] TodoHandler handler)
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value!;
            return (GenericCommandResult)handler.Handle(command);
        }
    }
}
