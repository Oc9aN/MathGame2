using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBox : MonoBehaviour {
    [System.NonSerialized]
    public int index = 0;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("ISprite"))
        {
            col.transform.parent.parent.GetComponent<Igame>().drag = null;
            col.transform.parent.parent.GetComponent<Igame>().check = true;
            col.transform.position = transform.GetChild(index).transform.position;
            index++;
            if (transform.name == "Left")
                col.transform.parent.parent.GetComponent<Igame>().IncreseLeft(index);
            else
                col.transform.parent.parent.GetComponent<Igame>().IncreseRight(index);
            col.transform.tag = "Untagged";
        }
    }
}
