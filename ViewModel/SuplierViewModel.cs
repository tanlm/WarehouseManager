using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class SuplierViewModel : BaseViewModel
    {
        private ObservableCollection<Suplier> _SuplierList;
        public ObservableCollection<Suplier> SuplierList { get => _SuplierList; set { _SuplierList = value; OnPropertyChanged(); } }

        private Suplier _SelectedItem;
        public Suplier SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    Address = SelectedItem.Address;
                    MoreInfo = SelectedItem.MoreInfo;
                    ContractDate = SelectedItem.ContractDate;
                }
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _MoreInfo;
        public string MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }

        private DateTime? _ContractDate;
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }

        public SuplierViewModel()
        {
            SuplierList = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers.ToList());

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var Suplier = new Suplier() { DisplayName = DisplayName, Address = Address, Email = Email, ContractDate = ContractDate, Phone = Phone, MoreInfo = MoreInfo };
                DataProvider.Ins.DB.Supliers.Add(Suplier);
                DataProvider.Ins.DB.SaveChanges();
                SuplierList.Add(Suplier);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                var isExistsSuplier = DataProvider.Ins.DB.Supliers.Any(x => x.Id == SelectedItem.Id);
                if (!isExistsSuplier)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                var Suplier = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Suplier.DisplayName = DisplayName;
                Suplier.DisplayName = DisplayName; 
                Suplier.Address = Address; 
                Suplier.Email = Email;
                Suplier.ContractDate = ContractDate; 
                Suplier.Phone = Phone; 
                Suplier.MoreInfo = MoreInfo;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DisplayName = DisplayName;
            });
        }
    }
}
