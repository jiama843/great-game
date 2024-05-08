using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyItem : MonoBehaviour
{

    public int health = 3; // number of clicks to clean

    private Animator anim;

    void Awake(){
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            anim.SetBool("isClean", true);
    }

    void OnMouseDown(){
        health -= 1;
        anim.SetTrigger("onClean");
    }
}
