namespace BZPAY_BE.Helpers.FakeMail
{
    public class ClassThatSendsEmail
    {
        private readonly IEmail _email;

        public ClassThatSendsEmail(IEmail email)
        {
            _email = email;
        }

        public void DoSomethingThatCausesAnEmailToGetSent()
        {
            var message = new Message { To = "bob@bob.com", Body = "Hi, Bob!" };
            _email.Send(message);
        }
    }

    public interface IEmail
    {
        void Send(Message message);
    }

    public class Message
    {
        public string To { get; set; }
        public string Body { get; set; }
    }

    public class EmailListDouble : List<Message>, IEmail
    {
        public void Send(Message message)
        {
            Add(message);
        }
    }
}
