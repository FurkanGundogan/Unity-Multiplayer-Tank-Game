using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : NetworkBehaviour
{
    [SerializeField] private Transform playerCameraTransform = null;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float screenBorderThickness = 10f;
    [SerializeField] private Vector2 screenXLimits = Vector2.zero;
    [SerializeField] private Vector2 screenZLimits = Vector2.zero;
     

    private Controls controls;
    private Vector2 prevInput;

    public override void OnStartAuthority()
    {
        playerCameraTransform.gameObject.SetActive(true);
        controls = new Controls();
        controls.Player.MoveCamera.performed+=SetPrevInput;
        controls.Player.MoveCamera.canceled+=SetPrevInput;
        controls.Enable();
    }

    [ClientCallback]
    private void Update() {
        if(!hasAuthority || !Application.isFocused) return;
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector3 pos = playerCameraTransform.position;
        if(prevInput == Vector2.zero){
            Vector3 cursorMovement = Vector3.zero;
            Vector2 cursorPos = Mouse.current.position.ReadValue();
            if(cursorPos.y >= Screen.height - screenBorderThickness){
                cursorMovement.z+=1;
            }else if(cursorPos.y <= screenBorderThickness){
                cursorMovement.z -=1;
            }

            if(cursorPos.x >= Screen.width - screenBorderThickness){
                cursorMovement.x+=1;
            }else if(cursorPos.x <= screenBorderThickness){
                cursorMovement.x -=1;
            }

            pos += cursorMovement.normalized*speed*Time.deltaTime;

        }else{
            pos += new Vector3(prevInput.x,0f,prevInput.y)*speed*Time.deltaTime;
        }

        pos.x=Mathf.Clamp(pos.x,screenXLimits.x,screenXLimits.y);
        pos.z=Mathf.Clamp(pos.z,screenZLimits.x,screenZLimits.y);

        playerCameraTransform.position = pos;
    }

    private void SetPrevInput(InputAction.CallbackContext ctx){
        prevInput = ctx.ReadValue<Vector2>();
    }
}
