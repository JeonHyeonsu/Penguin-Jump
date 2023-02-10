using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] level;
    public Transform[] levelposition;



    void Start()
    {
        Instantiate(level[0], levelposition[0].position, levelposition[0].rotation);
    }

}
