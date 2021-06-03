using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace quanlykho.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }

        public ICommand SuplierCommand { get; set; }
        // moi thu xu ly se nam trong nay
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsLoaded = true;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            });

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {   
                UnitWindow unitWindow = new UnitWindow();
                unitWindow.ShowDialog();
            });

            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SuplierWindow unitWindow = new SuplierWindow();
                unitWindow.ShowDialog();
            });
        }
    }
}