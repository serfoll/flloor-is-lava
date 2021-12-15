using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerInput : MonoBehaviour
{

    [SerializeField]
    private XRNode leftInputSource;
    [SerializeField]
    private XRNode rightInputSource;

    private InputDevice leftDevice;
    private InputDevice rightDevice;

    private bool RPrimaryBtnPressed;
    private Vector2 LInputAxis;
    private bool RSecondaryBtnPressed;
    ContinuousMovement movement;

    int rSecBtnCount;


    // Start is called before the first frame update
    void Start()
    {
        movement = FindObjectOfType<ContinuousMovement>();
        leftDevice = InputDevices.GetDeviceAtXRNode(leftInputSource);
        rightDevice = InputDevices.GetDeviceAtXRNode(rightInputSource);

        rSecBtnCount = 0;
    }

    void Update()
    {
        rightDevice.TryGetFeatureValue(CommonUsages.primaryButton, out RPrimaryBtnPressed);
        leftDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out LInputAxis);
        rightDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out RSecondaryBtnPressed);
        rightDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out RSecondaryBtnPressed);
    }

    private void FixedUpdate()
    {
        

        if (GameController.gameIsRunning && !GameController.isGameOver)
        {
            movement.jumpBtn = RPrimaryBtnPressed;
            movement.inputAxis = LInputAxis;
        }

        if (GameController.isGameOver && RSecondaryBtnPressed )
        {
            GameController.RestartGame();
        }

        if (GameController.isGameOver && RPrimaryBtnPressed )
        {
            GameController.QuitGame();
        }
    }

}
