using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandlesAnim : MonoBehaviour
{
    public Animator anim;
    bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    public void Handled()
    {
        anim.SetTrigger("isHandled");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Handled();
        }
    }
}
