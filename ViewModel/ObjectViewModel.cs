using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Object = QuanLyKho.Model.Object;

namespace QuanLyKho.ViewModel
{
    public class ObjectViewModel : BaseViewModel
    {
        private ObservableCollection<Object> _ObjectList;
        public ObservableCollection<Object> ObjectList { get => _ObjectList; set { _ObjectList = value; OnPropertyChanged(); } }

        private ObservableCollection<Unit> _UnitList;
        public ObservableCollection<Unit> UnitList { get => _UnitList; set { _UnitList = value; OnPropertyChanged(); } }

        private ObservableCollection<Suplier> _SuplierList;
        public ObservableCollection<Suplier> SuplierList { get => _SuplierList; set { _SuplierList = value; OnPropertyChanged(); } }

        private Object _SelectedItem;
        public Object SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    QRCode = SelectedItem.QRCode;
                    BarCode = SelectedItem.BarCode;
                    SelectedUnit = SelectedItem.Unit;
                    SelectedSuplier = SelectedItem.Suplier;
                }
            }
        }

        private Unit _SelectedUnit;
        public Unit SelectedUnit
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
            }
        }

        private Suplier _SelectedSuplier;
        public Suplier SelectedSuplier
        {
            get => _SelectedSuplier;
            set
            {
                _SelectedSuplier = value;
                OnPropertyChanged();

            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _QRCode;
        public string QRCode { get => _QRCode; set { _QRCode = value; OnPropertyChanged(); } }

        private string _BarCode;
        public string BarCode { get => _BarCode; set { _BarCode = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _MoreInfo;
        public string MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }

        private DateTime? _ContractDate;
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }

        public ObjectViewModel()
        {
            ObjectList = new ObservableCollection<Object>(DataProvider.Ins.DB.Objects.ToList());
            UnitList = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units.ToList());
            SuplierList = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers.ToList());
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSuplier == null || SelectedUnit == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                var Object = new Object() { DisplayName = DisplayName, QRCode = QRCode, BarCode = BarCode, IdSuplier = SelectedSuplier.Id, IdUnit = SelectedUnit.Id, Id = Guid.NewGuid().ToString() };
                DataProvider.Ins.DB.Objects.Add(Object);
                DataProvider.Ins.DB.SaveChanges();
                ObjectList.Add(Object);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedSuplier == null || SelectedUnit == null)
                {
                    return false;
                }
                var isExistsObject = DataProvider.Ins.DB.Objects.Any(x => x.Id == SelectedItem.Id);
                if (!isExistsObject)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                var Object = DataProvider.Ins.DB.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Object.DisplayName = DisplayName;
                Object.QRCode = QRCode;
                Object.BarCode = BarCode;
                Object.IdSuplier = SelectedSuplier.Id;
                Object.IdUnit = SelectedUnit.Id;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DisplayName = DisplayName;
            });
        }
    }
}
