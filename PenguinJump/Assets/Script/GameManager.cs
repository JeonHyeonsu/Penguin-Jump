using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager instance;



    public GameObject[] level;
    public Transform[] levelposition;
    public GameObject[] nextlevel;
    public GameObject[] coin;

    //스테이지 넘버
    public int levelIndex;



    //게임 진행시간 표시
    //public Text timetext;
    float time;
    float score;

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
        //Instantiate(level[1], levelposition[0].position, levelposition[0].rotation);
        Instantiate(level[PlayerPrefs.GetInt("level")]);
        Debug.Log("level : " + PlayerPrefs.GetInt("level"));
    }

    public void NextLevel()
    {
        //levelAt를 증가시켜서 다음 스테이지 해금
        if ((levelIndex + 1) > PlayerPrefs.GetInt("level"))
        {
            PlayerPrefs.SetInt("level", (levelIndex + 1));
            Debug.Log("level올림 : " + PlayerPrefs.GetInt("level") + "levelIndex : " + levelIndex);
        }

        RestartStage();
    }

    public void RestartStage()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ChangeScore(int value)
    {
        score += value;
        //scoreText.text = "Score: " + score;  // UI Text 업데이트
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PlayerPrefs.DeleteKey("level");
        }

        time += Time.deltaTime;
        //timetext.text = "Time : " + time.ToString("N2");
    }

}
