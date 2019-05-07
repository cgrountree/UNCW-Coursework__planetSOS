using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleporter : MonoBehaviour
{
    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_TeleportAction;

    private SteamVR_Behaviour_Pose m_Pose;
    private bool m_HasPosition = false;
    private bool m_IsTeleporting = false;
    private float m_FadeTime = 0.01f;
    private AudioSource theAudio;

    // Start is called before the first frame update
    private void Awake()
    {
        if (m_Pose == null)
            m_Pose = this.GetComponent<SteamVR_Behaviour_Pose>();

        
    }

    // Update is called once per frame
    private void Update()
    {
        // Pointer
        m_HasPosition = UpdatePointer();
        m_Pointer.SetActive(m_HasPosition);

        // Teleport
        if (m_TeleportAction.GetStateUp(m_Pose.inputSource))
            TryTeleport();
    }

    private void TryTeleport()
    {
        // Check for valid position, and if already teleporting
        if (!m_HasPosition || m_IsTeleporting)
            return;

        // Get camera rig, and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        // Figure out translation
        Vector3 pointOffset = new Vector3(0, 0, 2);
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 test = new Vector3(5, 5, 5);
        Vector3 translateVector = m_Pointer.transform.position - groundPosition;

        if (Mathf.Abs(translateVector.x) > 7 || Mathf.Abs(translateVector.z) >= 7 || Mathf.Abs(translateVector.y) >= 3)
        {
            if (translateVector.x < 0 && translateVector.z < 0)
            {
                translateVector.x += 3;
                translateVector.z += 3;
            }
            else if (translateVector.x < 0 && translateVector.z > 0)
            {
                translateVector.x += 3;
                translateVector.z -= 3;
            }
            else if (translateVector.x > 0 && translateVector.z < 0)
            {
                translateVector.x -= 3;
                translateVector.z += 3;
            }
            else
            {
                translateVector.x -= 3;
                translateVector.z -= 3;
            }

            if (translateVector.y < 0 && Mathf.Abs(translateVector.y) > 30)
            {
                translateVector.y += 8;
            }
            else if (translateVector.y > 0 && Mathf.Abs(translateVector.y) > 30)
            {
                translateVector.y -= 8;
            }
        }
            
        
        /*
        if (translateVector.x > 2 || translateVector.y > 2 || translateVector.z > 2)
        {
            if (m_Pointer.transform.position.y > cameraRig.position.y)
            {
                translateVector.y = translateVector.y - 2;
            }
            else if (m_Pointer.transform.position.y < cameraRig.position.y)
            {
                translateVector.y = translateVector.y + 2;
            }

            if (m_Pointer.transform.position.x > cameraRig.position.x)
            {
                translateVector.x = translateVector.x - 2;
            }
            else if (m_Pointer.transform.position.x < cameraRig.position.x)
            {
                translateVector.x = translateVector.x + 2;
            }

            if (m_Pointer.transform.position.z > cameraRig.position.z)
            {
                translateVector.z = translateVector.z - 2;
            }
            else if (m_Pointer.transform.position.z < cameraRig.position.z)
            {
                translateVector.z = translateVector.z + 2;
            }

            
        }
        */
        StartCoroutine(MoveRig(cameraRig, translateVector));

        // If 


        //if (cameraRig.transform.position.y > m_Pointer.transform.position.y)
        //{
        //    if (cameraRig.transform.position.y - m_Pointer.transform.position.y > 10)
        //    {
        //        translateVector.y += 5;
        //    }
        //    
        //}
        //else
        //{
        //    translateVector.y -= 5;
        //}



    }
    
    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        // Flag
        m_IsTeleporting = true;

        theAudio = GetComponent<AudioSource>();
        theAudio.Play();

        // Fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);

        // Apply translation
        yield return new WaitForSeconds(m_FadeTime);
        /*
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        Vector3 ptDist = new Vector3(Mathf.Abs(m_Pointer.transform.position.x), Mathf.Abs(m_Pointer.transform.position.y), Mathf.Abs(m_Pointer.transform.position.x));
        */
        
        cameraRig.position += translation;

        // Fade to clear
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);

        // De-flag
        m_IsTeleporting = false;
        
    }

    private bool UpdatePointer()
    {
        // Ray from the controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // If it's a hit
        if(Physics.Raycast(ray, out hit))
        {
            m_Pointer.transform.position = hit.point;
            return true;
        }

        // If not a hit

        return false;
    }
}
