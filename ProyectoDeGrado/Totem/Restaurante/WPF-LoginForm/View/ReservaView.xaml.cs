using Oracle.ManagedDataAccess.Client;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_LoginForm.View
{
    /// <summary>
    /// Lógica de interacción para ReservaView.xaml
    /// </summary>
    public partial class ReservaView : System.Windows.Controls.UserControl
    {
        public ReservaView()
        {
            InitializeComponent();
        }

        private void MostrarReservasEnTxtMensaje2()
        {
            string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTE;PASSWORD=123";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string sql = @"
            SELECT
    r.ID AS ReservaID,
    r.FECHA_RESERVA,
    r.NOMBRE AS ReservaNombre,
    r.APELLIDO AS ReservaApellido,
    r.CANTIDAD_COMENSALES,
    r.ESTADO_RESERVA,
    r.TIEMPO,
    r.COMENTARIO,
    r.MESAS,
    u.USERNAME AS Usuario,
    u.FIRST_NAME AS NombreUsuario,
    u.LAST_NAME AS ApellidoUsuario,
    u.EMAIL
FROM
    RESTAURANTE.RESERVA r
INNER JOIN
    RESTAURANTE.AUTH_USER u ON r.USER_ID = u.ID
    WHERE
                TRUNC(r.fecha_reserva) = TO_DATE('25-12-23', 'DD-MM-YY') ";

                using (OracleCommand command = new OracleCommand(sql, connection))
                {
                    // Utiliza un adaptador para llenar un DataTable con los resultados de la consulta
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Construir la cadena de mensaje personalizada
                            string mensaje = $"Este es tu código de reserva: {reader["ID"]}, tu fecha: {reader["fecha_reserva"]}, tu número de mesa: {reader["mesas"]}";

                            // Mostrar el mensaje en tu TextBox
                            txtMensaje.Text = mensaje;
                        }
                        else
                        {
                            txtMensaje.Text = "No hay reservas para la fecha especificada.";
                        }
                    }
                }
            }
        }

        private void MostrarReservasEnTxtMensaje()
        {
            string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string sql = @"
            SELECT
                r.codigo_reserva,
                r.fecha_hora,
                m.id_mesa,
                m.numero_mesa,
                u.nombre AS usuario_nombre,
                u.genero,
                u.rol_id_rol
            FROM
                reserva r
            JOIN
                mesa m ON r.mesa_id_mesa = m.id_mesa
            JOIN
                usuario u ON r.usuario_id_usuario = u.id_usuario
            WHERE
                TRUNC(r.fecha_hora) = TO_DATE('2023-12-14', 'YYYY-MM-DD')";

                using (OracleCommand command = new OracleCommand(sql, connection))
                {
                    // Utiliza un adaptador para llenar un DataTable con los resultados de la consulta
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Construir la cadena de mensaje personalizada
                            string mensaje = $"Este es tu código de reserva: {reader["codigo_reserva"]}, tu fecha: {reader["fecha_hora"]}, tu número de mesa: {reader["numero_mesa"]}";

                            // Mostrar el mensaje en tu TextBox
                            txtMensaje.Text = mensaje;
                        }
                        else
                        {
                            txtMensaje.Text = "No hay reservas para la fecha especificada.";
                        }
                    }
                }
            }
        }

        private string DataTableToString(DataTable dataTable)
        {
            // Convierte los datos del DataTable a una cadena legible
            string result = string.Join(Environment.NewLine,
                dataTable.AsEnumerable()
                         .Select(row => string.Join(", ", row.ItemArray)));

            return result;
        }

        private void BtnIngresar_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM usuario WHERE email = :correo AND contrasena = :contrasena";

                using (OracleCommand command = new OracleCommand(sql, connection))
                {
                    command.Parameters.Add(new OracleParameter("correo", OracleDbType.Varchar2)).Value = txtCorreo.Text;

                    // Acceder al texto dentro del TextBox para el parámetro de contraseña
                    command.Parameters.Add(new OracleParameter("contrasena", OracleDbType.Varchar2)).Value = txtContrasena.Text;

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        // Autenticación exitosa

                        MostrarReservasEnTxtMensaje();
                     
                    }


                    else
                    {
                        // Autenticación fallida
                        txtMensaje.Text = "Correo o contraseña incorrectos";
                    }
                }
            }
        }
    }
}
