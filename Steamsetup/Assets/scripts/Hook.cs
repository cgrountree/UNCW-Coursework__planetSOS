using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update

    // public RigidbodyFirstPersonController cc;
    public bool attached = false;
    public float speed;
    public SteamVR_Action_Boolean m_HookAction;
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;

    public Transform cam;
    private SteamVR_Behaviour_Pose Pose;
    private float momentum;
    private float step;
    private RaycastHit hit;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (m_HookAction.GetState(inputSource))
        {
            if (Physics.Raycast(cam.position.normalized, cam.forward, out hit))
            {
                attached = true;
                rb.isKinematic = true;
            }
            if (attached)
            {
                momentum += Time.deltaTime * speed;
                step = momentum * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, hit.point, step);

            }
            if (!attached && momentum >= 0)
            {
                momentum += Time.deltaTime * 5;
                step = 0;
            }
        }
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    if (Physics.Raycast (cam.position.normalized, cam.forward, out hit))
        //    {
        //        attached = true;
        //        rb.isKinematic = true;
        //    }
        //}
        //if (Input.GetButtonUp ("Fire1"))
        //{
        //    attached = false;
        //    rb.isKinematic = false;
        //    rb.velocity = cam.forward * momentum;
        
        //}
       // if (attached)
        //{
        //    momentum += Time.deltaTime * speed;
          //  step = momentum * Time.deltaTime;
         //   transform.position = Vector3.MoveTowards(transform.position, hit.point, step);

        //}
        //if (!attached && momentum >= 0) {
         //   momentum += Time.deltaTime * 5;
          //  step = 0;
        //}
        //if (cc.Grounded && momentum >= 0)
        //{
        //    momentum = 0;
        //    step = 0;
        //}
    }
}
