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
    /// Interaction logic for Respuestas.xaml
    /// </summary>
    public partial class Respuestas : Window
    {
        DataBase db = new DataBase();
        DataTable dt = new DataTable();
        int idPregunta, indexSelected;
        public Respuestas(int _idPregunta)
        {
            InitializeComponent();
            this.idPregunta = _idPregunta;

            dt = db.Consultar("SELECT * FROM InformationModuleAnswers WHERE fk_informationModuleQuestions = "+idPregunta);

            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i<dt.Rows.Count;i++)
                {
                    MyList.Items.Add(new { ID = dt.Rows[i][0].ToString(), Respuesta = dt.Rows[i][2].ToString(), Correcta = dt.Rows[i][3].ToString()});
                }
            }
        }

        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {
            MyList.Items.Add(new {Respuesta = tbRespuesta.Text, Correcta = ckCorrecta.IsChecked.ToString()});
            db.EjecutarConsultar("INSERT INTO InformationModuleAnswers (fk_informationModuleQuestions, text, correct) VALUES ("+idPregunta+",'"+ tbRespuesta.Text + "','"+ ckCorrecta.IsChecked.ToString() + "')");
        }

        private void btActualizar_Click(object sender, RoutedEventArgs e)
        {
            db.EjecutarConsultar("UPDATE InformationModuleAnswers SET text ='" + tbRespuesta.Text.Trim() + "', correct ='"+ ckCorrecta.IsChecked.ToString() + "' WHERE id = " + indexSelected);

            MyList.Items.Clear();
            dt = db.Consultar("SELECT * FROM InformationModuleAnswers WHERE fk_informationModuleQuestions = " + idPregunta);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MyList.Items.Add(new { ID = dt.Rows[i][0].ToString(), Respuesta = dt.Rows[i][2].ToString(), Correcta = dt.Rows[i][3].ToString() });
            }

            tbRespuesta.Text = "";
            btActualizar.Visibility = Visibility.Hidden;
            btAgregar.Visibility = Visibility.Visible;
            btModificar.Visibility = Visibility.Visible;
        }

        private void btEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(MyList.SelectedItem.ToString());
            
            indexSelected = int.Parse(MyList.SelectedItem.ToString().Split(',')[0].Split('=')[1]);
            db.EjecutarConsultar("DELETE FROM InformationModuleAnswers WHERE id = " + indexSelected);
            MyList.Items.RemoveAt(MyList.SelectedIndex);
        }

        private void btTerminar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btModificar_Click(object sender, RoutedEventArgs e)
        {

            bool correct;
            if (MyList.SelectedIndex != -1)
            {
                correct = Boolean.Parse(MyList.SelectedItem.ToString().Split('=').Last().Split('}')[0]);
                ckCorrecta.IsChecked = correct;
                indexSelected = int.Parse(MyList.SelectedItem.ToString().Split(',')[0].Split('=')[1]);
                tbRespuesta.Text = MyList.SelectedItem.ToString().Split('=')[2].Split(',')[0];
                btActualizar.Visibility = Visibility.Visible;
                btAgregar.Visibility = Visibility.Hidden;
                btModificar.Visibility = Visibility.Hidden;

            }
        }

        private void MyList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tbRespuesta.Text = "";
            btActualizar.Visibility = Visibility.Hidden;
            btAgregar.Visibility = Visibility.Visible;
            btModificar.Visibility = Visibility.Visible;
        }
    }
}
