using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public InputActionAsset actions;
    InputAction moveAction;
    InputAction zoomAction;
    Camera cameraComponent;

    private float minZoom = 2.0f;
    private float maxZoom = 50.0f;

    private float zoomSpeed = 20.0f;
    void Start()
    {
        moveAction = actions.FindAction("Move");
        moveAction.Enable();
        zoomAction = actions.FindAction("Zoom");
        zoomAction.Enable();
        cameraComponent = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        ZoomTowardsCursor();
    }

    private void MoveCamera()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = 10.0f * Time.deltaTime * new Vector3(moveInput.x, moveInput.y, 0);
        transform.position += move;
    }

    void ZoomTowardsCursor()
    {
        Camera cam = Camera.main;
        var zoomDelta = zoomAction.ReadValue<Vector2>().y * zoomSpeed * Time.deltaTime;
        if (Mathf.Approximately(zoomDelta, 0f))
            return;

        // Get world position of mouse before zoom
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = cam.nearClipPlane;
        Vector3 mouseWorldPosBefore = cam.ScreenToWorldPoint(mousePos);

        // Apply zoom (adjust orthographic size or position.z for perspective)
        cam.orthographicSize -= zoomDelta; // For 2D
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);

        // Get world position of mouse after zoom
        mousePos.z = cam.nearClipPlane;
        Vector3 mouseWorldPosAfter = cam.ScreenToWorldPoint(mousePos);

        // Calculate difference and adjust camera position
        Vector3 offset = mouseWorldPosBefore - mouseWorldPosAfter;
        cam.transform.position += offset;
    }
}
