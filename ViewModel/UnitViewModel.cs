using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unit> _UnitList;
        public ObservableCollection<Unit> UnitList { get => _UnitList; set { _UnitList = value; OnPropertyChanged(); } }

        public UnitViewModel()
        {
            UnitList = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units.ToList());
        }
    }
}
