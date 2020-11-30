namespace Gemfruit.Mod.API.Errors
{
    public class Error
    {
        public object Sender { get; protected set; }
        public string Message { get; protected set; }
        
        public Error(object sender, string message)
        {
            Sender = sender;
            Message = message;
        }
        
    }
}