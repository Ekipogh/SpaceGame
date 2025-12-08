using UnityEngine;
using UnityEngine.InputSystem;

public class OnMouseManager : MonoBehaviour
{
    public InputActionAsset actions;
    private InputAction clickAction;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Ensure there's a collider
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<CircleCollider2D>();
        }

        // Get click action from InputActionAsset
        clickAction = actions.FindAction("Click");
        clickAction.performed += OnClick;
        clickAction.Enable();
    }

    void OnDestroy()
    {
        if (clickAction != null)
        {
            clickAction.performed -= OnClick;
            clickAction.Disable();
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            OnCelestialBodyClicked();
        }
    }

    private void OnCelestialBodyClicked()
    {
        Debug.Log("Clicked on " + gameObject.name);
        // Add your click handling logic here
    }
}