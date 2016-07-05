using UnityEngine;
using System.Collections;

public class DropListList : MonoBehaviour {
    public DropListItem dp;
    public GameObject[] lista;
    public DropList listener;
	// Use this for initialization
	void Start () {
	}

    public void crearLista(DropList listener, System.Collections.Generic.List<string> items)
    {
        this.listener = listener;
        eliminarElementos();
        lista = new GameObject[items.Count];
        for (int i = 0; i < items.Count; i++)
        {
            GameObject g = Instantiate(dp.gameObject);
            DropListItem d = g.GetComponent<DropListItem>();
            d.inicializar(i, items[i], this);
            g.transform.parent = transform;
            g.transform.localScale = Vector3.one;
            g.transform.localPosition = new Vector3(0f, -40f * i, 0f);
            lista[i] = (g);
            print("agregando");
        }
        print("size " + lista.Length);
    }

    public void itemClicked(int indice)
    {
        print("item clicked droplistlist");
        eliminarElementos();
        listener.seleccionarItem(indice);
    }

    public void eliminarElementos()
    {
        if (lista.Length > 0)
        {
            print(lista.Length);
            for (int i = 0; i < lista.Length; i++)
            {
                print("eliminando");
                Destroy(lista[i]);
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
