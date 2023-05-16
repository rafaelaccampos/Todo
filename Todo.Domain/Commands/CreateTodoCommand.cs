using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands
{
    public class CreateTodoCommand : Notifiable, ICommand
    {
        public CreateTodoCommand()
        {

        }

        public CreateTodoCommand(string title, string user, DateTime date)
        {
            Title = title;
            User = user;
            Date = date;
        }

        public string Title { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }

        /* Não é bom lançar exceções no dominio, o ideal é armazenar para retornar tudo de uma vez
           Pode causar problemas de performance porque toda vez que uma exceção por muito entrada/saida (I/O)
        */
        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(Title, 3, "Title", "Por favor, descreva melhor essa tarefa!")
                .HasMinLen(User, 6, "User", "Usuário inválido!")
           );
        }
    }
}
