using System;

namespace ConsoleApp8
{
    public interface INotification
    {
        void Send(string message);
    }
    public class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("Email Sent: " + message);
        }
    }
        public class SMSNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("SMS Sent: " + message);
        }
    }
    public class PushNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine("Push Notification Sent: " + message);
        }
    }
    public class NotificationFactory
    {
        public INotification CreateNotification(string type)
        {
            switch (type.ToLower())
            {
                case "email":
                    return new EmailNotification();

                case "sms":
                    return new SMSNotification();

                case "push":
                    return new PushNotification();

                default:
                    throw new ArgumentException("Invalid notification type");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            NotificationFactory factory = new NotificationFactory();

            Console.WriteLine("Enter notification type (email/sms/push): ");
            string type = Console.ReadLine();

            INotification notification = factory.CreateNotification(type);

            notification.Send("Welcome to our service!");
        }
    }
}