using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * 90);
    }
}
