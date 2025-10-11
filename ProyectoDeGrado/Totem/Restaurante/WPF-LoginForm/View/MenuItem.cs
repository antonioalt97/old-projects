using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF_LoginForm.View
{
    public class MenuItem
    {
        public int IdMenu { get; set; }
        public string CodigoMenu { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Disponible { get; set; }
        public int TipoMenuId { get; set; }
        public ImageSource Imagen { get; set; } // Propiedad para almacenar la imagen
    }
}
