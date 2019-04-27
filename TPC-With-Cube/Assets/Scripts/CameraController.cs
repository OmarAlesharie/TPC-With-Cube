using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float Away;
    public float Up;
    public float cameraSmothFollow;

    [SerializeField]
    private Vector2 verticalViewLimits = new Vector2(-30, 40);

    private Transform Player;
    private Transform rootOfPlayer;
    private Vector3 Target;
    private Transform TargetTransform;
    private float mouseX, mouseY;
    private float StartedmouseX, StartedmouseY;

    // Start is called before the first frame update
    void Start()
    {
         
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rootOfPlayer = new GameObject("rootOfPlayer").transform;

        rootOfPlayer.position = Player.position;
        TargetTransform = new GameObject("TargetTransform").transform;

        SetTargetPosition();

        transform.position = Vector3.Lerp(transform.position, TargetTransform.position, cameraSmothFollow * Time.deltaTime);
        transform.LookAt(Player.position);

        StartedmouseX = Input.GetAxis("Mouse X");
        StartedmouseY = Input.GetAxis("Mouse Y");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetTargetPosition()
    {
        TargetTransform.position = rootOfPlayer.position + Vector3.up * Up - rootOfPlayer.forward * Away;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.Q))
        {
            mouseX = 0f;
            mouseY = 0f;
            rootOfPlayer.rotation = Quaternion.Euler(Player.rotation.x, Player.rotation.y, 0f);
        }

        mouseY = Mathf.Clamp(mouseY, verticalViewLimits.x, verticalViewLimits.y);

        if (mouseX-StartedmouseX != Mathf.Epsilon || mouseY-StartedmouseY != Mathf.Epsilon)
        {
            rootOfPlayer.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        }
        
        rootOfPlayer.SetPositionAndRotation(Player.position, rootOfPlayer.rotation);

        SetTargetPosition();
        WallCollision();

        transform.position = Vector3.Lerp(transform.position, TargetTransform.position, cameraSmothFollow*Time.deltaTime);
        transform.LookAt(Player.position);
    }

    private void WallCollision()
    {
        RaycastHit Hit;
        if (Physics.Linecast(rootOfPlayer.position, TargetTransform.position, out Hit))
        {
            TargetTransform.position = new Vector3(Hit.point.x, TargetTransform.position.y,Hit.point.z);
        }
    }
}
