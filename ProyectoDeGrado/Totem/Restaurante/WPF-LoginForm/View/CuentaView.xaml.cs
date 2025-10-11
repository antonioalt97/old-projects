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

namespace WPF_LoginForm.View
{
    /// <summary>
    /// Lógica de interacción para CuentaView.xaml
    /// </summary>
    public partial class CuentaView : UserControl
    {
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgCuenta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        //    OKCASAEntities db;
        //    public CuentaView()
        //    {
        //        InitializeComponent();
        //    }

        //    private void dgCuenta_Loaded(object sender, RoutedEventArgs e)
        //    {
        //        db = new OKCASAEntities();
        //        dgCuenta.ItemsSource = db.app_customuser.ToList();
        //    }

        //    private void btnAgregar_Click(object sender, RoutedEventArgs e)
        //    {
        //        app_customuser user = new app_customuser();
        //        // user.id = Convert.ToInt32(txtIdCuenta.Text);
        //        user.username = txtUsuario.Text;
        //        user.password = txtContra.Text;
        //        user.first_name = txtNombre.Text;
        //        user.last_name = txtApellido.Text;
        //        user.last_login = DateTime.Now;
        //        user.is_superuser = Convert.ToBoolean(txtTipo.Text);
        //        user.is_staff = Convert.ToBoolean(txtTipo.Text);
        //        user.is_active = Convert.ToBoolean(txtEstado.Text);
        //        user.date_joined = DateTime.Now;
        //        user.rut = txtRut.Text;
        //        user.direccion = txtDireccion.Text;
        //        user.telefono = txtTelefono.Text;
        //        db.app_customuser.Add(user);
        //        db.SaveChanges();
        //        MessageBox.Show("Cuenta Guardado");
        //        dgCuenta.ItemsSource = db.app_customuser.ToList();

        //    }

        //    private void btnEliminar_Click(object sender, RoutedEventArgs e)
        //    {
        //        int id = Convert.ToInt32(txtIdCuenta.Text);
        //        var aux = db.app_customuser.Where(w => w.id == id).FirstOrDefault();
        //        db.app_customuser.Remove(aux);
        //        db.SaveChanges();
        //        dgCuenta.ItemsSource = db.app_customuser.ToList();
        //        MessageBox.Show("Cuenta Eliminado");

        //    }

        //    private void btnModificar_Click(object sender, RoutedEventArgs e)
        //    {
        //        int id = Convert.ToInt32(txtIdCuenta.Text);
        //        var aux = db.app_customuser.Where(w => w.id == id).FirstOrDefault();
        //        aux.username = txtUsuario.Text;
        //        aux.password = txtContra.Text;
        //        aux.first_name = txtNombre.Text;
        //        aux.last_name = txtApellido.Text;
        //        aux.is_staff = Convert.ToBoolean(txtTipo.Text);
        //        aux.is_active = Convert.ToBoolean(txtEstado.Text);
        //        aux.email = txtCorreo.Text;
        //        aux.rut = txtRut.Text;
        //        aux.direccion = txtDireccion.Text;
        //        aux.telefono = txtTelefono.Text;
        //        db.SaveChanges();
        //        dgCuenta.ItemsSource = db.app_customuser.ToList();
        //        MessageBox.Show("Cuenta Modificado");


        //    }

        //    private void btnBuscar_Click(object sender, RoutedEventArgs e)
        //    {

        //        int id = Convert.ToInt32(txtIdCuenta.Text);
        //        var aux = db.app_customuser.Where(w => w.id == id).FirstOrDefault();
        //        txtUsuario.Text = aux.username;
        //        txtContra.Text = aux.password;
        //        txtNombre.Text = aux.first_name;
        //        txtApellido.Text = aux.last_name;
        //        txtTipo.Text = aux.is_staff.ToString();
        //        txtEstado.Text = aux.is_active.ToString();
        //        txtCorreo.Text = aux.email;
        //        txtRut.Text = aux.rut;
        //        txtDireccion.Text = aux.direccion;
        //        txtTelefono.Text = aux.telefono;
        //        MessageBox.Show("Cuenta Encontrada");
        //        btnModificar.IsEnabled = true;
        //    }
        //}
    }
}
