using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box1Anim : MonoBehaviour
{
    public Animator anim;
    bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    public void OpenBox()
    {
        anim.SetTrigger("isOpened");
        isOpened = true;
    }

    public void CloseBox()
    {
        anim.SetTrigger("isClosed");
        isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            OpenBox();
        }

        else if (Input.GetKeyDown(KeyCode.E) && isOpened)
        {
            CloseBox();
        }
    }
}
