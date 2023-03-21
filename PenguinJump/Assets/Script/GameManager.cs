using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager instance;


    public Slider coinSlider;
    public GameObject[] level;
    public Transform[] levelposition;
    public GameObject[] nextlevel;
    public Text coinText;

    //스테이지 넘버
    public int stageNumber;



    //게임 진행시간 표시
    //public Text timetext;
    public float cleartime = 0;
    public float gameTime;
    public int allCoin = 0;
    public int currentCoinScore = 0;
    public int coinScore = 0;
    public float maxCoin;
    public float minCoin;
    public static int hitCount = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재");
            Destroy(gameObject);
        }

    }

    void Start()
    {
        Debug.Log("PlayerPrefs Stage : " + PlayerPrefs.GetInt("Stage"));

        //Instantiate(level[1], levelposition[0].position, levelposition[0].rotation);
        Instantiate(level[PlayerPrefs.GetInt("Stage")]);
        ResetSlider();
    }

    public void GameEnd()
    {

        coinScore = (int)((float)currentCoinScore / (float)allCoin * 100);


        // 게임 결과 정보 저장
        PlayerPrefs.SetFloat("GameTime_Stage_" + stageNumber, gameTime);
        PlayerPrefs.SetInt("CoinCount_Stage_" + stageNumber, coinScore);
        PlayerPrefs.SetInt("HitCount_Stage_" + stageNumber, hitCount);
        PlayerPrefs.Save();

        Debug.Log("GameTime : " + PlayerPrefs.GetFloat("GameTime_Stage_" + stageNumber));
        Debug.Log("CoinCount : " + PlayerPrefs.GetInt("CoinCount_Stage_" + stageNumber));
        Debug.Log("HitCount : " + PlayerPrefs.GetInt("HitCount_Stage_" + stageNumber));

        //Stage를 증가시켜서 다음 스테이지 해금
        //if ((stageNumber + 1) > PlayerPrefs.GetInt("Stage"))
        //{
            PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage") + 1);
            stageNumber += 1;
        //}

        SceneManager.LoadScene("StageClear");
    }

    public void NextLevel()
    {
        // 게임 결과 정보 불러오기
        //float gameTime = PlayerPrefs.GetFloat("GameTime_Stage_" + stageNumber);
        //int coinCount = PlayerPrefs.GetInt("CoinCount_Stage_" + stageNumber);
        //int hitCount = PlayerPrefs.GetInt("HitCount_Stage_" + stageNumber);

        RestartStage();
    }

    public void RestartStage()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ChangeScore(int value)
    {
        currentCoinScore += value;
        coinSlider.value = Mathf.Clamp(currentCoinScore, coinSlider.minValue, allCoin);
    }

    public void CoinCount(int value)
    {
        allCoin += value;
    }

    public int HitCount(int value)
    {
        hitCount += value;
        return hitCount;
    }

    public void ResetSlider()
    {
        maxCoin = allCoin;
        minCoin = 0;
        coinSlider.maxValue = maxCoin;
        coinSlider.minValue = minCoin;
    }


    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs DeleteKey");
        }

        gameTime += Time.deltaTime;
        //timetext.text = "Time : " + time.ToString("N2");

        //coinText.text = currentCoinScore.ToString() + " / " + allCoin.ToString();
        coinText.text = ((float)currentCoinScore / (float)allCoin * 100).ToString("N1") + " % ";
    }

}
