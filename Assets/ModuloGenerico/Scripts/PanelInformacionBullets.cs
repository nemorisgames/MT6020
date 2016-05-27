using UnityEngine;
using System.Collections;

public class PanelInformacionBullets : MonoBehaviour {
    public GameObject informacionBullet;
    public UILabel descripcion;
	// Use this for initialization
	void Start () {
        
    }

    public void inicializar(string descripcion, InformacionBullet[] bullets)
    {
        this.descripcion.text = descripcion;
        GameObject g = null;
        if (bullets.Length > 0)
        {
            g = (GameObject)Instantiate(informacionBullet);
            g.transform.parent = transform;
            g.transform.localScale = Vector3.one;
            g.transform.localPosition = new Vector3(20f, -46f, 0f);
            UISprite sp = g.GetComponent<UISprite>();
            sp.spriteName = bullets[0].sprite;

            sp.SetAnchor(this.descripcion.gameObject);
            sp.leftAnchor.SetHorizontal(this.descripcion.transform, -1f);
            sp.leftAnchor.absolute = 0;
            sp.bottomAnchor.SetVertical(this.descripcion.transform, -1f);
            sp.bottomAnchor.absolute = -67;
            sp.rightAnchor.SetHorizontal(this.descripcion.transform, -1f);
            sp.rightAnchor.absolute = 59;
            sp.topAnchor.SetVertical(this.descripcion.transform, -1f);
            sp.topAnchor.absolute = -5;
            sp.UpdateAnchors();
            g.transform.FindChild("Label").GetComponent<UILabel>().text = bullets[0].texto;
        }
        for(int i = 1; i < bullets.Length; i++)
        {
            GameObject gAux = (GameObject)Instantiate(informacionBullet, new Vector3(20f, -46f + i * 70f, 0f), Quaternion.identity);
            gAux.transform.parent = transform;
            gAux.transform.localScale = Vector3.one;
            gAux.transform.localPosition = new Vector3(20f, -46f, 0f);
            UISprite sp = gAux.GetComponent<UISprite>();
            sp.spriteName = bullets[i].sprite;
            sp.SetAnchor(g.transform.FindChild("Label"));
            sp.leftAnchor.SetHorizontal(g.transform.FindChild("Label"), -1f);
            sp.leftAnchor.absolute = -67;
            sp.bottomAnchor.SetVertical(g.transform.FindChild("Label"), -1f);
            sp.bottomAnchor.absolute = -70;
            sp.rightAnchor.SetHorizontal(g.transform.FindChild("Label"), -1f);
            sp.rightAnchor.absolute = -8;
            sp.topAnchor.SetVertical(g.transform.FindChild("Label"), -1f);
            sp.topAnchor.absolute = -8;
            sp.UpdateAnchors();
            gAux.transform.FindChild("Label").GetComponent<UILabel>().text = bullets[i].texto;
            g = gAux;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
