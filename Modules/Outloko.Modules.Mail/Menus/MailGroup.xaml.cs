using Infragistics.Controls.Menus;
using Infragistics.Windows.OutlookBar;
using Outloko.Business;
using Outloko.Core;
using Outloko.Modules.Mail.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Outloko.Modules.Mail.Menus
{
    /// <summary>
    /// Interaction logic for MailGroup.xaml
    /// </summary>
    public partial class MailGroup : OutlookBarGroup, IOutlokoBarGroup
    {
        NavigationItem _selectedItem;

        public MailGroup()
        {
            InitializeComponent();
            _dataTree.Loaded += DataTree_Loaded;
        }

        private void DataTree_Loaded(object sender, RoutedEventArgs e)
        {
            _dataTree.Loaded -= DataTree_Loaded;
            _dataTree.Nodes[0].Nodes[0].IsSelected = true;

        }

        public string DefaultNavigationPath
        {
            get
            {
                var item = _dataTree.SelectionSettings.SelectedNodes[0] as XamDataTreeNode;
                if (item != null)
                    return ((NavigationItem)item.Data).NavigationPath;

                //item = ((MailGroupViewModel)DataContext).Items.FirstOrDefault(x => x.Caption == Properties.Resources.Folder_Inbox);
                return $"MailList?{FolderParameters.FolderKey}={FolderParameters.Inbox}";
            }
        }

    }
}
