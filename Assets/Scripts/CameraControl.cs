using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private enum MoveDirection {foward, backward, left, right}
    private bool moveInTargetDirection = false;
    
    [SerializeField] private Camera cam;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private MoveDirection moveDirection;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveInTargetDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.left:
                    cam.GetComponent<Transform>().position = new Vector3(cam.transform.position.x - (cameraSpeed * Time.deltaTime), cam.transform.position.y, cam.transform.position.z);
                    break;
                case MoveDirection.right:
                    cam.GetComponent<Transform>().position = new Vector3(cam.transform.position.x + (cameraSpeed * Time.deltaTime), cam.transform.position.y, cam.transform.position.z);
                    break;
                case MoveDirection.backward:
                    cam.GetComponent<Transform>().position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z - (cameraSpeed * Time.deltaTime));
                    break;
                case MoveDirection.foward:
                    cam.GetComponent<Transform>().position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + (cameraSpeed * Time.deltaTime));
                    break;
                default:
                    break;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        moveInTargetDirection = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        moveInTargetDirection = false;
    }
}
