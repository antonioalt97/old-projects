using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;

using System.Diagnostics;
using System.Collections.ObjectModel;
using WPF_LoginForm.ViewModels;
using Acr.UserDialogs;
using System.Windows.Threading;

namespace WPF_LoginForm.View
{
    /// <summary>
    /// Lógica de interacción para PedidoView.xaml
    /// </summary>
    public partial class PedidoView : UserControl
    {
        private ObservableCollection<InfoMenu> pedidoItems = new ObservableCollection<InfoMenu>();

        public PedidoView()
        {
            InitializeComponent();
            //LlenarComboBoxMenu();
           
            cbMenu1.ItemsSource = new List<string> { "Todo", "Comidas", "Bebidas", "Postres", "Ensaladas", "Vegana" };

            cbPagar.ItemsSource = new List<string> { "Caja","MercadoPago" };
        }

        private void EliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is InfoMenu menu)
            {
                EliminarDetallePedido(menu.Nombre, menu.Cantidad);
                pedidoItems.Remove(menu);
               
                ActualizarDataGrid();


            }
        }
        private void EliminarDetallePedido(string descripcion, int cantidad)
        {
            try
            {
                // Configurar la cadena de conexión
                string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

                // Crear una conexión Oracle
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Crear un comando OracleCommand para llamar al procedimiento almacenado
                    using (OracleCommand command = new OracleCommand("ActualizarEstadoDetallePedido", connection))
                    {
                        // Especificar que es un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros al procedimiento almacenado
                        command.Parameters.Add(new OracleParameter("p_descripcion", OracleDbType.Varchar2)).Value = descripcion;
                        command.Parameters.Add(new OracleParameter("p_cantidad", OracleDbType.Int32)).Value = cantidad;

                        // Ejecutar el procedimiento almacenado
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al llamar al procedimiento almacenado: {ex.Message}");
            }
        }

        private void LlenarComboBoxMenu()
        {

            string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ID_PRODUCTO, NOMBRE, PRECIO_VENTA FROM producto"; // Reemplaza con tus detalles
                using (OracleDataAdapter adapter = new OracleDataAdapter(query, connection))
                {
                    // Crear un DataTable para almacenar los resultados
                    DataTable dataTable = new DataTable();

                    // Llenar el DataTable con los resultados de la consulta
                    adapter.Fill(dataTable);

                    // Asignar el DataTable como origen de datos del ComboBox
                   // cbMenu.ItemsSource = dataTable.DefaultView;

                    // Definir qué columna se mostrará en el ComboBox
                  //  cbMenu.DisplayMemberPath = "NOMBRE";

                    // Definir qué valor se utilizará al seleccionar un elemento del ComboBox
                   // cbMenu.SelectedValuePath = "ID_PRODUCTO";
                }
            }
        }
        private void ActualizarDataGrid()
        {
            // Asignar la colección actualizada al ItemsSource del DataGrid
            dgCaja.ItemsSource = pedidoItems;

            // Actualizar el total
            ActualizarTotal();

        }

        private void ActualizarTotal()
        {
            // Calcular el total sumando los subtotales de todos los elementos en pedidoItems
            decimal total = pedidoItems.Sum(item => item.Subtotal);

            // Actualizar el contenido de lblTotal con el nuevo total
            lblTotal.Content = $"$ {total}";
        }
        public Tuple<decimal, string> ObtenerPrecioYNombrePorId(int idMenu)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection("DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123"))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("SELECT precio, nombre FROM menu WHERE id_menu = :idMenu", connection))
                    {
                        command.Parameters.Add(new OracleParameter(":idMenu", idMenu));

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal precio = reader.GetDecimal(reader.GetOrdinal("precio"));
                                string nombre = reader.GetString(reader.GetOrdinal("nombre"));

                                return new Tuple<decimal, string>(precio, nombre);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al obtener información del menú: {ex.Message}");
            }

            return null;
        }


        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtIdMenu.Text, out int idMenu) && int.TryParse(txtCantidad.Text, out int cantidad))
            {
                var precioYNombre = ObtenerPrecioYNombrePorId(idMenu);

                if (precioYNombre != null)
                {
                    decimal precio = precioYNombre.Item1;
                    string nombre = precioYNombre.Item2;
                    InfoMenu menu = new InfoMenu
                    {
                        Nombre = nombre,
                        Precio = precio,
                        Cantidad = cantidad,
                        Subtotal = precio * cantidad
                    };
                    int subtotall = (int)menu.Subtotal;
                    pedidoItems.Add(menu);
                    ActualizarDataGrid();
                    string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

                    using (OracleConnection connection = new OracleConnection(connectionString))
                    {
                        connection.Open();

                        using (OracleCommand cmd = new OracleCommand("INSERTARPEDIDO", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Agregar los parámetros del procedimiento almacenado



                            cmd.Parameters.Add("p_fecha", OracleDbType.Date).Value = DateTime.Now;

                                cmd.Parameters.Add("p_total", OracleDbType.Decimal).Value = subtotall;
                            
                            cmd.Parameters.Add("p_propina", OracleDbType.Decimal).Value = 200;


                            // Ejecutar el procedimiento almacenado
                            cmd.ExecuteNonQuery();


                        }

                        using (OracleCommand cmd = new OracleCommand("INSERTARDETALLEPEDIDO", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Agregar los parámetros del procedimiento almacenado

                            cmd.Parameters.Add("p_descripcion", OracleDbType.Varchar2, 100).Value = nombre;
                            //cmd.Parameters.Add("p_pedido_id_pedido", OracleDbType.Int32).Value =
                            cmd.Parameters.Add("p_receta_id_receta", OracleDbType.Int32).Value = 2;
                          
                                // La conversión fue exitosa, ahora puedes usar la variable 'cantidad'
                                cmd.Parameters.Add("p_cantidad", OracleDbType.Int32).Value = cantidad;
                            
                            cmd.Parameters.Add("p_producto_id_producto", OracleDbType.Int32).Value = 3;
                            cmd.Parameters.Add("p_u_medida_id_u_medida", OracleDbType.Int32).Value = 2;
                            cmd.Parameters.Add("p_tipo_id", OracleDbType.Int32).Value = 3;
                            cmd.Parameters.Add("p_est_pedido_id_est_pedido", OracleDbType.Int32).Value = 1;

                            // Ejecutar el procedimiento almacenado
                            cmd.ExecuteNonQuery();


                        }



                    }
                   
                   
                }
                else
                {
                    MessageBox.Show("Menú no encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor numérico para el ID del menú.");
            }


         
        }

        private void CargarDatos()
        {
            string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT dp.pedido_id_pedido AS ID_PEDIDO, p.total AS PRECIO, dp.cantidad, PR.NOMBRE AS NOMBRE_PRODUCTO,(dp.cantidad * P.total) AS PRECIO_FINAL  FROM DETALLE_PEDIDO DP LEFT JOIN PEDIDO P ON DP.PEDIDO_ID_PEDIDO = P.ID_PEDIDO LEFT JOIN MESA MESA ON P.MESA_ID_MESA = MESA.ID_MESA LEFT JOIN PRODUCTO PR ON DP.PRODUCTO_ID_PRODUCTO = PR.ID_PRODUCTO WHERE DP.DESCRIPCION ='Nuevo pedido'";
                    OracleCommand cmd = new OracleCommand(sqlQuery, connection);
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgCaja.ItemsSource = dataTable.DefaultView;
                    dgCaja.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private InfoMenu ObtenerMenuDesdeBaseDeDatos(int idMenu)
        {
            try
            {
                string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand("SELECT * FROM menu WHERE id_menu = :idMenu", connection))
                    {
                        command.Parameters.Add("idMenu", OracleDbType.Int32).Value = idMenu;

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Crear un nuevo objeto MenuItem con los datos del lector
                                InfoMenu nuevoMenuItem = new InfoMenu
                                {

                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("precio")),
                              
                                    // Otras propiedades...
                                };

                                return nuevoMenuItem;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al obtener menú desde la base de datos: {ex.Message}");
            }
            return null;
        }


            private void btnVerMenu_Click(object sender, RoutedEventArgs e)
        {

            if (cbMenu1.SelectedItem == null)
            {
                MessageBox.Show("Seleccionar un Menu");
            }
            else
            {
                if (cbMenu1.SelectedItem.ToString() == "Todo")
                {

                    PopupMenuImagen popupMenu = new PopupMenuImagen();
                    Window popupWindow = new Window
                    {
                        Content = popupMenu,
                        SizeToContent = SizeToContent.WidthAndHeight,
                        WindowStyle = WindowStyle.ToolWindow,
                        ResizeMode = ResizeMode.NoResize,
                        Title = "Imagen del Menú",
                    };

                    popupWindow.ShowDialog();
                    return;
                }
                if (cbMenu1.SelectedItem.ToString() == "Comidas")
                {

                    PopupMenuComida popupMenu = new PopupMenuComida();
                    Window popupWindow = new Window
                    {
                        Content = popupMenu,
                        SizeToContent = SizeToContent.WidthAndHeight,
                        WindowStyle = WindowStyle.ToolWindow,
                        ResizeMode = ResizeMode.NoResize,
                        Title = "Imagen del Menú",
                    };

                    popupWindow.ShowDialog();
                    return;
                }
                if (cbMenu1.SelectedItem.ToString() == "Bebidas")
                {

                    PopupMenuBebidas popupMenu = new PopupMenuBebidas();
                    Window popupWindow = new Window
                    {
                        Content = popupMenu,
                        SizeToContent = SizeToContent.WidthAndHeight,
                        WindowStyle = WindowStyle.ToolWindow,
                        ResizeMode = ResizeMode.NoResize,
                        Title = "Imagen del Menú",
                    };

                    popupWindow.ShowDialog();
                    return;
                }

                if (cbMenu1.SelectedItem.ToString() == "Postres")
                {

                    PopupMenuPostre popupMenu = new PopupMenuPostre();
                    Window popupWindow = new Window
                    {
                        Content = popupMenu,
                        SizeToContent = SizeToContent.WidthAndHeight,
                        WindowStyle = WindowStyle.ToolWindow,
                        ResizeMode = ResizeMode.NoResize,
                        Title = "Imagen del Menú",
                    };

                    popupWindow.ShowDialog();
                    return;
                }
                if (cbMenu1.SelectedItem.ToString() == "Ensaladas")
                {

                    PopupMenuEnsalada popupMenu = new PopupMenuEnsalada();
                    Window popupWindow = new Window
                    {
                        Content = popupMenu,
                        SizeToContent = SizeToContent.WidthAndHeight,
                        WindowStyle = WindowStyle.ToolWindow,
                        ResizeMode = ResizeMode.NoResize,
                        Title = "Imagen del Menú",
                    };

                    popupWindow.ShowDialog();
                    return;
                }

                if (cbMenu1.SelectedItem.ToString() == "Vegana")
                {

                    PopupMenuVegana popupMenu = new PopupMenuVegana();
                    Window popupWindow = new Window
                    {
                        Content = popupMenu,
                        SizeToContent = SizeToContent.WidthAndHeight,
                        WindowStyle = WindowStyle.ToolWindow,
                        ResizeMode = ResizeMode.NoResize,
                        Title = "Imagen del Menú",
                    };

                    popupWindow.ShowDialog();
                    return;
                }
            }
           
        }

        private async void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCantidad.Text) || string.IsNullOrEmpty(txtIdMenu.Text))
            {
                MessageBox.Show("Seleccionar un Menu para realizar tu Pago");
                return;
            }else
            {
                if (cbPagar.SelectedItem.ToString() == "Caja")
                {
                    string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

                    // Crear una conexión Oracle
                    using (OracleConnection connection = new OracleConnection(connectionString))
                    {
                        connection.Open();

                        // Crear un comando OracleCommand para llamar al procedimiento almacenado
                        using (OracleCommand command = new OracleCommand("InsertarPago", connection))
                        {
                            // Especificar que es un procedimiento almacenado
                            command.CommandType = CommandType.StoredProcedure;

                            // Agregar parámetros al procedimiento almacenado
                           // command.Parameters.Add(new OracleParameter("p_id_pago", OracleDbType.Int32)).Value = ObtenerNuevoIdPago(); // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_fecha_pago", OracleDbType.Date)).Value = DateTime.Now; // Puedes ajustar la fecha según sea necesario
                            command.Parameters.Add(new OracleParameter("p_url", OracleDbType.Varchar2, 500)).Value = "www.CajeroGod.cl"; // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_token", OracleDbType.Varchar2, 500)).Value = "ASKLDJKLASasjkdjaskSD4"; // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_orden_pago", OracleDbType.Int32)).Value = 1; // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_tipo_de_pago_id_tipo_pago", OracleDbType.Int32)).Value = 3; // Ajusta según sea necesario
                           // command.Parameters.Add(new OracleParameter("p_pedido_id_pedido", OracleDbType.Int32)).Value = ObtenerIdPedido(); // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_cuotas", OracleDbType.Int32)).Value = 1; // Ajusta según sea necesario

                            // Ejecutar el procedimiento almacenado
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Gracias por Preferirnos, Pagar en Caja");
                }
                if (cbPagar.SelectedItem.ToString() == "MercadoPago")
                {
                    string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

                    // Crear una conexión Oracle
                    using (OracleConnection connection = new OracleConnection(connectionString))
                    {
                        connection.Open();

                        // Crear un comando OracleCommand para llamar al procedimiento almacenado
                        using (OracleCommand command = new OracleCommand("InsertarPago", connection))
                        {
                            // Especificar que es un procedimiento almacenado
                            command.CommandType = CommandType.StoredProcedure;

                            // Agregar parámetros al procedimiento almacenado
                            // command.Parameters.Add(new OracleParameter("p_id_pago", OracleDbType.Int32)).Value = ObtenerNuevoIdPago(); // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_fecha_pago", OracleDbType.Date)).Value = DateTime.Now; // Puedes ajustar la fecha según sea necesario
                            command.Parameters.Add(new OracleParameter("p_url", OracleDbType.Varchar2, 500)).Value = "www.mercadopago.cl"; // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_token", OracleDbType.Varchar2, 500)).Value = "TEST-7650912131212913-121016-b7669504450ec7d191e7f6f153bd8ac7-1585773925"; // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_orden_pago", OracleDbType.Int32)).Value = 1; // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_tipo_de_pago_id_tipo_pago", OracleDbType.Int32)).Value = 1; // Ajusta según sea necesario
                           // command.Parameters.Add(new OracleParameter("p_pedido_id_pedido", OracleDbType.Int32)).Value = ObtenerIdPedido(); // Ajusta según sea necesario
                            command.Parameters.Add(new OracleParameter("p_cuotas", OracleDbType.Int32)).Value = 1; // Ajusta según sea necesario

                            // Ejecutar el procedimiento almacenado
                            command.ExecuteNonQuery();
                        }
                    }
                    // Configurar las credenciales de Mercado Pago
                    MercadoPagoConfig.AccessToken = "TEST-7650912131212913-121016-b7669504450ec7d191e7f6f153bd8ac7-1585773925";
                    //"APP_USR-2614696993281853-111303-ffa7ae104b3dedef83f59ee22d06c87e-1545968131"
                    var request = new PreferenceRequest
                    {
                        Items = new List<PreferenceItemRequest>
    {
        new PreferenceItemRequest
        {
            Title = "Mi producto",
            Quantity = 1,
            CurrencyId = "CLP",
            UnitPrice = 75m,
        },
    },
                    };

                    // Crea la preferencia usando el client
                    var client = new PreferenceClient();
                    Preference preference = await client.CreateAsync(request);

                    // Obtener la URL de pago desde la preferencia
                    var urlPagoMercadoPago = preference.InitPoint;

                    // Abrir la URL en el navegador web predeterminado
                    Process.Start(new ProcessStartInfo(urlPagoMercadoPago) { UseShellExecute = true });




                    // Aquí puedes mostrar tu pantalla de carga
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(5);
                    timer.Tick += (senderr, ex) =>
                    {
                        // Ocultar la pantalla de carga
                        this.Visibility = Visibility.Collapsed;
                        timer.Stop();
                    };
                    // Mostrar la pantalla de carga y iniciar el temporizador
                    this.Visibility = Visibility.Visible;
                    timer.Start();
                }

            }

   

        }
    }

    
}
