using Microsoft.Win32;
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
    /// Interaction logic for CreacionContenido.xaml
    /// </summary>
    public partial class CreacionContenido : Window
    {
        public int idDiseño, idDesigneType;
        private string myIcon, fondo,texto, myFontColor;
        string filename, nameDesigne;

        //Txt=tbTextoContenido.Text, Icon=myIcon, Back=fondo, FontColor=texto });
        private string txtIngresar, iconIngresar, backIngresar, fontIngresar;
        DataBase db = new DataBase();
        DataTable dt = new DataTable();

        public CreacionContenido(int _Diseño, string _nameDesigne)
        {
            InitializeComponent();
            this.idDiseño = _Diseño;
            this.nameDesigne = _nameDesigne;

            dt = db.Consultar("SELECT * FROM Structure WHERE fk_InformationPageDesign = (SELECT id FROM InformationPageDesign WHERE fk_slider="+idDiseño+") ORDER BY orderStructure");
            for (int i = 0;i<dt.Rows.Count;i++)
            {
                myIcon = dt.Rows[i][3].ToString();
                if (dt.Rows[i][2].ToString() != "")
                    texto = dt.Rows[i][2].ToString();
                else
                    texto = "";
                if (dt.Rows[i][4].ToString() != "")
                    fondo = dt.Rows[i][4].ToString();
                else
                    fondo = "White";
                if(dt.Rows[i][5].ToString()!="")
                    myFontColor = dt.Rows[i][5].ToString();
                else
                    myFontColor = "Black";

                MyList.Items.Add(new { Txt = texto, Icon = myIcon,  Back = fondo , FontColor = myFontColor});
            }

            dt = db.Consultar("SELECT image, sound FROM InformationPageDesign WHERE fk_slider=" + idDiseño);

            if (dt.Rows.Count != 0)
            {
                txtImagen.Text = dt.Rows[0][0].ToString();
                txtAudio.Text = dt.Rows[0][1].ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "PNG Files (*.png)|*.png";
            dlg.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                filename = dlg.FileName;
                txtImagen.Text = filename.ToString().Split('\\').Last();
     
            }
        }

        private void btLimpiarLista_Click(object sender, RoutedEventArgs e)
        {
            MyList.Items.Clear();
        }

        private void btEliminarÚltimo_Click(object sender, RoutedEventArgs e)
        {
            if(MyList.Items.Count!=0)
                MyList.Items.RemoveAt(MyList.SelectedIndex);
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            int lastId;
            dt = db.Consultar("SELECT id FROM DesignType WHERE name = '" + nameDesigne+"'");

            if (dt.Rows.Count != 0)
            {
                int.TryParse(dt.Rows[0][0].ToString(), out idDesigneType);
            }
            else
            {
                MessageBox.Show("No se encuentra el diseño");
            }


             dt = db.Consultar("SELECT id FROM InformationPageDesign WHERE fk_slider = "+idDiseño);
            
            if (dt.Rows.Count == 0)
            {
                db.EjecutarConsultar("INSERT INTO InformationPageDesign (fk_slider, fk_DesignType, image, sound) VALUES (" + idDiseño + ","+idDesigneType+", '"+txtImagen.Text+"','" + txtAudio.Text + "')");
                lastId = db.LastID("InformationPageDesign");
            }
            else
            {
                db.EjecutarConsultar("UPDATE InformationPageDesign SET image = '"+ txtImagen.Text + "', sound = '"+ txtAudio.Text + "', fk_DesignType = "+idDesigneType+" WHERE fk_slider = " + idDiseño);
                //lastId = int.Parse(dt.Rows[0][0].ToString());
                lastId = db.LastID("InformationPageDesign");
            }

            db.Consultar("DELETE FROM Structure WHERE fk_informationPageDesign=" + lastId);
            //INGRESAR RESULTADOS A LA BD
            for (int i = 0; i < MyList.Items.Count; i++)
            {
                txtIngresar = MyList.Items[i].ToString().Split(',')[0].Split('=')[1].Trim();
                iconIngresar = MyList.Items[i].ToString().Split(',')[1].Split('=')[1].Trim();
                backIngresar = MyList.Items[i].ToString().Split(',')[2].Split('=')[1].Trim();
                fontIngresar = MyList.Items[i].ToString().Split(',')[3].Split('=')[1].Trim().Split('}')[0];
                db.EjecutarConsultar("INSERT INTO Structure (fk_informationPageDesign,text,icon,background, fontColor, orderStructure) VALUES ("+lastId+",'"+txtIngresar+ "','"+iconIngresar+ "','"+backIngresar+ "','"+fontIngresar+"',"+i+")");
            }

            this.Close();
        }
        
        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {
            switch (cbSelecionarIcono.SelectedIndex)
            {
                case 0:
                    myIcon = "";
                    break;

                case 1:
                    myIcon =@"1a.png";

                    break;
                case 2:
                    myIcon = @"2a.png";

                    break;
                case 3:
                    myIcon = @"3a.png";

                    break;
                case 4:
                    myIcon = @"4a.png";

                    break;
                case 5:
                    myIcon = @"5a.png";

                    break;
                case 6:
                    myIcon = @"6a.png";

                    break;
                case 7:
                    myIcon = @"7a.png";

                    break;
                case 8:
                    myIcon = @"8a.png";

                    break;
                case 9:
                    myIcon = @"9a.png";

                    break;
                case 10:
                    myIcon = @"1v.png";

                    break;
                case 11:
                    myIcon = @"2v.png";

                    break;
                case 12:
                    myIcon = @"3v.png";

                    break;
                case 13:
                    myIcon = @"4v.png";

                    break;
                case 14:
                    myIcon = @"5v.png";

                    break;
                case 15:
                    myIcon = @"6v.png";

                    break;
                case 16:
                    myIcon = @"7v.png";

                    break;
                case 17:
                    myIcon = @"8v.png";

                    break;
                case 18:
                    myIcon = @"9v.png";

                    break;
                case 19:
                    myIcon = @"a.png";

                    break;
                case 20:
                    myIcon = @"b.png";

                    break;
                case 21:
                    myIcon = @"c.png";

                    break;
                case 22:
                    myIcon = @"d.png";

                    break;
                case 23:
                    myIcon = @"e.png";

                    break;
                case 24:
                    myIcon = @"f.png";

                    break;
                case 25:
                    myIcon = @"g.png";

                    break;
                case 26:
                    myIcon = @"h.png";

                    break;
                case 27:
                    myIcon = @"i.png";

                    break;
                case 28:
                    myIcon = @"j.png";

                    break;
                case 29:
                    myIcon = @"a_verde.png";

                    break;
                case 30:
                    myIcon = @"b_verde.png";

                    break;
              
                
            }

            switch (cbSeleccionarFondo.SelectedIndex)
            {
                case 0:
                    fondo = "";
                    break;
                case 1:
                    fondo = "Red";
                break;
                case 2:
                    fondo = "Blue";
                break;
                case 3:
                    fondo = "Yellow";
                    break;
                case 4:
                    fondo = "Green";
                    break;
                case 5:
                    fondo = "Cyan";
                    break;
                case 6:
                    fondo = "Orange";
                    break;
                case 7:
                    fondo = "Black";
                    break;
            
            }
            switch (cbColorTexto.SelectedIndex)
            {
                case 0:
                    texto = "Black";
                    break;
                case 1:
                    texto = "White";
                    break;
                case 2:
                    texto = "Red";
                    break;
                case 3:
                    texto = "Blue";
                    break;
                case 4:
                    texto = "Yellow";
                    break;
                case 5:
                    texto = "Green";
                    break;
                case 6:
                    texto = "Cyan";
                    break;
                case 7:
                    texto = "Orange";
                    break;
            }

          
            MyList.Items.Add(new { Txt=tbTextoContenido.Text, Icon=myIcon, Back=fondo, FontColor=texto });
            tbTextoContenido.Text = "";

        }
    }
}
