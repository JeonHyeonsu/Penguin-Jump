using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject PauseMenuSet; // 환경설정 메뉴창

    //게임 종료버튼
    public void gameExit()
    {
        Application.Quit();
    }

    //환경설정 버튼을 눌렀을 경우 메뉴를 불러옴
    public void CallMenu()
    {
        PauseMenuSet.SetActive(true);
        Time.timeScale = 0;
    }

    //메뉴 닫기
    public void CloseMenu()
    {
        PauseMenuSet.SetActive(false);
        Time.timeScale = 1;
    }

    //시작 화면으로 이동
    public void SceneExit()
    {
        //SceneManager.LoadScene("StartScenes");
    }
}
