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
using WinDoorsWPF.Model;
using WinDoorsWPF.ViewModel;
using WinDoorsWPF.View;
namespace WinDoorsWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     //   Person person=new Person();
    //    Price price = new Price();
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Price();
            //.GetPricesGoogle();
         //   Price m = new Price();
        //    m.GetPricesGoogle();
            // dataGrid.ItemsSource = m.priceList.Materials; 
         //   materialList.ItemsSource = m.priceList.Materials;
       //     materialList.DisplayMemberPath = "Name";

        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {

         //   Window_ windows = new Window_();
        //    windows.Height = windows.GetDouble(heightBox.Text, 0);
        //    windows.Width = windows.GetDouble(widthBox.Text, 0);
          //  windows.Delivery = GetDouble(delivery.Text, 0);
       //     windows.InstallWindow = GetDouble(installWindow.Text, 0);
        //    windows.Metering = GetDouble(metering.Text, 0);
        //    if (person.FirstName != nameBox1.Text || person.SecondName != nameBox2.Text)
            {
      //          person = new Person();
      //          person.Name = sNameBox.Text + " " + fNameBox.Text;
      //          person.Phone = phoneNumber.Text;
            }
         //   windows.Deaf = checkDeaf.IsChecked.Value;
       //     windows.OpenWindow = openWindow.Checked;
      //      windows.Pipe = checkPipe1.Checked;
       //     windows.PaintPipe = paintPipe.Checked;
       //     windows.FullOpenWindow = fullOpenWindow.Checked;
       //     windows.Flash = flash1.Checked;

        //    if (materialList.SelectedItem != null)
            {
                
        //        windows.Material = materialList.SelectedValue.ToString();


                //windows.Cutting = cuttingbox.Checked;
              //  person.Windows.Add(windows);

                CalculateWindow f = new CalculateWindow ();
                
                f.Show();
            }
        }

        private void priceButton_Click(object sender, RoutedEventArgs e)
        {
            PriceListWindow f = new PriceListWindow();
            f.DataContext = DataContext;
            f.Show();

        }
    }
}
