using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    //public List<Sprite> Text = new List<Sprite>();

    public List<AudioSource> Sounds = new List<AudioSource>();

    public GameObject Stage;

    public GameObject GameStage;

    public GameObject Hgame;
    public GameObject Igame;
    public GameObject Jgame;

    public GameObject ui;

    public Transform Parent;

    public List<Transform> RemoveList = new List<Transform>();

    public GameObject ADDNumbers;
    public GameObject MinNumbers;

    public Image BGSprite;
    public List<Sprite> BGs = new List<Sprite>();

    public RectTransform StopBtn;
    //public List<RectTransform> StopPoints = new List<RectTransform>();

    public GameObject Back;

    [System.NonSerialized]
    public bool click = true;

    int StageNum;

    void Start()
    {
        Screen.SetResolution(1280, 800, true);
    }

    public void StartGame(int index)
    {
        Back.SetActive(false);
        StageNum = index;
        BGSprite.sprite = BGs[0];
        //StopBtn.position = StopPoints[0].position;
        switch (StageNum)
        {
            case 1:
                GameStage.GetComponent<ADD>().GameStart(1);
                BGSprite.sprite = BGs[1];
                //StopBtn.position = StopPoints[1].position;
                break;
            case 2:
                GameStage.GetComponent<ADD>().GameStart(2);
                BGSprite.sprite = BGs[1];
                //StopBtn.position = StopPoints[1].position;
                break;
            case 3:
                GameStage.GetComponent<ADD>().GameStart(3);
                BGSprite.sprite = BGs[1];
                //StopBtn.position = StopPoints[1].position;
                break;
            case 4:
                GameStage.GetComponent<Min>().GameStart(1);
                BGSprite.sprite = BGs[1];
                //StopBtn.position = StopPoints[1].position;
                break;
            case 5:
                GameStage.GetComponent<Min>().GameStart(2);
                BGSprite.sprite = BGs[1];
                //StopBtn.position = StopPoints[1].position;
                break;
            case 6:
                Hgame.GetComponent<Hgame>().HgameStart();
                break;
            case 7:
                Igame.GetComponent<Igame>().IgameStart();
                break;
            case 8:
                Jgame.GetComponent<Jgame>().JgameStart();
                break;
        }
        Stage.SetActive(false);
    }

    public void GameStop()
    {
        click = false;
        ui.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReStartGame()
    {
        click = true;
        ui.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void GoToMain()
    {
        StopAllCoroutines();
        //if (GameStage.GetComponent<ADD>().F != null)
        //    GameStage.GetComponent<ADD>().StopCoroutine(GameStage.GetComponent<ADD>().F);

        //if (GameStage.GetComponent<ADD>().C != null)
        //    GameStage.GetComponent<ADD>().StopCoroutine(GameStage.GetComponent<ADD>().C);

        //if (GameStage.GetComponent<Min>().F != null)
        //    GameStage.GetComponent<Min>().StopCoroutine(GameStage.GetComponent<Min>().F);

        //if (GameStage.GetComponent<Min>().C != null)
        //    GameStage.GetComponent<Min>().StopCoroutine(GameStage.GetComponent<Min>().C);

        BGSprite.sprite = BGs[0];
        click = true;
        
        SoundStop();
        for (int i = 0; i < Parent.childCount; i++)
            Destroy(Parent.GetChild(i).gameObject);
        for (int i = 0; i < RemoveList.Count; i++)
            RemoveList[i].gameObject.SetActive(false);

        GameStage.GetComponent<ADD>().RESET();
        GameStage.GetComponent<Min>().RESET();

        Hgame.SetActive(false);
        Hgame.GetComponent<Hgame>().LeftText.transform.parent.gameObject.SetActive(false);
        Hgame.GetComponent<Hgame>().Box.index = 0;
        Hgame.GetComponent<Hgame>().MainText.sprite = Hgame.GetComponent<Hgame>().Numbers[0];
        Hgame.GetComponent<Hgame>().UpMessage.gameObject.SetActive(false);

        Igame.SetActive(false);
        Igame.GetComponent<Igame>().LeftText.transform.parent.gameObject.SetActive(false);
        Igame.GetComponent<Igame>().LBox.index = 0;
        Igame.GetComponent<Igame>().RBox.index = 0;
        Igame.GetComponent<Igame>().LeftText.sprite = Igame.GetComponent<Igame>().Numbers[0];
        Igame.GetComponent<Igame>().RightText.sprite = Igame.GetComponent<Igame>().Numbers[0];
        Igame.GetComponent<Igame>().UpMessage.gameObject.SetActive(false);

        Jgame.GetComponent<Jgame>().Count = 0;
        Jgame.SetActive(false);

        Stage.SetActive(true);
        ui.SetActive(false);
        ADDNumbers.SetActive(false);
        MinNumbers.SetActive(false);
        Back.SetActive(true);
        Time.timeScale = 1;
    }

    public GameObject Main;
    public GameObject Stop;
    public void StartStage()
    {
        Main.SetActive(false);
        Stage.SetActive(true);
        Stop.SetActive(true);
        Back.SetActive(true);
    }

    public GameObject Exit;
    public void ExitPopUp()
    {
        Exit.SetActive(true);
    }

    public void End(bool check)
    {
        if (check)
            Application.Quit();
        else
            Exit.SetActive(false);
    }

    public void SoundStop()
    {
        for (int i = 0; i < Sounds.Count; i++)
        {
            Sounds[i].Stop();
        }
    }

    bool soundscale = true;

    public List<Sprite> btn = new List<Sprite>();
    public void SoundScale(Image obj)
    {
        for (int i = 0; i < Sounds.Count; i++)
        {
            if (soundscale)
            {
                Sounds[i].volume = 0;
            }
            else
            {
                Sounds[i].volume = 1;
                if (i == 4)
                    Sounds[i].volume = 0.3f;
            }
        }
        soundscale = !soundscale;
        obj.sprite = btn[(int)Sounds[0].volume];
    }

    public void BackBtn()
    {
        Main.SetActive(true);
        Stage.SetActive(false);
        Stop.SetActive(false);
        Back.SetActive(false);
    }
}
