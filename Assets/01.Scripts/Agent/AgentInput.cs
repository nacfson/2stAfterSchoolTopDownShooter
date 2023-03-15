using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class AgentInput : MonoBehaviour, IAgentInput
{
    [field:SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPress { get ; set; }
    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerpositionChanged { get ; set ; }
    [field: SerializeField]
    public UnityEvent OnFireButtonPress { get ; set  ; }
    [field: SerializeField]
    public UnityEvent OnFireButtonRelease { get; set ; }

    private bool _fireButtonDown = false;

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();

    }

    private void GetFireInput()
    {
        if(Input.GetAxisRaw("Fire1") > 0 )
        {
            if(_fireButtonDown == false)
            {
                _fireButtonDown = true;
                OnFireButtonPress?.Invoke();

            }
            else
            {
                if(_fireButtonDown == true)
                {
                    _fireButtonDown = false;
                    OnFireButtonRelease?.Invoke();
                }
            }
        }
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;
        Vector2 mouseInWorldPos = MainCam.ScreenToWorldPoint(mousePos); //이 코드는 좋지 않음
        OnPointerpositionChanged?.Invoke(mouseInWorldPos);
    }

    private void GetMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal"), v = Input.GetAxisRaw("Vertical");
        OnMovementKeyPress?.Invoke(new Vector2(h, v));
    }
}
