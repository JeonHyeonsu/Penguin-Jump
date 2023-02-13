using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{

    public GameObject Player;
    public Transform Hole;
    public bool telpoChk = false;

    // Update is called once per frame
    void Start()
    {

    }

    void Update()
    {
        if (telpoChk)
        {
            Player.GetComponent<PlayerMove>().PlayerScale();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = collision.gameObject;
            //Player.transform.position = Hole.transform.position;
            //Player.transform.position = new Vector2(Hole.position.x, Hole.position.y);
            Debug.Log("블랙홀");
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return null;
        Player.GetComponent<PlayerMove>().isDamaged = true;
        telpoChk = true;
        yield return new WaitForSeconds(2f);
        Player.transform.position = Hole.transform.position;
        Player.GetComponent<PlayerMove>().isDamaged = false;
        telpoChk = false;
        Player.GetComponent<PlayerMove>().OnDisableScale();
    }
}
