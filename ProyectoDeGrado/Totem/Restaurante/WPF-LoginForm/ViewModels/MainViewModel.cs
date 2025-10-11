using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_LoginForm.Model;
using WPF_LoginForm.View;

namespace WPF_LoginForm.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields

        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private IUserRepository userRepository;
        //Properties

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        //--> Commands

        public ICommand ShowBancoViewCommand { get; }
        public ICommand ShowClienteViewCommand { get; }
        public ICommand ShowMesaViewCommand { get; }
        public ICommand ShowPedidoViewCommand { get; }
        public ICommand ShowCajaViewCommand {  get; }

        public ICommand ShowCuentaViewCommand { get; }
        public ICommand ShowEquipoViewCommand { get; }
        public ICommand ShowVisitaViewCommand { get; }
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }

        public ICommand ShowPedidosCheffCommand { get; }
        public MainViewModel()
        {

            //Initialize commands

            ShowVisitaViewCommand = new ViewModelCommand(ExecuteVisitaViewCommand);
            ShowEquipoViewCommand = new ViewModelCommand(ExecuteEquipoViewCommand);
            ShowCuentaViewCommand = new ViewModelCommand(ExecuteCuentaViewCommand);
            ShowCajaViewCommand = new ViewModelCommand(ExecuteCajaViewCommand);
            ShowPedidoViewCommand = new ViewModelCommand(ExecutePedidoViewCommand);
            ShowMesaViewCommand = new ViewModelCommand(ExecuteMesaViewCommand);
            ShowClienteViewCommand = new ViewModelCommand(ExecuteShowClienteViewCommand);
            ShowPedidosCheffCommand = new ViewModelCommand(ExecutePedidosCheffCommand);
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);

            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);

            ShowBancoViewCommand = new ViewModelCommand(ExecuteShowBancoViewCommand);
            //Default view
            ExecuteShowHomeViewCommand(null);

        }

        private void ExecutePedidosCheffCommand(object obj)
        {
            CurrentChildView = new PedidosCheffViewModel();
            Caption = "Pedido";
            Icon = IconChar.FirstOrder;
        }

        private void ExecuteMesaViewCommand(object obj)
        {
            CurrentChildView = new MesaViewModel();
            Caption = "Mesa";
            Icon = IconChar.Table;
        }

        private void ExecutePedidoViewCommand(object obj)
        {
            CurrentChildView = new PedidoViewModel();
            Caption = "Pedido";
            Icon = IconChar.Home;
        }

        private void ExecuteCajaViewCommand(object obj)
        {
            CurrentChildView = new CajaViewModel();
            Caption = "Cajero";
            Icon = IconChar.CashRegister;
        }

        private void ExecuteVisitaViewCommand(object obj)
        {
            CurrentChildView = new VisitaViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void ExecuteEquipoViewCommand(object obj)
        {
            CurrentChildView = new EquipoViewModel();
            Caption = "Equipo";
            Icon = IconChar.Home;
        }

        private void ExecuteCuentaViewCommand(object obj)
        {
            CurrentChildView = new CuentaViewModel();
            Caption = "Cuenta";
            Icon = IconChar.Home;
        }

        private void ExecuteShowClienteViewCommand(object obj)
        {
            CurrentChildView = new ClienteViewModel();
            Caption = "Cliente";
            Icon = IconChar.Home;
        }

        private void ExecuteShowBancoViewCommand(object obj)
        {
            CurrentChildView = new BancoViewModel();
            Caption = "Banco";
            Icon = IconChar.Home;
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {

            Caption = "Customers";
            Icon = IconChar.UserGroup;
        }
        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }
    }

}
