using System.Windows.Controls;

namespace CreadorModulos
{
    class ListaContenido
    {
        public Image _Image { get; set; }
        public string _Value { get; set; }

        public ListaContenido(Image _image, string _value)
        {
            _Image = _image;
            _Value = _value;
        }

    }
}