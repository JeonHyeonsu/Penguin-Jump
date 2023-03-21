using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int scoreValue = 1;  // 코인 수집 시 증가할 스코어 값
    public int coincount = 1; //코인 갯수 체크


    void Awake()
    {
        GameManager.instance.CoinCount(coincount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // 충돌한 오브젝트가 플레이어인 경우
        {
            GameManager.instance.ChangeScore(scoreValue);  // 스코어 증가
            Destroy(gameObject);  // 코인 오브젝트 삭제
        }
    }
}
