using UnityEngine;
using System.Collections;

public class DropList : MonoBehaviour {
    public Camera camera;
    UIPopupList pop;
    public DropListList dpl;
	// Use this for initialization
	void Start () {
	
	}

    public void seleccionarItem(int indice)
    {
        if(pop != null)
        {
            pop.value = pop.items[indice];
            dpl.transform.root.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //print("click");
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                UIPopupList popAux = hit.collider.GetComponent<UIPopupList>();
                if(popAux != null && popAux.openOn == UIPopupList.OpenOn.Manual && popAux.items.Count > 0)
                {
                    pop = popAux;
                    System.Collections.Generic.List<string> lista = pop.items;
                    dpl.transform.root.gameObject.SetActive(true);
                    dpl.crearLista(this, lista);
                    dpl.GetComponent<UIPanel>().clipOffset = new Vector2(0f, -370f);
                    dpl.transform.localPosition = new Vector3(0f, 390f, 0f);
                    // pop.value = "Modulo 10";
                }
            }
        }
    }
}
