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
using System.Windows.Shapes;

namespace CreadorModulos
{
    /// <summary>
    /// Interaction logic for Preguntas.xaml
    /// </summary>
    public partial class Preguntas : Window
    {
        DataBase db = new DataBase();
        DataTable dt = new DataTable();
        int idModulo, idPregunta;
        int indexSelected;
        

        public Preguntas(int _idModulo)
        {
            InitializeComponent();
            btActualizar.Visibility = Visibility.Hidden;
             
            this.idModulo = _idModulo;

            dt = db.Consultar("SELECT * FROM InformationModuleQuestion WHERE fk_module = "+idModulo);

            for (int i= 0; i<dt.Rows.Count; i++)
            {
                myList.Items.Add(new { idModule = dt.Rows[i][0].ToString(), Txt = dt.Rows[i][2].ToString() });
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           

            idPregunta = int.Parse(myList.SelectedItem.ToString().Split('=')[1].Split(',')[0].Trim());
            Respuestas res = new Respuestas(idPregunta);
            res.ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (tbPregunta.Text != "")
            {
                db.EjecutarConsultar("INSERT INTO InformationModuleQuestion (fk_module, question) VALUES ("+idModulo+",'"+tbPregunta.Text+"')");
                idPregunta = db.LastID("InformationModuleQuestion");
                myList.Items.Add(new { idModule = idPregunta, Txt = tbPregunta.Text });
                tbPregunta.Text = "";

            }
            else
            {
                MessageBox.Show("Debe ingresar una pregunta", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btModificar_Click(object sender, RoutedEventArgs e)
        {
            if (myList.SelectedIndex != -1)
            {
                indexSelected = int.Parse(myList.SelectedItem.ToString().Split(',')[0].Split('=')[1]);
                tbPregunta.Text = myList.SelectedItem.ToString().Split('=').Last().Split('}')[0];
                btActualizar.Visibility = Visibility.Visible;
                btAgregar.Visibility = Visibility.Hidden;
            }
        }

        private void btActualizar_Click(object sender, RoutedEventArgs e)
        {
            db.EjecutarConsultar("UPDATE InformationModuleQuestion SET question ='"+tbPregunta.Text.Trim()+"' WHERE id = "+indexSelected);

            myList.Items.Clear();
            dt = db.Consultar("SELECT * FROM InformationModuleQuestion WHERE fk_module = " + idModulo);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                myList.Items.Add(new { idModule = dt.Rows[i][0].ToString(), Txt = dt.Rows[i][2].ToString() });
            }

            tbPregunta.Text = "";
            btActualizar.Visibility = Visibility.Hidden;
            btAgregar.Visibility = Visibility.Visible;
        }

        private void myList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btActualizar.Visibility = Visibility.Hidden;
            btAgregar.Visibility = Visibility.Visible;
            tbPregunta.Text = "";
        }

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            indexSelected = int.Parse(myList.SelectedItem.ToString().Split(',')[0].Split('=')[1]);
            db.EjecutarConsultar("DELETE FROM InformationModuleQuestion WHERE id = " + indexSelected);
            myList.Items.RemoveAt(myList.SelectedIndex);
        }

        private void btTerminar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
