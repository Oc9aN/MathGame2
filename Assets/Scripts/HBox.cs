using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBox : MonoBehaviour {

    [System.NonSerialized]
    public int index = 0;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("HSprite"))
        {
            col.transform.parent.parent.GetComponent<Hgame>().drag = null;
            col.transform.parent.parent.GetComponent<Hgame>().check = true;
            col.transform.position = transform.GetChild(index).position;
            index++;
            col.transform.parent.parent.GetComponent<Hgame>().IncreseMain(index);
            col.transform.tag = "Untagged";
        }
    }
}
