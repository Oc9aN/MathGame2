using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hgame : MonoBehaviour {
    public List<Sprite> Sprites = new List<Sprite>();

    public Transform Left;
    public Transform Right;

    public List<Sprite> Numbers = new List<Sprite>();

    public SpriteRenderer LeftText;
    public SpriteRenderer RightText;
    public SpriteRenderer MainText;

    public Image UpMessage;

    public Game SystemGame;

    List<Vector3> LeftPos = new List<Vector3>();
    List<Vector3> RightPos = new List<Vector3>();

    List<SpriteRenderer> LeftObj = new List<SpriteRenderer>();
    List<SpriteRenderer> RightObj = new List<SpriteRenderer>();

    int LeftCount;
    int RightCount;

    public HBox Box;

    public void IncreseMain(int index)
    {
        MainText.sprite = Numbers[index];
        check = false;
        if (LeftCount + RightCount == index)
        {
            Box.GetComponent<HBox>().index = 0;
            StartCoroutine(ReStart());
        }
    }

    IEnumerator ReStart()
    {
        SystemGame.SoundStop();
        SystemGame.Sounds[4].Play();
        SystemGame.Sounds[1].Play();
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < Left.childCount; i++)
        {
            LeftObj[i].transform.position = LeftPos[i];
        }
        for (int i = 0; i < Right.childCount; i++)
        {
            RightObj[i].transform.position = RightPos[i];
        }
        HgameStart();
    }

    void Setting()
    {
        LeftPos.RemoveRange(0, LeftPos.Count);
        LeftObj.RemoveRange(0, LeftObj.Count);
        RightPos.RemoveRange(0, RightPos.Count);
        RightObj.RemoveRange(0, RightObj.Count);

        for (int i = 0; i < Left.childCount; i++)
        {
            LeftPos.Add(Left.GetChild(i).position);
            LeftObj.Add(Left.GetChild(i).GetComponent<SpriteRenderer>());
        }
        for (int i = 0; i < Right.childCount; i++)
        {
            RightPos.Add(Right.GetChild(i).position);
            RightObj.Add(Right.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

    bool once = true;
    public void HgameStart()
    {
        if (once)
        {
            Setting();
            once = false;
        }
        MainText.sprite = Numbers[0];
        LeftText.transform.parent.gameObject.SetActive(true);
        SystemGame.SoundStop();

        UpMessage.transform.gameObject.SetActive(true);
        UpMessage.SetNativeSize();

        SystemGame.Sounds[0].Play();
        
        gameObject.SetActive(true);
        Sprite tempSprite = Sprites[Random.Range(0, Sprites.Count)];

        LeftText.transform.parent.gameObject.SetActive(true);

        while (true)
        {
            LeftCount = Random.Range(1, 6);
            LeftText.sprite = Numbers[LeftCount];
            RightCount = Random.Range(1, 6);
            RightText.sprite = Numbers[RightCount];
            if (LeftCount + RightCount <= 9)
                break;
        }

        for (int i = 0; i < LeftObj.Count; i++)
        {
            if (i < LeftCount)
            {
                LeftObj[i].sprite = tempSprite;
                LeftObj[i].GetComponent<BoxCollider2D>().size = LeftObj[i].sprite.bounds.size;
                LeftObj[i].GetComponent<BoxCollider2D>().offset = LeftObj[i].sprite.bounds.center;
            }
            else
            {
                LeftObj[i].sprite = null;
                LeftObj[i].GetComponent<BoxCollider2D>().size = Vector3.zero;
            }
            LeftObj[i].transform.position = LeftPos[i];
            LeftObj[i].tag = "HSprite";
        }
        for (int i = 0; i < RightObj.Count; i++)
        {
            if (i < RightCount)
            {
                RightObj[i].sprite = tempSprite;
                RightObj[i].GetComponent<BoxCollider2D>().size = RightObj[i].sprite.bounds.size;
                RightObj[i].GetComponent<BoxCollider2D>().offset = RightObj[i].sprite.bounds.center;
            }
            else
            {
                RightObj[i].sprite = null;
                RightObj[i].GetComponent<BoxCollider2D>().size = Vector3.zero;
            }
            RightObj[i].transform.position = RightPos[i];
            RightObj[i].tag = "HSprite";
        }
    }

    [System.NonSerialized]
    public Transform drag = null;

    [System.NonSerialized]
    public bool check = false;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.transform.CompareTag("HSprite") && drag == null)
            {
                drag = hit.transform;
            }
            if (drag != null)
            {
                Vector3 temp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
                temp = Camera.main.ScreenToWorldPoint(temp);
                temp.z = 0;
                drag.position = temp;
            }
        }
        else if (Input.GetMouseButtonUp(0) && !check)
        {
            MouseOff();
        }
    }

    public void MouseOff()
    {
        if (drag != null)
        {
            switch (drag.gameObject.name)
            {
                case "0":
                    if (drag.parent.name == "Left")
                        drag.position = LeftPos[0];
                    else
                        drag.position = RightPos[0];
                    break;
                case "1":
                    if (drag.parent.name == "Left")
                        drag.position = LeftPos[1];
                    else
                        drag.position = RightPos[1];
                    break;
                case "2":
                    if (drag.parent.name == "Left")
                        drag.position = LeftPos[2];
                    else
                        drag.position = RightPos[2];
                    break;
                case "3":
                    if (drag.parent.name == "Left")
                        drag.position = LeftPos[3];
                    else
                        drag.position = RightPos[3];
                    break;
                case "4":
                    if (drag.parent.name == "Left")
                        drag.position = LeftPos[4];
                    else
                        drag.position = RightPos[4];
                    break;
                case "5":
                    if (drag.parent.name == "Left")
                        drag.position = LeftPos[5];
                    else
                        drag.position = RightPos[5];
                    break;
                default:
                    break;
            }
        }
        drag = null;
    }
}
