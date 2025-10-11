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
using Oracle.ManagedDataAccess.Client;
namespace WPF_LoginForm.View
{
    /// <summary>
    /// Lógica de interacción para MesaView.xaml
    /// </summary>
    public partial class MesaView : UserControl
    {


       
        public MesaView()
        {
            InitializeComponent();
        }

        private void btnReserva_Click(object sender, RoutedEventArgs e)
        {
            ReservaView reservaView = new ReservaView();

            // Crear una ventana emergente y establecer su contenido como el UserControl de ReservaView
            Window popupWindow = new Window
            {
                Content = reservaView,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.ToolWindow,
                ResizeMode = ResizeMode.NoResize,
                Title = "Reserva",
            };

            // Mostrar la ventana emergente de ReservaView
            popupWindow.ShowDialog();
        }


        private void btnAsignar_Click(object sender, RoutedEventArgs e)
        { // Obtén la capacidad ingresada por el usuario desde el TextBox
            int capacidadCliente;
            if (int.TryParse(txtMesaPara.Text, out capacidadCliente))
            {
                try
                {
                    // Define la cadena de conexión a Oracle
                    string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123"; // Reemplaza con la cadena de conexión correcta

                    // Crea una conexión a Oracle
                    using (OracleConnection connection = new OracleConnection(connectionString))
                    {
                        connection.Open();

                        // Crea un comando para ejecutar el procedimiento almacenado
                        using (OracleCommand command = new OracleCommand("AsignarMesaCliente", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;

                            // Define y asigna los parámetros del procedimiento
                            command.Parameters.Add("p_cliente_id", OracleDbType.Int32).Value = 5; // Reemplaza con el ID del cliente
                            command.Parameters.Add("p_cantidad_personas", OracleDbType.Int32).Value = capacidadCliente;

                            // Ejecuta el procedimiento almacenado
                            command.ExecuteNonQuery();

                            // Llama a una función para obtener el número de mesa asignada
                            int numeroMesaAsignada = ObtenerNumeroMesaAsignada();

                            // Muestra el número de mesa asignada en lblNumeroMesa
                            lblNumeroMesa.Content = numeroMesaAsignada.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ingresa una capacidad válida para el cliente.");
            }

        }

        private int ObtenerNumeroMesaAsignada()
        {
            
            
                int numeroMesaAsignada = -1; // Valor predeterminado en caso de que no se encuentre ninguna mesa asignada

                try
                {
                    // Define la cadena de conexión a Oracle
                    string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123"; // Reemplaza con la cadena de conexión correcta

                    // Crea una conexión a Oracle
                    using (OracleConnection connection = new OracleConnection(connectionString))
                    {
                        connection.Open();

                        // Crea un comando para ejecutar la consulta que recupera el número de mesa asignada
                        using (OracleCommand command = new OracleCommand("SELECT numero_mesa FROM mesa WHERE en_uso =1", connection))
                        {
                            // Define y asigna el parámetro del cliente ID
                           // command.Parameters.Add("p_cliente_id", OracleDbType.NVarchar2).Value = 's'; // Reemplaza con el ID del cliente

                            // Ejecuta la consulta y obtén el resultado
                            using (OracleDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Si se encuentra un registro, obtén el número de mesa asignada
                                    numeroMesaAsignada = reader.GetInt32(0);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Maneja cualquier excepción que pueda ocurrir durante la consulta
                    MessageBox.Show("Ocurrió un error al obtener el número de mesa asignada: " + ex.Message);
                }

                return numeroMesaAsignada;
            }

        }
    }

