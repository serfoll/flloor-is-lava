using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    [SerializeField] bool showController;
    [SerializeField] InputDeviceCharacteristics controllerCharacteristics;
    [ SerializeField] List<GameObject> controllerPrefabs;
    [SerializeField] GameObject handModelPrefab;
    

    public InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;



    // Start is called before the first frame update
    void Start()
    {
        TryInitialize ();
    }


    void TryInitialize ()
    {
        //Create a list called devices of type InputDevice
        List<InputDevice> devices = new List<InputDevice> ();

        //Populat devices list 
        InputDevices.GetDevicesWithCharacteristics (controllerCharacteristics , devices);

        //if items in list greaten then 0, set targetDevice to the first item in the list
        if ( devices.Count > 0 )
        {
            targetDevice = devices[ 0 ];
            //find and get the propper device controller by using the targetDevice name
            GameObject prefab =
                controllerPrefabs.Find (controller => controller.name == targetDevice.name);

            //assign the prefab to spawnController if it's found
            if ( prefab )
            {
                spawnedController = Instantiate (prefab , transform);
            }
            else
            {
                Debug.LogError ("Corresponding controller model not found! ");
                spawnedController = Instantiate (controllerPrefabs[ 0 ] , transform);
            }

            spawnedHandModel = Instantiate (handModelPrefab , transform);
            handAnimator = spawnedHandModel.GetComponent<Animator> ();
        }
    }
    //Update hand animation
    void UpdateHandAnimation ()
    {
        if ( targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) )
        {
            handAnimator.SetFloat ("Trigger" , triggerValue);
        }
        else
        {
            handAnimator.SetFloat ("Trigger" , 0);

        }

        if ( targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) )
        {
            handAnimator.SetFloat ("Grip" , gripValue);
        }else
            handAnimator.SetFloat ("Grip" , 0);

    }

    // Update is called once per frame
    void Update()
    {

        if ( !targetDevice.isValid )
        {
            TryInitialize ();
        }
        else
        {
            if ( showController )
            {
                spawnedController.SetActive (true);
                spawnedHandModel.SetActive (false);
            }
            else
            {
                spawnedController.SetActive (false);
                spawnedHandModel.SetActive (true);
                UpdateHandAnimation ();
            }
        }

    }
}
