using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOpener : MonoBehaviour
{
    public Animator anim;
    bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenBox()
    {
        anim.SetTrigger("openTrigger");
        isOpened = true;
        anim.SetBool("isOpen", true);
    }

    public void CloseBox()
    {
        anim.SetTrigger("closeTrigger");
        isOpened = false;
        anim.SetBool("isOpen", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
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
