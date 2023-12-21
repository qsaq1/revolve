using Microsoft.Win32.SafeHandles;
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

namespace UP
{
    /// <summary>
    /// Логика взаимодействия для Provider.xaml
    /// </summary>
    public partial class Providers : Page
    {
        public Providers()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                
                BussEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProvider.ItemsSource = BussEntities.GetContext().Provider.ToList();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new ProvEdit(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.MainFrame.Navigate(new ProvEdit((sender as Button).DataContext as Provider));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DGridProvider.SelectedItems.Cast<Provider>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить слудующие {hotelsForRemoving.Count()} элементов", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    BussEntities.GetContext().Provider.RemoveRange(hotelsForRemoving);
                    BussEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    DGridProvider.ItemsSource = BussEntities.GetContext().Provider.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
