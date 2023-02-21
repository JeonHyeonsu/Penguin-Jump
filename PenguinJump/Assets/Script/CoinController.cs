using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int scoreValue = 1;  // 코인 수집 시 증가할 스코어 값
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // 충돌한 오브젝트가 플레이어인 경우
        {
            GameManager.instance.ChangeScore(scoreValue);  // 스코어 증가
            Destroy(gameObject);  // 코인 오브젝트 삭제
        }
    }
}
