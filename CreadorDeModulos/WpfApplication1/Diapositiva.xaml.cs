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
    /// Interaction logic for Diapositiva.xaml
    /// </summary>
    public partial class Diapositiva : Window
    {
        DataBase db = new DataBase();
        DataTable dt = new DataTable();
        private string myDiapo, selectDesigne;
        private int idModulo, diapo;
     
        public Diapositiva(int _idModulo)
        {
            InitializeComponent();
            this.idModulo = _idModulo;
            CargarLista();
            
        }

        private void btPreguntas_Click(object sender, RoutedEventArgs e)
        {
            Preguntas pre = new Preguntas(idModulo);
            pre.ShowDialog();
        }

        private void CargarLista()
        {
            MyList.Items.Clear();
            dt = db.Consultar("SELECT * FROM Slider WHERE fk_module =" + idModulo + " ORDER BY orderSlider");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MyList.Items.Add(new { Diapo = dt.Rows[i][4].ToString(), Texto = dt.Rows[i][2].ToString(), idDiapo = dt.Rows[i][0].ToString() });
            }
        }

        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {

            switch (cbDiseño.SelectedIndex)
            {
                case 0:
                    myDiapo = "Diseño1.png";
                break;
                case 1:
                    myDiapo = "Diseño2.png";
                    break;
                case 2:
                    myDiapo = "Diseño3.png";
                    break;
                case 3:
                    myDiapo = "Diseño4.png";
                    break;
                case 4:
                    myDiapo = "Diseño5.png";
                    break;
                case 5:
                    myDiapo = "Diseño6.png";
                    break;
            }

            if (cbDiseño.SelectedIndex != -1 && tbNombre.Text.Trim() != "")
            {
                //MessageBox.Show("INSERT INTO Slider (fk_module, Title, order) VALUES (" + idModulo + ",'" + tbNombre.Text + "'," + MyList.Items.Count + ")");
                db.EjecutarConsultar("INSERT INTO Slider (fk_module, Title, orderSlider, designe) VALUES ("+idModulo+",'"+tbNombre.Text+"',"+MyList.Items.Count+",'"+ myDiapo + "')");

                MyList.Items.Add(new { Diapo = myDiapo, Texto = tbNombre.Text, idDiapo = db.LastID("Slider")});
                tbNombre.Text = "";
                //db.EjecutarConsultar("INSERT INTO Slider (fk_module, Title)");
               

            }
            else
                MessageBox.Show("Debe ingresar todos los datos", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string localIdDiapo;

            if (MyList.Items.Count != 0)
            {
                localIdDiapo = MyList.Items[MyList.SelectedIndex].ToString().Split('=')[3].Split('}')[0].Trim();
                MyList.Items.Remove(MyList.SelectedItem);
                db.Consultar("DELETE FROM Slider WHERE id ="+localIdDiapo); 
            }

            //Alterar orden
        }

        private void btSubir_Click(object sender, RoutedEventArgs e)
        {
            string localDiseño, localNombreDiapo, localIdDiapo, localIdDiapoReemplazado;
            int selectedIndex;
            if (MyList.SelectedIndex > 0)
            {
                localDiseño = MyList.Items[MyList.SelectedIndex].ToString().Split(',')[0].Split('=')[1].Trim();
                localNombreDiapo = MyList.Items[MyList.SelectedIndex].ToString().Split(',')[1].Split('=')[1].Trim();
                localIdDiapo = MyList.Items[MyList.SelectedIndex].ToString().Split('=')[3].Split('}')[0].Trim();
                localIdDiapoReemplazado = MyList.Items[MyList.SelectedIndex - 1].ToString().Split('=')[3].Split('}')[0].Trim();
                selectedIndex = MyList.SelectedIndex;
                /*MyList.Items.Insert(MyList.SelectedIndex - 1, new { Diapo = localDiseño, Texto = localNombreDiapo, idDiapo = int.Parse(localIdDiapo) });
                MyList.Items.Remove(MyList.SelectedItem);*/
             
                db.EjecutarConsultar("UPDATE Slider SET orderSlider="+(selectedIndex-1)+" WHERE id = "+localIdDiapo);
                db.EjecutarConsultar("UPDATE Slider SET orderSlider=" + selectedIndex + " WHERE id = " + localIdDiapoReemplazado);
                CargarLista();
            }

            /* MessageBox.Show(localDiseño);
             MessageBox.Show(localNombreDiapo);
             MessageBox.Show(localIdDiapo);*/

            //MyList.Items[0].ToString().Split('=')[1].Split('}')[0].Trim();
        }

     
        private void btBajar_Click(object sender, RoutedEventArgs e)
        {
            string localDiseño, localNombreDiapo, localIdDiapo, localIdDiapoReemplazado;
            int selectedIndex;
            if (MyList.SelectedIndex < MyList.Items.Count-1)
            {
                localDiseño = MyList.Items[MyList.SelectedIndex].ToString().Split(',')[0].Split('=')[1].Trim();
                localNombreDiapo = MyList.Items[MyList.SelectedIndex].ToString().Split(',')[1].Split('=')[1].Trim();
                localIdDiapo = MyList.Items[MyList.SelectedIndex].ToString().Split('=')[3].Split('}')[0].Trim();
                localIdDiapoReemplazado = MyList.Items[MyList.SelectedIndex + 1].ToString().Split('=')[3].Split('}')[0].Trim();
                selectedIndex = MyList.SelectedIndex;
              /*  MyList.Items.Insert(MyList.SelectedIndex + 2, new { Diapo = localDiseño, Texto = localNombreDiapo, idDiapo = int.Parse(localIdDiapo) });
                MyList.Items.Remove(MyList.SelectedItem);*/
            
                db.EjecutarConsultar("UPDATE Slider SET orderSlider=" + (selectedIndex + 1) + " WHERE id = " + localIdDiapo);
                db.EjecutarConsultar("UPDATE Slider SET orderSlider=" + selectedIndex + " WHERE id = " + localIdDiapoReemplazado);
                CargarLista();
            }

        }

        // MyList.Items.Insert(2, new { Diapo = myDiapo, Texto = tbNombre.Text, Orden = 2 }); 

        private void MyList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            selectDesigne = MyList.SelectedItem.ToString().Split('=')[1].Split('}')[0].Split(',')[0].Trim();
            int mySlider = int.Parse(MyList.SelectedItem.ToString().Split('=')[3].Split('}')[0].Trim());

            if (selectDesigne == "Diseño1.png")
            {
                CreacionContenido cr = new CreacionContenido(mySlider, "diseno1");
                cr.ShowDialog();
            }else
                if (selectDesigne == "Diseño2.png")
            {
                CreacionContenido cr = new CreacionContenido(mySlider, "diseno2");
                cr.ShowDialog();
            }
            else
                if (selectDesigne == "Diseño3.png")
            {
                CreacionContenido cr = new CreacionContenido(mySlider, "diseno3");
                cr.ShowDialog();
            }
            else
                if (selectDesigne == "Diseño4.png")
            {
                CreacionContenido cr = new CreacionContenido(mySlider, "diseno4");
                cr.ShowDialog();
            }
            else
                if (selectDesigne == "Diseño5.png")
            {
                CreacionContenido cr = new CreacionContenido(mySlider, "diseno5");
                cr.ShowDialog();
            }
            else
                if (selectDesigne == "Diseño6.png")
            {
                CreacionContenido cr = new CreacionContenido(mySlider, "diseno6");
                cr.ShowDialog();
            }
            


        }
    }
}
