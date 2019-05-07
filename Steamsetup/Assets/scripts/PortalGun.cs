using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class PortalGun : MonoBehaviour


{
    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");
    public event PointerEventHandler PointerIn;
    public event PointerEventHandler PointerOut;
    public event PointerEventHandler PointerClick;
    public GameObject leftPortal, rightPortal;
    public bool triggerButtonDown = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;





            // Start is called before the first frame update
            void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactWithUI == null)
            Debug.LogError("No ui interaction action has been set on this component.");
        if (interactWithUI.GetState(pose.inputSource))
        {
            ThrowPortal(leftPortal);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            ThrowPortal(rightPortal);
        }
    }

    void ThrowPortal(GameObject portal)
    {
        portal.GetComponent<Portal>().portalOn = true;

        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            portal.transform.position = hit.point;
            portal.transform.rotation = Quaternion.LookRotation(hit.normal);
        }
    }
}

