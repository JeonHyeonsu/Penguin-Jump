using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager instance;
    public GameObject[] level;
    public Transform[] levelposition;
    public GameObject[] nextlevel;
    //스테이지 넘버
    public int levelIndex = 1;

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
        Instantiate(level[1], levelposition[0].position, levelposition[0].rotation);
        Instantiate(level[0]);
    }

    public void NextLevel()
    {
        //levelAt를 증가시켜서 다음 스테이지 해금
        if ((levelIndex + 1) > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("level", (levelIndex + 1));
        }
    }

}
