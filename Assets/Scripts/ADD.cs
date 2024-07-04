using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADD : MonoBehaviour {
    GameObject TenNumber;
    GameObject OneNumber;
    GameObject AddedNumber;
    GameObject EmptyNumber;

    public GameObject Bag;
    public GameObject BagTag;

    public List<Sprite> Sprites = new List<Sprite>();

    public List<Transform> Points = new List<Transform>();

    List<SpriteRenderer> tenRenders = new List<SpriteRenderer>();
    List<SpriteRenderer> oneRenders = new List<SpriteRenderer>();
    List<SpriteRenderer> addedRenders = new List<SpriteRenderer>();

    List<SpriteRenderer> BlacktenRenders = new List<SpriteRenderer>();
    List<SpriteRenderer> BlackoneRenders = new List<SpriteRenderer>();
    List<SpriteRenderer> BlackaddedRenders = new List<SpriteRenderer>();

    List<Transform> EmptyPos = new List<Transform>();

    public SpriteRenderer tenImage;
    public SpriteRenderer oneImage;
    public SpriteRenderer addImage;

    public List<Sprite> NumImage = new List<Sprite>();

    int ten;
    int one;
    int added;

    public Text tenField;
    public Text oneField;

    public Transform Parent;

    public GameObject Numbers;

    public Game SystemGame;

    int stageLevel;

    void GameSet(int level)
    {
        temp1 = 0;
        temp2 = 0;
        
        for (int i = 0; i < Parent.childCount; i++)
        {
            Destroy(Parent.GetChild(i).gameObject);
        }
        
        oneField.transform.parent.gameObject.SetActive(true);

        if (level == 3 || level == 4)
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
        tenField.transform.parent.gameObject.SetActive(true);

        OneNumber = Instantiate(BagTag, Points[0].position, Quaternion.identity);
        OneNumber.name = "One";
        OneNumber.transform.parent = Parent;
        oneRenders.RemoveRange(0, oneRenders.Count);
        BlackoneRenders.RemoveRange(0, BlackoneRenders.Count);
        for (int i = 1; i < OneNumber.transform.childCount; i++)
        {
            oneRenders.Add(OneNumber.transform.GetChild(i).GetComponent<SpriteRenderer>());
            BlackoneRenders.Add(OneNumber.transform.GetChild(0).GetChild(i - 1).GetComponent<SpriteRenderer>());
        }


        AddedNumber = Instantiate(BagTag, Points[2].position, Quaternion.identity);
        AddedNumber.name = "Add";
        AddedNumber.transform.parent = Parent;
        addedRenders.RemoveRange(0, addedRenders.Count);
        BlackaddedRenders.RemoveRange(0, BlackaddedRenders.Count);
        for (int i = 1; i < AddedNumber.transform.childCount; i++)
        {
            addedRenders.Add(AddedNumber.transform.GetChild(i).GetComponent<SpriteRenderer>());
            BlackaddedRenders.Add(AddedNumber.transform.GetChild(0).GetChild(i - 1).GetComponent<SpriteRenderer>());
        }

        EmptyNumber = Instantiate(Bag, Points[0].position, Quaternion.identity);
        EmptyNumber.transform.parent = Parent;
        EmptyNumber.transform.position = Points[0].position + Vector3.left * 2.5f;
        EmptyPos.RemoveRange(0, EmptyPos.Count);
        for (int i = 1; i < EmptyNumber.transform.childCount; i++)
        {
            EmptyPos.Add(EmptyNumber.transform.GetChild(i).GetComponent<Transform>());
        }
    }

    Sprite tempSprite = null;
    Sprite BlacktempSprite = null;
    int tempint;
    public void GameStart(int Level)
    {
        stageLevel = Level;
        Numbers.SetActive(true);

        gameObject.SetActive(true);

        if (Level == 1)
        {
            ten = 0;
            while (true)
            {
                one = Random.Range(2, 10);
                oneImage.gameObject.SetActive(true);
                oneImage.sprite = NumImage[one - 1];

                added = Random.Range(2, 10);
                addImage.gameObject.SetActive(true);
                addImage.sprite = NumImage[added - 1];
                if (one + added < 10)
                    break;
            }
        }
        else if (Level == 2)
        {
            ten = 0;
            while (true)
            {
                one = Random.Range(2, 10);
                oneImage.gameObject.SetActive(true);
                oneImage.sprite = NumImage[one - 1];

                added = Random.Range(2, 10);
                addImage.gameObject.SetActive(true);
                addImage.sprite = NumImage[added - 1];
                if (one + added >= 10)
                    break;
            }
        }
        else if (Level == 3)
        {
            ten = 1;
            tenImage.gameObject.SetActive(true);
            tenImage.sprite = NumImage[0];

            one = Random.Range(2, 10);
            oneImage.gameObject.SetActive(true);
            oneImage.sprite = NumImage[one - 1];

            added = Random.Range(2, 10);
            addImage.gameObject.SetActive(true);
            addImage.sprite = NumImage[added - 1];
        }

        GameSet(Level);


        if (Level == 1 || Level == 2 || Level == 3)
        {
            tempint = Random.Range(0, 2);
            tempSprite = Sprites[tempint];
            BlacktempSprite = Sprites[tempint + 2];
        }


        if (Level == 3 || Level == 4)
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

        for (int i = 0; i < addedRenders.Count; i++)
        {
            if (i < added)
            {
                addedRenders[i].sprite = tempSprite;
                BlackaddedRenders[i].sprite = BlacktempSprite;
                if (tempint == 1)
                {
                    addedRenders[i].transform.localScale = Vector3.one * 2f;
                    BlackaddedRenders[i].transform.localScale = Vector3.one * 2f;
                }
                else
                {
                    addedRenders[i].transform.localScale = Vector3.one;
                    BlackaddedRenders[i].transform.localScale = Vector3.one;
                }
            }
            else
                addedRenders[i].sprite = null;
        }
    }

    int temp1 = 0;
    int temp2 = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && SystemGame.click)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.transform.CompareTag("ADD"))
            {
                if (temp1 + temp2 <= 9)
                {
                    if (hit.transform.name == "One" && one - temp1 >= 1)
                    {
                        StartCoroutine(Move(hit.transform.GetChild(one - temp1), EmptyPos[temp1 + temp2].transform.position, temp1 + temp2));
                        hit.transform.GetChild(one - temp1).parent = EmptyNumber.transform;
                        temp1++;
                    }
                    else if (hit.transform.name == "Add" && added - temp2 >= 1)
                    {
                        StartCoroutine(Move(hit.transform.GetChild(added - temp2), EmptyPos[temp1 + temp2].transform.position, temp1 + temp2));
                        hit.transform.GetChild(added - temp2).parent = EmptyNumber.transform;
                        temp2++;
                    }
                }
            }
        }
    }

    IEnumerator Move(Transform obj, Vector3 pos, int index)
    {
        while (obj.position != pos)
        {
            obj.position = Vector3.MoveTowards(obj.position, pos, Time.deltaTime * 15f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (index == 9)
        {
            if (stageLevel == 2)
                StartCoroutine(Move(EmptyNumber.transform, Points[1].position, 0));
            else
                StartCoroutine(Move(EmptyNumber.transform, Points[1].position + Vector3.left * 2.5f, 0));
        }
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
                ADDEDNumberTen = NowNum;
                tenField.text = ADDEDNumberTen.ToString();
                tenField.transform.parent.localScale = Vector3.one;
            }
            else if (temptext.name == "One")
            {
                ADDEDNumberOne = NowNum;
                oneField.text = ADDEDNumberOne.ToString();
                oneField.transform.parent.localScale = Vector3.one;
            }
            temptext = null;
            NowNum = -1;
        }
        else
            BtnScale(num);
    }

    int ADDEDNumberTen = 123;
    int ADDEDNumberOne = 123;
    Text temptext;
    public void TenEntered()
    {
        if (NowNum >= 0)
        {
            ADDEDNumberTen = NowNum;
            tenField.text = ADDEDNumberTen.ToString();
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
            ADDEDNumberOne = NowNum;
            oneField.text = ADDEDNumberOne.ToString();
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
        if (((ADDEDNumberOne < 10 && ADDEDNumberTen < 10) || ADDEDNumberOne < 10) && finshBtn)
        {
            if (ADDEDNumberTen > 10)
                ADDEDNumberTen = 0;
            finshBtn = false;
            Debug.Log(ADDEDNumberTen * 10 + ", " + ADDEDNumberOne + ", " + (ten * 10 + one + added));
            if (ADDEDNumberTen * 10 + ADDEDNumberOne == ten * 10 + one + added)
            {
                Debug.Log("Okay");
                C = StartCoroutine(Clear());
            }
            else
            {
                Debug.Log("Fail");
                F = StartCoroutine(Fail());
            }
        }
    }

    public Coroutine F;
    public Coroutine C;
    IEnumerator Fail()
    {
        SystemGame.Sounds[2].Play();
        yield return new WaitForSeconds(2.0f);
        RESET();
        if (stageLevel == 3 || stageLevel == 4)
            GameStart(4);
        else if (stageLevel == 2 || stageLevel == 5)
            GameStart(5);
        else if (stageLevel == 1 || stageLevel == 6)
            GameStart(6);
    }


    public IEnumerator Clear()
    {
        SystemGame.SoundStop();
        SystemGame.Sounds[4].Play();
        SystemGame.Sounds[1].Play();
        yield return new WaitForSeconds(2.0f);
        Debug.Log("clear");
        RESET();
        if (stageLevel == 3 || stageLevel == 4)
            GameStart(3);
        else if (stageLevel == 2 || stageLevel == 5)
            GameStart(2);
        else if (stageLevel == 1 || stageLevel == 6)
            GameStart(1);
    }

    public void RESET()
    {
        tenField.transform.parent.localScale = Vector3.one;
        oneField.transform.parent.localScale = Vector3.one;
        BtnScale(100);
        StopAllCoroutines();
        tenField.text = "?";
        oneField.text = "?";
        ADDEDNumberTen = 123;
        ADDEDNumberOne = 123;
        temptext = null;
        finshBtn = true;
        NowNum = -1;
    }

    public void REStart()
    {
        RESET();
        if (stageLevel == 3 || stageLevel == 4)
            GameStart(3);
        else if (stageLevel == 2 || stageLevel == 5)
            GameStart(2);
        else if (stageLevel == 1 || stageLevel == 6)
            GameStart(1);
    }
}
