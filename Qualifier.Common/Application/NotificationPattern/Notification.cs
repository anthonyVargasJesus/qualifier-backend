
namespace Qualifier.Common.Application.NotificationPattern
{
    public class Notification
    {
        public List<Error> errors { get; set; } = new List<Error>();

        public Notification()
        {
        }

        public void addError(string message)
        {
            addError(message, null);
        }

        public void addError(string message, Exception ex)
        {
            errors.Add(new Error(message, ex));
        }

        public string errorMessage()
        {
            return string.Join(",", errors.Select(error => error.message));
        }

        public string errorMessage(string separator)
        {
            return string.Join(separator, errors.Select(error => error.message));
        }

        public bool hasErrors()
        {
            return errors.Count > 0;
        }

    }
}
