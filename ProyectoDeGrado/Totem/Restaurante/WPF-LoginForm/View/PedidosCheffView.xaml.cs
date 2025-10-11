using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para PedidosCheffView.xaml
    /// </summary>
    public partial class PedidosCheffView : UserControl
    {
        public PedidosCheffView()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT dp.pedido_id_pedido AS ID_PEDIDO, dp.cantidad, dp.DESCRIPCION AS NOMBRE, MESA.NUMERO_MESA, dp.est_pedido_id_est_pedido AS ESTADO FROM DETALLE_PEDIDO DP LEFT JOIN PEDIDO P ON DP.PEDIDO_ID_PEDIDO = P.ID_PEDIDO LEFT JOIN MESA MESA ON P.MESA_ID_MESA = MESA.ID_MESA LEFT JOIN PRODUCTO PR ON DP.PRODUCTO_ID_PRODUCTO = PR.ID_PRODUCTO WHERE dp.est_pedido_id_est_pedido = 1";
                    OracleCommand cmd = new OracleCommand(sqlQuery, connection);
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgCheff.ItemsSource = dataTable.DefaultView;
                    dgCheff.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    

       

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtIdPedido.Text, out int idPedido))
            {
                MessageBox.Show("El ID del pedido debe ser un número entero válido.", "Error de Validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Sale del evento si la validación falla
            }


            // Llama al procedimiento almacenado en la base de datos Oracle
            ModificarEstadoPedido(idPedido);
            CargarDatos();

            // Puedes agregar un mensaje de éxito o actualización en tu interfaz de usuario si lo deseas.
            MessageBox.Show("El estado del pedido ha sido modificado correctamente.");
        }

        private void ModificarEstadoPedido(int idPedido)
        {
            try
            {
                // Cadena de conexión a la base de datos Oracle
                string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Define y ejecuta tu procedimiento almacenado
                    using (OracleCommand cmd = new OracleCommand("ModificarEstadoPedido", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agrega los parámetros al procedimiento
                        cmd.Parameters.Add("p_pedido_id", OracleDbType.Int32).Value = idPedido;
                       

                        cmd.ExecuteNonQuery(); // Ejecuta el procedimiento
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el estado del pedido: " + ex.Message);
            }
        }

        private void CancelarEstadoPedido(int idPedido)
        {
            try
            {
                // Cadena de conexión a la base de datos Oracle
                string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Define y ejecuta tu procedimiento almacenado
                    using (OracleCommand cmd = new OracleCommand("CancelarEstadoPedido", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Agrega los parámetros al procedimiento
                        cmd.Parameters.Add("p_pedido_id", OracleDbType.Int32).Value = idPedido;


                        cmd.ExecuteNonQuery(); // Ejecuta el procedimiento
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cancelar el estado del pedido: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(txtIdPedido.Text, out int idPedido))
            {
                MessageBox.Show("El ID del pedido debe ser un número entero válido.", "Error de Validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Sale del evento si la validación falla
            }

          




            // Llama al procedimiento almacenado en la base de datos Oracle
          CancelarEstadoPedido(idPedido);
            CargarDatos();
            // Puedes agregar un mensaje de éxito o actualización en tu interfaz de usuario si lo deseas.
            MessageBox.Show("El estado del pedido ha sido modificado correctamente.");

        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }
    }
    }

