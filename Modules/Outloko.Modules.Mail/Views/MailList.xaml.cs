using Outloko.Core;
using Outloko.Modules.Mail.Menus;
using System.Windows.Controls;

namespace Outloko.Modules.Mail.Views
{
    /// <summary>
    /// Interaction logic for MailList
    /// </summary>
    [DependentView(RegionNames.RibbonRegion, typeof(HomeTab))]
    public partial class MailList : UserControl
    {
        public MailList()
        {
            InitializeComponent();
        }
    }
}
