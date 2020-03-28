using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour
{
    public float speed;
    public bool canWalk;
    private Animator an;
    private void Start()
    {
        //speed = 1;
        an = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (canWalk)
        {
            an.SetBool("run", true);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

    }
}
