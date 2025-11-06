using System.Windows.Controls;
using Presentation.ViewModels;

namespace Presentation.Views
{
    public partial class MemberEditView : UserControl
    {
        public MemberEditView(MemberEditViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
