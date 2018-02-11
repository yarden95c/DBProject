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
using FirstFloor.ModernUI.Windows.Controls;
using Project.Client.ViewModel;

namespace Project.Client.View
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
            this.DataContext = new SignUpViewModel();
        }

        
        private void UIElement_OnIsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ModernWindow m = (ModernWindow)Application.Current.MainWindow;
                if (m != null) m.ContentSource = m.MenuLinkGroups[2].Links[0].Source;
            });
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ModernWindow m = (ModernWindow)Application.Current.MainWindow;
                if (m != null) m.ContentSource = m.MenuLinkGroups[2].Links[0].Source;
            });
        }
    }

}
