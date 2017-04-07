using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EuroJobsCrm.Models;
using EuroJobsCrm.Services;

namespace EuroJobsCrm
{
    public class EventRemindWorker
    {
        private Timer _timer;

        public void DoWork()
        {
            _timer = new Timer(TimerCallBack, null, new TimeSpan(0, 1, 0), new TimeSpan(2, 0, 0));
        }

        public void Stop()
        {
            _timer?.Dispose();
        }

        private void TimerCallBack(object state)
        {
            try
            {
                using (DB_A12601_bielkaContext context = new DB_A12601_bielkaContext())
                {
                    var eventsToRemind = context.Notes.Where(n =>
                                n.NotAuditRd == null && n.NotReminded != true && n.NotRemindDate != null &&
                                n.NotRemindDate < DateTime.Now).ToList();

                    var userEmails = context.AspNetUsers.Where(u => !u.Deleted).ToDictionary(u => u.Id, u => u.Email);

                    foreach (Notes @event in eventsToRemind)
                    {
                        var emails = userEmails.Where(u => u.Key == @event.NotTargetUser || u.Key == @event.NotAuditCu)
                            .Select(u => u.Value)
                            .ToList();

                        SendNotification(@event, emails);
                        @event.NotReminded = true;
                    }

                    context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void SendNotification(Notes @event, List<string> emails)
        {
            IEmailMessageBuilder messageBuilder = new EventNotificationBuilder(@event);
            string message = messageBuilder.BuildBody();
            string subject = messageBuilder.BuildSubject();

            IEmailSender sender = new NotificationEmailSender();
            foreach (string email in emails)
            {
                sender.SendEmailAsync(email, subject, message);
            }
        }
    }

    
}
