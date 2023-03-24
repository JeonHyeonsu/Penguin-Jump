using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Rank;
    public Text gameTime;
    public Text hitCount;
    public Text coinCount;
    public GameObject NextStageText;

    private float gameTimeP;
    private int hitCountP;
    private int coinCountP;

    private int score;

    private int stageNumber;

    void Awake()
    {
        Debug.Log("PlayerPrefs Stage : " + PlayerPrefs.GetInt("Stage"));
        stageNumber = PlayerPrefs.GetInt("Stage") - 1;

        Debug.Log("stageNumber : " + stageNumber);

        gameTimeP = PlayerPrefs.GetFloat("GameTime_Stage_" + stageNumber);
        hitCountP = PlayerPrefs.GetInt("HitCount_Stage_" + stageNumber);
        coinCountP = PlayerPrefs.GetInt("CoinCount_Stage_" + stageNumber);

    }

    void Start()
    {
        StartCoroutine(ClearUI());
    }

    IEnumerator ClearUI()
    {
        yield return new WaitForSeconds(1f);

        gameTime.text = "플레이 타임 : " + gameTimeP.ToString("N1");

        yield return new WaitForSeconds(1f);

        hitCount.text = "방해물과 충돌 횟수 : " + hitCountP.ToString() + "회";

        yield return new WaitForSeconds(1f);

        coinCount.text = "코인 수집률 : " + coinCountP.ToString() + " % ";

        yield return new WaitForSeconds(2f);

        RankUpdate();

        yield return new WaitForSeconds(1f);

        NextStageText.SetActive(true);

    }

    public void RankUpdate()
    {
        score = coinCountP - (((int)gameTimeP / 10) + (hitCountP / 5));

        if(score > 90 && score <= 100)
        {
            Rank.text = "<S>";
            PlayerPrefs.SetString("Rank_Stage_" + stageNumber, "S");
        }
        else if(score > 80 && score <= 90)
        {
            Rank.text = "<A>";
            PlayerPrefs.SetString("Rank_Stage_" + stageNumber, "A");
        }
        else if (score > 70 && score <= 80)
        {
            Rank.text = "<B>";
            PlayerPrefs.SetString("Rank_Stage_" + stageNumber, "B");
        }
        else if (score > 60 && score <= 70)
        {
            Rank.text = "<C>";
            PlayerPrefs.SetString("Rank_Stage_" + stageNumber, "C");
        }
        else if (score > 50 && score <= 60)
        {
            Rank.text = "<D>";
            PlayerPrefs.SetString("Rank_Stage_" + stageNumber, "D");
        }
        else
        {
            Rank.text = "<F>";
            PlayerPrefs.SetString("Rank_Stage_" + stageNumber, "F");
        }
    }

    private void NextStage()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if( touch.phase == TouchPhase.Began )
            {
                SceneManager.LoadScene("GameScene");
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void Update()
    {
        if(NextStageText.activeSelf == true)
        {
            NextStage();
        }
    }
}
