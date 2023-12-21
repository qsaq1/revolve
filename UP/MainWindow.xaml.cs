using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;

namespace UP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Lst_control.Visibility = Visibility.Hidden;
            MainFrame.SetValue(Grid.ColumnSpanProperty, 2);
            MainFrame.Navigate(new Auth());
            FrameManager.MainFrame = MainFrame;
            //Import();
            //ImportS();
        }
        private void Import()
        {
            var fileData = File.ReadAllLines(@"..\1.txt");
            var images = Directory.GetFiles(@"..\materials");
            foreach (var line in fileData)
            {
                var data = line.Split('\t');

                var tempMaterial = new Material
                {
                    Наименование = data[0].ToString(),
                    ТипМатериала = data[1].ToString(),
                    Цена = decimal.Parse(data[3]),
                    КолНаСкладе = int.Parse(data[4]),
                    МинКолНаСкладе = int.Parse(data[5]),
                    КолУпаковки = int.Parse(data[6]),
                    ЕдиницаИзм = data[7].ToString()
                };

                try
                {
                    tempMaterial.ИзображениеМатериала = File.ReadAllBytes(".." + data[2].ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                BussEntities.GetContext().Material.Add(tempMaterial);
                BussEntities.GetContext().SaveChanges();
            }
        }

        private void ImportS()
        {
            var fileData = File.ReadAllLines(@"..\2.txt");
            
            foreach (var line in fileData)
            {
                var data = line.Split('\t');

                
                    var currentType = BussEntities.GetContext().Material.ToList();
                int types = 0;
                for (int i = 0; i < currentType.Count; i++) {
                    for (int j = 0; j < data[0].Length; j++) {
                        if (data[0].ToString() == currentType[i].Наименование.ToString())
                        {
                            types = currentType[i].IDМатериала;
                        }


                        //if (currentType != null)
                        //    tempTour.Type.Add(currentType);
                    }
                }
               var currentProv = BussEntities.GetContext().Provider.ToList();
                int prov = 0;
                for (int i = 0; i < currentProv.Count; i++)
                {
                    for (int j = 0; j < data[1].Length; j++)
                    {
                        if (data[1].ToString() == currentProv[i].Наименование.ToString())
                        {
                            prov = currentProv[i].IDПоставщика;
                        }
                    }

                }

                
                if (prov != 0 && types != 0) 
                {
                    var post = new  PossibleSuppliers()
                    {
                        IDМатериала = types,
                        IDПоставщика = prov
                    };
                    BussEntities.GetContext().PossibleSuppliers.Add(post);
                    BussEntities.GetContext().SaveChanges();
                }

                

            }
            
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if(AuthStatus.InAuth == false)
            {
                Lst_control.Visibility = Visibility.Hidden;
                MainFrame.SetValue(Grid.ColumnSpanProperty, 2);
            }
            else
            {
                Lst_control.Visibility= Visibility.Visible;
                MainFrame.SetValue(Grid.ColumnProperty, 1);
                MainFrame.SetValue(Grid.ColumnSpanProperty, 1);
                Lst_control.Items.Clear();
                if(AuthStatus.Status == "Admin" || AuthStatus.Status == "Manager")
                {
                    Lst_control.Items.Add("Материалы");
                }
                if (AuthStatus.Status == "Admin")
                {
                    Lst_control.Items.Add("Поставщики");
                }
            }

        }

        private void Lst_control_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Lst_control.Items.Count > 0 && Lst_control.SelectedIndex != -1)
            {
                if (Lst_control.Items[Lst_control.SelectedIndex].ToString() == "Материалы")
                {
                    MainFrame.Navigate(new Materials());
                }

                if (Lst_control.Items[Lst_control.SelectedIndex].ToString() == "Поставщики")
                {
                    MainFrame.Navigate(new Providers());
                }
            }

        }
    }
}
