using Outloko.Business;
using Outloko.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Outloko.Services
{
    public class MailService : IMailService
    {
        static List<MailMessage> InboxItems = new List<MailMessage>()
        {
            new MailMessage()
            {
                Id=1,
                From="eu@eu.com",
                To=new ObservableCollection<string>(){"teste@derp.com", "jane@doe.com"},
                Subject="Test email",
                Body="This is the message of the email",
                DateSent = DateTime.Now
            },
            new MailMessage()
            {
                Id=2,
                From="eu@eu.com",
                To=new ObservableCollection<string>(){"teste@derp.com", "jane@doe.com"},
                Subject="Test email 2",
                Body="This is the message of the email 2",
                DateSent = DateTime.Now.AddDays(-1)
            },
            new MailMessage()
            {
                Id=3,
                From="eu@eu.com",
                To=new ObservableCollection<string>(){"teste@derp.com", "jane@doe.com"},
                Subject="Test email 3 ",
                Body="This is the message of the email 3",
                DateSent = DateTime.Now.AddDays(-5)
            }
        };

        static List<MailMessage> SentItems = new List<MailMessage>();

        static List<MailMessage> DeletedItems = new List<MailMessage>();

        public IList<MailMessage> GetDeletedItems()
        {
            return DeletedItems;
        }

        public IList<MailMessage> GetInboxItems()
        {
            return InboxItems;
        }

        public IList<MailMessage> GetSentItems()
        {
            return SentItems;
        }
    }
}
