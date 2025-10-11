using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WPF_LoginForm.View;

namespace WPF_LoginForm.ViewModels
{
  public class MenuBebidaViewModel
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public MenuBebidaViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string connectionString = "DATA SOURCE=localhost:1521/orcl; USER ID=RESTAURANTEE;PASSWORD=123";



                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();



                    using (OracleCommand command = new OracleCommand("SELECT * FROM menu where disponible=1 AND codigo_menu='Menu2023-3' ORDER BY id_menu ASC", connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();

                            while (reader.Read())
                            {
                                MenuItem menuItem = new MenuItem
                                {
                                    IdMenu = reader.GetInt32(reader.GetOrdinal("id_menu")),
                                    CodigoMenu = reader.GetString(reader.GetOrdinal("codigo_menu")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("precio")),
                                    Disponible = reader.GetInt32(reader.GetOrdinal("disponible")),
                                    TipoMenuId = reader.GetInt32(reader.GetOrdinal("tipo_menu_id_tipo_menu")),
                                };

                                // Convertir byte[] a BitmapImage
                                byte[] imagenBytes = reader["foto"] as byte[];
                                if (imagenBytes != null && imagenBytes.Length > 0)
                                {
                                    BitmapImage imagen = new BitmapImage();
                                    imagen.BeginInit();
                                    imagen.CacheOption = BitmapCacheOption.OnLoad;
                                    imagen.StreamSource = new MemoryStream(imagenBytes);
                                    imagen.EndInit();

                                    menuItem.Imagen = imagen;
                                }

                                menuItems.Add(menuItem);
                            }

                            MenuItems = new ObservableCollection<MenuItem>(menuItems);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
