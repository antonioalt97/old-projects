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
    /// Lógica de interacción para ClienteView.xaml
 
    public partial class ClienteView : UserControl
    {
        
        
        
        /// <summary>

        /// <summary>
        /// OKCASAEntities db;
        /// </summary>
        public ClienteView()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgCliente_Loaded(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// private void dgCliente_Loaded(object sender, RoutedEventArgs e)
        /// {
        ///      db = new OKCASAEntities();
        ///     dgCliente.ItemsSource = db.api_cliente.ToList();
        ///  }

        //private void btnBuscar_Click(object sender, RoutedEventArgs e)
        ////{
        ////    int id = Convert.ToInt32(txtIdCliente.Text);
        ////    var aux = db.api_cliente.Where(w => w.id == id).FirstOrDefault();
        ////    txtNombreCliente.Text = aux.nombre;
        ////    txtApellidoCliente.Text = aux.apellido;
        ////    txtRut.Text = aux.rut;
        ////    txtHipotecario.Text = aux.es_hipotecario.ToString();
        ////    MessageBox.Show("Cliente Encontrado");
        ////    btnModificar.IsEnabled = true;
        //}

        //private void btnAgregar_Click(object sender, RoutedEventArgs e)
        //{

        //    //ServiceReference1.WebServiceOkcasaClient ws = new ServiceReference1.WebServiceOkcasaClient();
        //    //bool aux = ws.ExisteCliente2(txtRut.Text, Convert.ToInt32(txtIdCliente.Text));
        //    //if (aux == false)
        //    //{
        //    //    api_cliente cliente = new api_cliente();
        //    //    cliente.id = Convert.ToInt32(txtIdCliente.Text);
        //    //    cliente.nombre = txtNombreCliente.Text;
        //    //    cliente.apellido = txtApellidoCliente.Text;
        //    //    cliente.rut = txtRut.Text;
        //    //    cliente.es_hipotecario = Convert.ToBoolean(txtHipotecario.Text);

        //    //    db.api_cliente.Add(cliente);
        //    //    db.SaveChanges();
        //    //    MessageBox.Show("Cliente Guardado");
        //    //    dgCliente.ItemsSource = db.api_cliente.ToList();


        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("Cliente ya existe, porfavor ingresar nuevamente");
        //    //    txtNombreCliente.Text = "";
        //    //    txtApellidoCliente.Text = "";
        //    //    txtRut.Text = "";
        //    //    txtHipotecario.Text = "";

        //    //}

        //}

        //private void btnEliminar_Click(object sender, RoutedEventArgs e)
        //{
        //    //int id = Convert.ToInt32(txtIdCliente.Text);
        //    //var aux = db.api_cliente.Where(w => w.id == id).FirstOrDefault();
        //    //db.api_cliente.Remove(aux);
        //    //db.SaveChanges();
        //    //dgCliente.ItemsSource = db.api_cliente.ToList();
        //    //MessageBox.Show("Cliente Eliminado");

        //}

        //private void btnModificar_Click(object sender, RoutedEventArgs e)
        //{

        //    //int id = Convert.ToInt32(txtIdCliente.Text);
        //    //var aux = db.api_cliente.Where(w => w.id == id).FirstOrDefault();
        //    //aux.nombre = txtNombreCliente.Text;
        //    //aux.apellido = txtApellidoCliente.Text;
        //    //aux.rut = txtRut.Text;
        //    //aux.es_hipotecario = Convert.ToBoolean(txtHipotecario.Text);

        //    //db.SaveChanges();
        //    //dgCliente.ItemsSource = db.api_cliente.ToList();
        //    //MessageBox.Show("Cliente Modificado");
        //}

        //private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        //{

        //    //txtIdCliente = null;
        //    //txtNombreCliente.Text = "";
        //    //txtApellidoCliente.Text = "";
        //    //txtRut.Text = "";
        //    //txtHipotecario.Text = "";

        //}

        ///// </summary>
    }
}
