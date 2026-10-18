using System;

namespace EduHub.Ch02.Services
{
    public interface INotificationService
    {
        void Send(string recipient, string message);
    }

    public class EmailNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"Sending Email to {recipient}: {message}");
        }
    }

    public class SlackNotificationService : INotificationService
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"Sending Slack notification to {recipient}: {message}");
        }
    }

    
    public static class NotificationServiceFactory
    {
        public static INotificationService Create(string channel)
        {
            return channel.ToLower() switch
            {
                "email" => new EmailNotificationService(),
                "slack" => new SlackNotificationService(),
                _ => new EmailNotificationService()
            };
        }
    }
}