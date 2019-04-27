using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCharacterController : MonoBehaviour
{
    private float Horizontal = 0f;
    private float Vertical = 0f;

    [SerializeField]
    private float Speed = 3f;

    private Transform CameraTransform;

    private Vector3 InputDirection;
    private Vector3 MoveDirection;
    private Vector3 CamDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (!CameraTransform)
        {
            CameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        
        InputDirection = new Vector3(Horizontal, 0.0f, Vertical);

        CamDirection = CameraTransform.forward;
        CamDirection.y = 0f;

        Quaternion ReferenceRotation = Quaternion.FromToRotation(Vector3.forward, CamDirection);

        MoveDirection = ReferenceRotation * InputDirection;

        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        Debug.DrawRay(transform.position, MoveDirection, Color.red);

        if (Horizontal != 0f || Vertical != 0f)
        {
            transform.forward = MoveDirection;
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
    }
}
