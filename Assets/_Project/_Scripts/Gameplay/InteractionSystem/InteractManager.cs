using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractManager : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Camera cam;

    [Header("Settings")] 
    [SerializeField] private float range;
    [SerializeField] private TMP_Text text;
    
    private IInteractable _interactable;

    // Start is called before the first frame update
    private void Start()
    {
        PlayerInputManager.Input.General.Interact.performed += Interact;
    }

    void Interact(InputAction.CallbackContext callbackContext) => _interactable?.Interact();

    // Update is called once per frame
    void Update()
    {
        if (text)
            text.gameObject.SetActive(_interactable != null);
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range))
        {
            if (hit.transform.gameObject.TryGetComponent(out IInteractable newInteractable))
            {
                if (_interactable != null && _interactable != newInteractable)
                {
                    _interactable.OnDeselect();
                    _interactable = null;
                }

                _interactable = newInteractable;
                _interactable.OnSelect();
                return;
            }
        }

        if (_interactable != null)
        {
            _interactable.OnDeselect();
            _interactable = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * range);
    }
}
