using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Min : MonoBehaviour {
    GameObject TenNumber;
    GameObject OneNumber;
    GameObject MinNumber;

    public GameObject Bag;
    public GameObject BagTag;

    public List<Sprite> Sprites = new List<Sprite>();

    public List<Transform> Points = new List<Transform>();

    List<SpriteRenderer> tenRenders = new List<SpriteRenderer>();
    List<SpriteRenderer> oneRenders = new List<SpriteRenderer>();
    List<SpriteRenderer> minRenders = new List<SpriteRenderer>();
    
    List<SpriteRenderer> BlacktenRenders = new List<SpriteRenderer>();
    List<SpriteRenderer> BlackoneRenders = new List<SpriteRenderer>();
    List<SpriteRenderer> BlackminRenders = new List<SpriteRenderer>();

    public SpriteRenderer tenImage;
    public SpriteRenderer oneImage;
    public SpriteRenderer minImage;

    public List<Sprite> NumImage = new List<Sprite>();

    int ten;
    int one;
    int Minus;

    public Text tenField;
    public Text oneField;

    public Transform Parent;

    public GameObject Numbers;

    public Game SystemGame;

    void GameSet(int level)
    {
        temp = 1;
        for (int i = 0; i < Parent.childCount; i++)
        {
            Destroy(Parent.GetChild(i).gameObject);
        }

        once = true;
        tenField.transform.parent.gameObject.SetActive(true);
        oneField.transform.parent.gameObject.SetActive(true);

        if (level == 2 || level == 3)
        {
            TenNumber = Instantiate(Bag, Points[1].position, Quaternion.identity);
            TenNumber.transform.parent = Parent;
            tenRenders.RemoveRange(0, tenRenders.Count);
            BlacktenRenders.RemoveRange(0, BlacktenRenders.Count);
            for (int i = 1; i < TenNumber.transform.childCount; i++)
            {
                tenRenders.Add(TenNumber.transform.GetChild(i).GetComponent<SpriteRenderer>());
                BlacktenRenders.Add(TenNumber.transform.GetChild(0).GetChild(i - 1).GetComponent<SpriteRenderer>());
            }
        }

        OneNumber = Instantiate(Bag, Points[0].position, Quaternion.identity);
        OneNumber.transform.parent = Parent;
        oneRenders.RemoveRange(0, oneRenders.Count);
        BlackoneRenders.RemoveRange(0, BlackoneRenders.Count);
        for (int i = 1; i < OneNumber.transform.childCount; i++)
        {
            oneRenders.Add(OneNumber.transform.GetChild(i).GetComponent<SpriteRenderer>());
            BlackoneRenders.Add(OneNumber.transform.GetChild(0).GetChild(i - 1).GetComponent<SpriteRenderer>());
        }

        MinNumber = Instantiate(BagTag, Points[2].position, Quaternion.identity);
        MinNumber.transform.parent = Parent;
        minRenders.RemoveRange(0, minRenders.Count);
        BlackminRenders.RemoveRange(0, BlackminRenders.Count);
        for (int i = 1; i < MinNumber.transform.childCount; i++)
        {
            minRenders.Add(MinNumber.transform.GetChild(i).GetComponent<SpriteRenderer>());
            BlackminRenders.Add(MinNumber.transform.GetChild(0).GetChild(i - 1).GetComponent<SpriteRenderer>());
        }
    }

    int StageLevel;
    Sprite tempSprite = null;
    Sprite BlacktempSprite = null;
    int tempint;
    public void GameStart(int Level)
    {
        Numbers.SetActive(true);
        StageLevel = Level;

        GameSet(Level);

        gameObject.SetActive(true);

        if (Level == 1)
        {
            ten = 0;
            while (true)
            {
                one = Random.Range(2, 10);
                oneImage.gameObject.SetActive(true);
                oneImage.sprite = NumImage[one - 1];

                Minus = Random.Range(2, 10);
                minImage.gameObject.SetActive(true);
                minImage.sprite = NumImage[Minus - 1];
                if (one - Minus >= 1)
                    break;
            }
        }
        else if (Level == 2)
        {
            ten = 1;
            tenImage.gameObject.SetActive(true);
            tenImage.sprite = NumImage[0];

            one = Random.Range(2, 10);
            oneImage.gameObject.SetActive(true);
            oneImage.sprite = NumImage[one - 1];

            Minus = Random.Range(2, 10);
            minImage.gameObject.SetActive(true);
            minImage.sprite = NumImage[Minus - 1];
        }


        if (Level == 1 || Level == 2)
        {
            tempint = Random.Range(0, 2);
            tempSprite = Sprites[tempint];
            BlacktempSprite = Sprites[tempint + 2];
        }

        if (Level == 2 || Level == 3)
        {
            for (int i = 0; i < tenRenders.Count; i++)
            {
                if (i < ten * 10)
                {
                    tenRenders[i].sprite = tempSprite;
                    BlacktenRenders[i].sprite = BlacktempSprite;
                    if (tempint == 1)
                    {
                        tenRenders[i].transform.localScale = Vector3.one * 2f;
                        BlacktenRenders[i].transform.localScale = Vector3.one * 2f;
                    }
                    else
                    {
                        tenRenders[i].transform.localScale = Vector3.one;
                        BlacktenRenders[i].transform.localScale = Vector3.one;
                    }
                }
                else
                    tenRenders[i].sprite = null;

            }
        }

        for (int i = 0; i < oneRenders.Count; i++)
        {
            if (i < one)
            {
                oneRenders[i].sprite = tempSprite;
                BlackoneRenders[i].sprite = BlacktempSprite;
                if (tempint == 1)
                {
                    oneRenders[i].transform.localScale = Vector3.one * 2f;
                    BlackoneRenders[i].transform.localScale = Vector3.one * 2f;
                }
                else
                {
                    oneRenders[i].transform.localScale = Vector3.one;
                    BlackoneRenders[i].transform.localScale = Vector3.one;
                }
            }
            else
                oneRenders[i].sprite = null;
        }
        
        for (int i = 0; i < minRenders.Count; i++)
        {
            if (i < Minus)
            {
                minRenders[i].sprite = tempSprite;
                BlackminRenders[i].sprite = BlacktempSprite;
                if (tempint == 1)
                {
                    minRenders[i].transform.localScale = Vector3.one * 2f;
                    BlackminRenders[i].transform.localScale = Vector3.one * 2f;
                }
                else
                {
                    minRenders[i].transform.localScale = Vector3.one;
                    BlackminRenders[i].transform.localScale = Vector3.one;
                }
            }
            else
                minRenders[i].sprite = null;
        }
    }

    int temp = 1;
    bool once = true;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && SystemGame.click)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.transform.CompareTag("Min"))
            {
                if (one - temp >= 0 && Minus - temp >= 0)
                {
                    hit.transform.GetChild(Minus - temp + 1).gameObject.SetActive(false);
                    oneRenders[one - temp].gameObject.SetActive(false);
                }
                else if ((StageLevel == 2 || StageLevel == 3) && Minus - temp + 1 >= 0 && one < Minus)
                {
                    if (once)
                    {
                        StartCoroutine(Move(TenNumber.transform, Points[0].position + Vector3.left * 2.5f));
                        once = false;
                    }
                    else
                    {
                        hit.transform.GetChild(Minus - temp + 2).gameObject.SetActive(false);
                        tenRenders[ten * 10 - temp + one + 1].gameObject.SetActive(false);
                    }
                }
                temp++;
            }
        }
    }

    IEnumerator Move(Transform obj, Vector3 pos)
    {
        SystemGame.click = false;
        while (obj.position != pos)
        {
            obj.position = Vector3.MoveTowards(obj.position, pos, Time.deltaTime * 15f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        SystemGame.click = true;
    }

    void BtnScale(int index)
    {
        for (int i = 0; i < Numbers.transform.childCount - 1; i++)
        {
            Numbers.transform.GetChild(i).localScale = Vector3.one;
            if (i == index)
            {
                Numbers.transform.GetChild(i).localScale = Vector3.one * 0.8f;
            }
        }
    }

    int NowNum = -1;
    public void NumberButton(int num)
    {
        NowNum = num;
        if (temptext != null)
        {
            if (temptext.name == "Ten")
            {
                MinNumberTen = NowNum;
                tenField.text = MinNumberTen.ToString();
                tenField.transform.parent.localScale = Vector3.one;
            }
            else if (temptext.name == "One")
            {
                MinNmberOne = NowNum;
                oneField.text = MinNmberOne.ToString();
                oneField.transform.parent.localScale = Vector3.one;
            }
            temptext = null;
            NowNum = 0;
        }
        else
        {
            BtnScale(num);
        }
    }

    int MinNumberTen = 123;
    int MinNmberOne = 123;
    Text temptext;
    public void TenEntered()
    {
        if (NowNum >= 0)
        {
            MinNumberTen = NowNum;
            tenField.text = MinNumberTen.ToString();
            NowNum = -1;
        }
        else
        {
            tenField.transform.parent.localScale = Vector3.one * 0.8f;
            temptext = tenField;
        }
        BtnScale(100);
    }
    public void OneEntered()
    {
        if (NowNum >= 0)
        {
            MinNmberOne = NowNum;
            oneField.text = MinNmberOne.ToString();
            NowNum = -1;
        }
        else
        {
            oneField.transform.parent.localScale = Vector3.one * 0.8f;
            temptext = oneField;
        }
        BtnScale(100);
    }

    bool finshBtn = true;
    public void FinishEnter()
    {
        if (((MinNmberOne < 10 && MinNumberTen < 10) || MinNmberOne < 10) && finshBtn)
        {
            if (MinNumberTen > 10)
                MinNumberTen = 0;
            finshBtn = false;
            Debug.Log(MinNumberTen + ", " + MinNmberOne + ", " + ((ten * 10 + one) - Minus));
            Debug.Log(ten + ", " + one + ", " + Minus);
            if (MinNumberTen * 10 + MinNmberOne == (ten * 10 + one) - Minus)
            {
                C = StartCoroutine(Clear());
            }
            else
            {
                F = StartCoroutine(Fail());
            }
        }
    }

    public Coroutine F;
    public Coroutine C;
    public IEnumerator Fail()
    {
        SystemGame.Sounds[2].Play();
        yield return new WaitForSeconds(2.0f);
        RESET();
        if (StageLevel == 2 || StageLevel == 3)
            GameStart(3);
        else if (StageLevel == 1 || StageLevel == 4)
            GameStart(4);
        finshBtn = true;
    }

    public IEnumerator Clear()
    {
        SystemGame.SoundStop();
        SystemGame.Sounds[4].Play();
        SystemGame.Sounds[1].Play();
        yield return new WaitForSeconds(2.0f);
        RESET();
        if (StageLevel == 2 || StageLevel == 3)
            GameStart(2);
        else if (StageLevel == 1 || StageLevel == 4)
            GameStart(1);
        finshBtn = true;
    }

    public void RESET()
    {
        tenField.transform.parent.localScale = Vector3.one;
        oneField.transform.parent.localScale = Vector3.one;
        BtnScale(100);
        StopAllCoroutines();
        tenField.text = "?";
        oneField.text = "?";
        MinNumberTen = 123;
        MinNmberOne = 123;
        temptext = null;
        NowNum = -1;
    }

    public void REStart()
    {
        RESET();
        if (StageLevel == 2 || StageLevel == 3)
            GameStart(2);
        else if (StageLevel == 1 || StageLevel == 4)
            GameStart(1);
        finshBtn = true;
    }
}
