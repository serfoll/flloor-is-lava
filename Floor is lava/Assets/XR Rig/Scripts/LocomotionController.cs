using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{

    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    public bool enableRightTeleport { get; set; } = true;
    public bool enableLeftTeleport { get; set; } = true;

    public XRRayInteractor rightRayInteractor;
    public XRRayInteractor leftRayInteractor;

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {

        Vector3 pos = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validTarget = false;

        if ( leftTeleportRay)
        {
            bool isLeftRayInteractorRayHovering = 
                leftRayInteractor.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);

            leftTeleportRay.gameObject.SetActive (enableLeftTeleport && CheckIfActivated (leftTeleportRay) && !isLeftRayInteractorRayHovering);
        }
        
        if ( rightTeleportRay )
        {
            bool isRightRayInteractorRayHovering =
                rightRayInteractor.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);

            rightTeleportRay.gameObject.SetActive (enableRightTeleport && CheckIfActivated (rightTeleportRay) && !isRightRayInteractorRayHovering);
        }
    }

    public bool CheckIfActivated (XRController controller)
    {
        InputHelpers.IsPressed (controller.inputDevice , 
            teleportActivationButton , 
            out bool isActivated , activationThreshold);

        return isActivated;
    }
}
