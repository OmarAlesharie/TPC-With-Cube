using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCharacterController : MonoBehaviour
{
    private float Horizontal = 0f;
    private float Vertical = 0f;

    [SerializeField]
    private float Speed = 3;

    [SerializeField]
    Transform CameraTransform;
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
        
        Vector3 direction = new Vector3(Horizontal, 0.0f, Vertical);
        Vector3 RelativePosition = new Vector3(CameraTransform.position.x, CameraTransform.position.y, 0.0f);
        
        transform.Translate(direction * Speed * Time.deltaTime, CameraTransform);
    }
}
