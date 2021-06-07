using QuanLyKho.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unit> _UnitList;
        public ObservableCollection<Unit> UnitList { get => _UnitList; set { _UnitList = value; OnPropertyChanged(); } }

        private Unit _SelectedItem;
        public Unit SelectedItem { get => _SelectedItem; 
            set {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null) 
                { 
                    DislayName = SelectedItem.DisplayName; 
                } 
            } 
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        private string _DisplayName;
        public string DislayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        public UnitViewModel()
        {
            UnitList = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units.ToList());

            AddCommand = new RelayCommand<object>((p) => {
                if (string.IsNullOrEmpty(DislayName))
                {
                    return false;
                }
                var isExistsUnit = DataProvider.Ins.DB.Units.Any(x => x.DisplayName == DislayName);
                if(isExistsUnit)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) => {
                var unit = new Unit() { DisplayName = DislayName };
                DataProvider.Ins.DB.Units.Add(unit);
                DataProvider.Ins.DB.SaveChanges();
                UnitList.Add(unit);
            });

            EditCommand = new RelayCommand<object>((p) => {
                if (string.IsNullOrEmpty(DislayName) || SelectedItem == null)
                {
                    return false;
                }
                var isExistsUnit = DataProvider.Ins.DB.Units.Any(x => x.DisplayName == DislayName);
                if (isExistsUnit)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) => {
                var unit = DataProvider.Ins.DB.Units.Where(x=>x.Id == SelectedItem.Id).SingleOrDefault();
                unit.DisplayName = DislayName;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DisplayName = DislayName;
                for (int i = 0; i < UnitList.Count(); i++)
                {
                    if (UnitList[i].Id == SelectedItem.Id)
                    {
                        UnitList[i] = new Unit() { Id = SelectedItem.Id, DisplayName = SelectedItem.DisplayName };
                        break;
                    }
                }
            });
        }
    }
}
