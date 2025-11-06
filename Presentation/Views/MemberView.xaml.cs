using System.Windows;
using Presentation.ViewModels;

namespace Presentation.Views
{
    public partial class MemberView : Window
    {
        private MemberViewModel ViewModel => (MemberViewModel)DataContext;

        public MemberView()
        {
            InitializeComponent();
            DataContext = new MemberViewModel();
        }

        private void UpdateMembership_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateMembership();
        }

    }
}
