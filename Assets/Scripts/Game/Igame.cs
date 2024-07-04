using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Igame : MonoBehaviour {

    public List<Sprite> Sprites = new List<Sprite>();

    public Transform Main;

    public List<Sprite> Numbers = new List<Sprite>();

    public SpriteRenderer LeftText;
    public SpriteRenderer RightText;
    public SpriteRenderer MainText;

    public Image UpMessage;

    public Game SystemGame;

    List<Vector3> MainPos = new List<Vector3>();

    List<SpriteRenderer> MainObj = new List<SpriteRenderer>();

    public List<IBox> indexs = new List<IBox>();

    public IBox LBox;
    public IBox RBox;

    int MainCount;

    int left;
    int right;
    public void IncreseLeft(int index)
    {
        left = index;
        LeftText.sprite = Numbers[left];
        if (left + right == MainCount)
        {
            StartCoroutine(ReStart());
        }
    }
    public void IncreseRight(int index)
    {
        right = index;
        RightText.sprite = Numbers[right];
        if (left + right == MainCount)
        {
            StartCoroutine(ReStart());
        }
    }

    IEnumerator ReStart()
    {
        SystemGame.SoundStop();
        SystemGame.Sounds[4].Play();
        SystemGame.Sounds[1].Play();
        yield return new WaitForSeconds(2.0f);
        check = false;
        for (int i = 0; i < indexs.Count; i++)
            indexs[i].index = 0;
        left = 0;
        LeftText.sprite = Numbers[left];
        right = 0;
        RightText.sprite = Numbers[right];
        for (int i = 0; i < Main.childCount; i++)
        {
            MainObj[i].transform.position = MainPos[i];
        }
        IgameStart();
    }

    void Setting()
    {
        MainPos.RemoveRange(0, MainPos.Count);
        MainObj.RemoveRange(0, MainObj.Count);

        for (int i = 0; i < Main.childCount; i++)
        {
            MainPos.Add(Main.GetChild(i).position);
            MainObj.Add(Main.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

    bool once = true;
    public void IgameStart()
    {
        if (once)
        {
            Setting();
            once = false;
        }
        LBox.index = 0;
        RBox.index = 0;
        LeftText.sprite = Numbers[0];
        LeftText.transform.parent.gameObject.SetActive(true);
        SystemGame.SoundStop();

        RightText.sprite = Numbers[0];

        UpMessage.transform.gameObject.SetActive(true);
        UpMessage.SetNativeSize();

        SystemGame.Sounds[0].Play();

        
        gameObject.SetActive(true);
        Sprite tempSprite = Sprites[Random.Range(0, Sprites.Count)];

        MainText.transform.parent.gameObject.SetActive(true);

        MainCount = Random.Range(1, 7);
        MainText.sprite = Numbers[MainCount];

        for (int i = 0; i < MainObj.Count; i++)
        {
            if (i < MainCount)
            {
                MainObj[i].sprite = tempSprite;
                MainObj[i].GetComponent<BoxCollider2D>().size = MainObj[i].sprite.bounds.size;
                MainObj[i].GetComponent<BoxCollider2D>().offset = MainObj[i].sprite.bounds.center;
            }
            else
            {
                MainObj[i].GetComponent<BoxCollider2D>().size = Vector3.zero;
                MainObj[i].sprite = null;
            }
            MainObj[i].transform.position = MainPos[i];
            MainObj[i].tag = "ISprite";
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

            if (hit.collider != null && hit.transform.CompareTag("ISprite") && drag == null)
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
                    drag.position = MainPos[0];
                    break;
                case "1":
                    drag.position = MainPos[1];
                    break;
                case "2":
                    drag.position = MainPos[2];
                    break;
                case "3":
                    drag.position = MainPos[3];
                    break;
                case "4":
                    drag.position = MainPos[4];
                    break;
                case "5":
                    drag.position = MainPos[5];
                    break;
                default:
                    break;
            }
        }
        drag = null;
    }
}
