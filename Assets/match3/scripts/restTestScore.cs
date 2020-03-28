using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restTestScore : MonoBehaviour
{
    candycrush cc;
    void Start()
    {
        cc = GetComponent<candycrush>();
    }

    
    void Update()
    {
        cc.score = 0;
    }
}
