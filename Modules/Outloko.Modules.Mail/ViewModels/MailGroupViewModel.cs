using Outloko.Business;
using Outloko.Core;
using Outloko.Modules.Mail.Properties;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloko.Modules.Mail.ViewModels
{
    public class MailGroupViewModel : ViewModelBase
    {
        private ObservableCollection<NavigationItem> _items;
        public ObservableCollection<NavigationItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private DelegateCommand<NavigationItem> _selectedCommand;
        private readonly IApplicationCommands applicationCommands;

        public DelegateCommand<NavigationItem> SelectedCommand =>
            _selectedCommand ?? (_selectedCommand = new DelegateCommand<NavigationItem>(ExecuteSelectedCommand));


        public MailGroupViewModel(IApplicationCommands applicationCommands)
        {
            GenerateMenu();
            this.applicationCommands = applicationCommands;
        }
        void ExecuteSelectedCommand(NavigationItem navigationItem)
        {
            if (navigationItem != null)
                applicationCommands.NavigateCommand.Execute(navigationItem.NavigationPath);
        }

        void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();
            NavigationItem root = new NavigationItem() { Caption = "Personal Folder", NavigationPath = "MailList", IsExpanded = true };
            root.Items.Add(new NavigationItem() { Caption = Resources.Folder_Inbox, NavigationPath = GetNavigationPath(FolderParameters.Inbox) });
            root.Items.Add(new NavigationItem() { Caption = Resources.Folder_Deleted, NavigationPath = GetNavigationPath(FolderParameters.Deleted) });
            root.Items.Add(new NavigationItem() { Caption = Resources.Folder_Sent, NavigationPath = GetNavigationPath(FolderParameters.Sent) });
            Items.Add(root);

        }

        string GetNavigationPath(string folder)
        {
            return $"MailList?{FolderParameters.FolderKey}={folder}";
        }
    }

    public class FolderParameters
    {
        public const string FolderKey = "Folder";

        public const string Inbox = "Inbox";
        public const string Sent = "Sent";
        public const string Deleted = "Deleted";
    }
}
