using Outloko.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace Outloko.Services.Interfaces
{
    public interface IMailService
    {
        IList<MailMessage> GetInboxItems();
        IList<MailMessage> GetSentItems();
        IList<MailMessage> GetDeletedItems();
    }
}
