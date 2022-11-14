using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympics.Controller;
using Olympics.Models;
namespace Olympics.ViewModels
{
	internal class MainWindowViewModel : BaseViewModel
	{
		public MainWindowViewModel()
        {
			ListaGames = Athletes.GetGames();
			ListaSport = Athletes.GetSport( String.Empty);//inizializzata vuota
			ListaEvent1 = Athletes.GetEvent1(String.Empty, String.Empty);//inizializzata vuota
			ListaMedal = Athletes.GetMedal();
			ListaSessi = Athletes.GetGenders();
        }
		//name
		private string _filtraName;

		public string FiltraName
		{
			get { return _filtraName; }
			set {
				_filtraName = value; NotifyPropertyChanged("FiltraName");  LoadData(); ;
				
			}
		}

		//sex

		private List<string> _listaSessi;

		public List<string> ListaSessi
		{
			get { return _listaSessi; }
			set { _listaSessi = value; NotifyPropertyChanged("ListaSessi"); }
		}

		private string _filtraSex;

		public string FiltraSex
		{
			get { return _filtraSex; }
			set { _filtraSex = value; NotifyPropertyChanged("FiltraSex"); LoadData(); }
		}

		//games


		private List<string> _listaGames;

		public List<string> ListaGames
		{
			get { return _listaGames; }
			set { _listaGames = value; NotifyPropertyChanged("ListaGames"); }
		}


		private string _filtraGames;

		public string FiltraGames
		{
			get { return _filtraGames; }
			set { _filtraGames = value; NotifyPropertyChanged("FiltraGames"); LoadData(); }
		}

		//sport

		private List<string> _listaSport;
		public List<string> ListaSport
		{
			get { return _listaSport; }
			set { _listaSport = value; NotifyPropertyChanged("ListaSport"); }
		}

		private string _filtraSport;

		public string FiltraSport
		{
			get { return _filtraSport; }
			set { _filtraSport = value; NotifyPropertyChanged("FiltraSport"); LoadData(); }
		}

		//event1

		private List<string> _listaEvent1;
		public List<string> ListaEvent1
		{
			get { return _listaEvent1; }
			set { _listaEvent1 = value; NotifyPropertyChanged("ListaEvent1"); }
		}

		private string _filtraEvent1;

		public string FiltraEvent1
		{
			get { return _filtraEvent1; }
			set { _filtraEvent1 = value; NotifyPropertyChanged("FiltraEvent1"); LoadData(); }
		}

		//medal

		private List<string> _listaMedal;
		public List<string> ListaMedal
		{
			get { return _listaMedal; }
			set { _listaMedal = value; NotifyPropertyChanged("ListaMedal");  }
		}

		private string _filtraMedal;

		public string FiltraMedal
		{
			get { return _filtraMedal; }
			set { _filtraMedal = value; NotifyPropertyChanged("FiltraMedal"); LoadData(); }
		}

		private List<Athlete> _datiAthlete;

		public List<Athlete> DatiAthlete
		{
			get { return _datiAthlete; }
			set { _datiAthlete = value; NotifyPropertyChanged("DatiAthlete"); }
		}

		public void AzzeraFiltri()
        {
			DatiAthlete = Athletes.FindAll(FiltraName="", FiltraSex = "", FiltraGames = "", FiltraSport = "", FiltraEvent1 = "", FiltraMedal = "");
			LoadData();

		}

		private void LoadData()
		{
			DatiAthlete.Clear();
			DatiAthlete = Athletes.FindAll(FiltraName, FiltraSex, FiltraGames, FiltraSport, FiltraEvent1, FiltraMedal);
		}

	}
}
