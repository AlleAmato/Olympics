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
using Olympics.ViewModels;
using Olympics.Controller;
using Olympics.Models;

namespace Olympics
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainWindowViewModel();
            DataContext = vm;
            vm.DatiAthlete=Athletes.GetAll();
            this.tab.ItemsSource = vm.DatiAthlete;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Alessandro Amato");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.FiltraName=this.tNome.Text;
            this.tab.ItemsSource = vm.DatiAthlete;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.tab.ItemsSource = null;
            
          vm.FiltraSex =this.CbSesso.SelectedItem.ToString();
            this.tab.ItemsSource = vm.DatiAthlete;
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            vm.ListaSport.Clear();
            vm.ListaEvent1.Clear();
            string GameUtente = this.CbGame.SelectedItem.ToString();
            vm.ListaSport= Athletes.GetSport(GameUtente);

            vm.FiltraGames = GameUtente;
            this.tab.ItemsSource = vm.DatiAthlete;
        }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            vm.ListaEvent1.Clear();
            string GameUtente = this.CbGame.SelectedItem.ToString();
            if (this.CbSport.SelectedItem != null)
            {
                string SportUtente = this.CbSport.SelectedItem.ToString();
                vm.ListaEvent1 = Athletes.GetEvent1(GameUtente, SportUtente);
                vm.FiltraSport = SportUtente;
                this.tab.ItemsSource = vm.DatiAthlete;
            }
        }

        private void ComboBox_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {
            vm.FiltraEvent1 = this.CbEvent.SelectedItem.ToString();
            this.tab.ItemsSource = vm.DatiAthlete;
        }

        private void ComboBox_SelectionChanged_4(object sender, SelectionChangedEventArgs e)
        {
            vm.FiltraMedal = this.CbMedal.SelectedItem.ToString();
            this.tab.ItemsSource = vm.DatiAthlete;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.AzzeraFiltri();
            vm.DatiAthlete = Athletes.GetAll();
            this.tab.ItemsSource = vm.DatiAthlete;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //PrimaPagina();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //UltimaPagina();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //PaginaPrecedente();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //PaginaSuccessiva();
        }
    }
}
