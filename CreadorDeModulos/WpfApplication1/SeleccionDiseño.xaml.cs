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
using System.Windows.Shapes;

namespace CreadorModulos
{
    /// <summary>
    /// Interaction logic for SeleccionDiseño.xaml
    /// </summary>
    public partial class SeleccionDiseño : Window
    {
        public string cuadrado;

        public SeleccionDiseño(string _cuadrado)
        {
            InitializeComponent();
            this.cuadrado = _cuadrado;
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
           
            
        }
    }
}
