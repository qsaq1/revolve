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
    /// Логика взаимодействия для ProvEdit.xaml
    /// </summary>
    public partial class ProvEdit : Page
    {
        private Provider _currentProviders = new Provider();
        public ProvEdit(Provider selectedHotel)
        {
            InitializeComponent();
            if (selectedHotel != null)
                _currentProviders = selectedHotel;
            DataContext = _currentProviders;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(_currentProviders.Наименование))
                errors.AppendLine("Укажите название отеля");
            if (string.IsNullOrEmpty(_currentProviders.ИНН.ToString()))
                errors.AppendLine("Инн не должени быть пустым");
            if (string.IsNullOrEmpty(_currentProviders.ТипПоставщика))
                errors.AppendLine("Укажите тип поставщика");
            if (string.IsNullOrEmpty(_currentProviders.РейтингКачества.ToString()))
                errors.AppendLine("Укажите рейтинг качества");
            if (string.IsNullOrEmpty(_currentProviders.ДатаНачалоРаботы.ToString()))
                errors.AppendLine("Укажите дату начало работы");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentProviders.IDПоставщика == 0)
            {
                BussEntities.GetContext().Provider.Add(_currentProviders);
            }
            try
            {
                BussEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация Сохранена!");
                FrameManager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
