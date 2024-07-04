using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jgame : MonoBehaviour {
    public GameObject Lineobj;

    public Game SystemGame;

    public List<Sprite> fruits = new List<Sprite>();

    public List<SpriteRenderer> objs = new List<SpriteRenderer>();

    public List<GameObject> Numbers = new List<GameObject>();

    LineRenderer line;

    [System.NonSerialized]
    public int Count = 0;

    GameObject NumberObj;

    public void JgameStart()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            objs[i].sprite = null;
        }
        for (int i = 1; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        gameObject.SetActive(true);
        if (Count == 9)
            Count = 0;
        Count += 1;
        int Ftemp = Random.Range(0, fruits.Count);

        for (int i = 0; i < Count; i++)
        {
            objs[i].sprite = fruits[Ftemp];
        }
        NumberObj = Instantiate(Numbers[Count - 1], transform);
    }

    RaycastHit2D hit2D;
    Transform lastHit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && line == null)
        {
            line = Instantiate(Lineobj, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
            line.transform.SetParent(transform);
        }
        if (Input.GetMouseButton(0) && line != null)
        {
            Vector3 temp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            temp = Camera.main.ScreenToWorldPoint(temp);
            temp.z = 0;
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, temp);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity);


            if (hit2D.transform == null)
            {
                Fail(lastHit);
            }
            else if (hit2D.transform.CompareTag("1") || hit2D.transform.CompareTag("2"))
            {
                lastHit = hit2D.transform;
                Check(hit2D.transform);
            }
        }
        if (Input.GetMouseButtonUp(0) && line != null)
        {
            Fail(lastHit);
        }
    }

    void Fail(Transform hit)
    {
        check8 = true;
        Destroy(line.transform.gameObject);
        if (hit != null)
        {
            Number Num = hit.parent.GetComponent<Number>();
            if (Num.firstcount != Num.first)
            {
                Num.firstcount = 0;
                if (hit.parent.name == "8(Clone)")
                    for (int i = 0; i < Num.first; i++)
                    {
                        NumberObj.transform.GetChild(i).gameObject.SetActive(true);
                    }
                else
                    for (int i = 0; i < Num.first + 1; i++)
                    {
                        NumberObj.transform.GetChild(i).gameObject.SetActive(true);
                    }
            }
            Num.secondcount = 0;
            for (int i = Num.first + 1; i < NumberObj.transform.childCount; i++)
            {
                NumberObj.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        line = null;
    }
    

    bool firstfinish = false;
    bool check8 = true;
    void Check(Transform hit)
    {
        if (hit.CompareTag("1"))
        {
            firstfinish = false;
            hit.gameObject.SetActive(false);
            hit.parent.GetComponent<Number>().firstcount++;

            if (hit.parent.name == "8(Clone)" && check8 && hit.parent.GetComponent<Number>().firstcount == 2)
            {
                hit.parent.GetChild(1).gameObject.SetActive(true);
                check8 = false;
            }
        }
        if (hit.parent.GetComponent<Number>().firstcount == hit.parent.GetComponent<Number>().first)
        {
            if (firstfinish == false)
                line = null;
            firstfinish = true;
            if (hit.CompareTag("2"))
            {
                hit.gameObject.SetActive(false);
                hit.parent.GetComponent<Number>().secondcount++;
            }
            if (hit.parent.GetComponent<Number>().secondcount == hit.parent.GetComponent<Number>().second)
            {
                StartCoroutine(Clear());
                line = null;
            }
        }
    }

    IEnumerator Clear()
    {
        check8 = true;
        SystemGame.Sounds[4].Play();
        SystemGame.Sounds[1].Play();
        yield return new WaitForSeconds(2.0f);
        JgameStart();
    }
}
