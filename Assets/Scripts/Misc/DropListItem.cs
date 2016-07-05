using UnityEngine;
using System.Collections;

public class DropListItem : MonoBehaviour {
    public int indice = -1;
    public UILabel label;
    DropListList padre;
	// Use this for initialization
	void Start () {
	
	}

    public void inicializar(int indice, string texto, DropListList padre)
    {
        this.padre = padre;
        this.indice = indice;
        label.text = texto;
    }

    public void clicked()
    {
        padre.itemClicked(indice);

        print("item clicked");
    }

	// Update is called once per frame
	void Update () {
	
	}
}
