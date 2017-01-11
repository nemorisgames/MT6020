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
using System.Data;

namespace CreadorModulos
{
    /// <summary>
    /// Interaction logic for CrearModulo.xaml
    /// </summary>
    public partial class CrearModulo : Window
    {
        DataBase db = new DataBase();
        DataBase db2 = new DataBase();
        DataTable dt = new DataTable();
        ComboBoxItem item = new ComboBoxItem();
        List<Modulo> cbm = new List<Modulo>();
        private int idModuleType, idModule;
        private string nombreTipo, nombreTipo2, nombreTipo3, seleccionado;

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lvLista.Items.Count != 0)
            {
                var result = MessageBox.Show("¿Está seguro que desea eliminar este módulo?. Todos los datos asociados se perderán.","Mensaje", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    int idModulo = int.Parse(lvLista.SelectedItem.ToString().Split(',')[0].Split('=')[1].ToString());
                    db.EjecutarConsultar("DELETE FROM Module WHERE id = "+idModulo);
                    lvLista.Items.RemoveAt(lvLista.SelectedIndex);
                    
                }
            }
        }

        public CrearModulo()
        {
            InitializeComponent();

            dt = db.Consultar("SELECT * FROM ModuleType");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbm.Add(new Modulo(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString()));
                if (i == 0)
                    nombreTipo = dt.Rows[i][1].ToString();
                if (i == 1)
                    nombreTipo2 = dt.Rows[i][1].ToString();
                if (i == 2)
                    nombreTipo3 = dt.Rows[i][1].ToString();


            }
            cbSeleccioneTipo.SelectedValuePath = "_Key";
            cbSeleccioneTipo.DisplayMemberPath = "_Value";
            cbSeleccioneTipo.ItemsSource = cbm;

            dt = db.Consultar("SELECT * FROM Module Order By id");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == "1")
                    seleccionado = nombreTipo;
                if (dt.Rows[i][1].ToString() == "2")
                    seleccionado= nombreTipo2;
                if (dt.Rows[i][1].ToString() == "3")
                    seleccionado = nombreTipo3;

                lvLista.Items.Add(new { ID = dt.Rows[i][0].ToString(), Nombre = dt.Rows[i][2].ToString(), Tipo = seleccionado});

            }

        }

        private void btCrearModulo_Click(object sender, RoutedEventArgs e)
        {
            if (cbSeleccioneTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe elegir un tipo de módulo", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                Modulo m;
                m = (Modulo)(cbSeleccioneTipo.SelectedItem);
                int.TryParse(m._Key, out idModuleType);
              
                //Crar nuevo módulo en la BD
                //MessageBox.Show(m._Key + m._Value);

                if (txtNombre.Text != "")
                {
                    dt = db.Consultar("SELECT name FROM Module WHERE name = '" + txtNombre.Text.Trim() + "'");
                    if (dt.Rows.Count == 0)
                    {
                        db.EjecutarConsultar("INSERT INTO Module (fk_moduleType, name) VALUES ("+idModuleType+",'"+txtNombre.Text.Trim()+"')");
                        idModule = db.LastID("Module");
                        lvLista.Items.Add(new { ID = idModule, Nombre = txtNombre.Text.Trim(), Tipo = m._Value});
                        txtNombre.Text = "";
                        
                    }
                    else
                    {
                        MessageBox.Show("El nombre del módulo ya existe", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }


                }
                else
                {
                    MessageBox.Show("Debe ingresar un nombre", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }


        private void lvLista_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           int idModulo = int.Parse(lvLista.SelectedItem.ToString().Split(',')[0].Split('=')[1].ToString());
            Diapositiva dp = new Diapositiva(idModulo);
            dp.ShowDialog();
                

        }
    }
}
