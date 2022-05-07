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
using HtmlAgilityPack;

namespace XDXDXDXD
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private decimal precio_dolar;
        public MainWindow()
        {
            InitializeComponent();
            HtmlWeb oWeb = new HtmlWeb();
            HtmlDocument doc = oWeb.Load("http://bcv.org.ve/");
            HtmlNode price = doc.GetElementbyId("dolar");
            Console.WriteLine(price.SelectSingleNode("div"));
            foreach (var nodo in price.SelectSingleNode("div").Descendants())
            {
                if (nodo.Name == "strong")
                {
                    this.precio_dolar = Math.Round(Decimal.Parse(nodo.InnerText), 2);
                    Console.WriteLine(this.precio_dolar);
                }
            }
            this.dolarPrice.Content = this.precio_dolar.ToString() + " Bs";

        }

        private void dolars_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal current_dolars = 0;
            decimal bs_result = 0;
            
            bool parsed = Decimal.TryParse(this.dolars.Text, out current_dolars);
            if (parsed)
            {
                bs_result = current_dolars * this.precio_dolar;
                this.bsConversion.Text = Decimal.Round(bs_result, 2).ToString();
            }
            else if(!(this.bsConversion == null))
            {
                this.bsConversion.Clear();
            }
            

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void bolivars_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal current_bs = 0;
            decimal dolar_result = 0;

            bool parsed = Decimal.TryParse(this.bolivars.Text, out current_bs);
            if (parsed)
            {
                dolar_result = current_bs / this.precio_dolar;
                this.dolarConversion.Text = Decimal.Round(dolar_result, 2).ToString();
            }
            else if (!(this.dolarConversion == null))
            {
                this.dolarConversion.Clear();
            }
        }
    }
}
