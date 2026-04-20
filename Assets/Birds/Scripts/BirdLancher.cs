using UnityEngine;
using UnityEngine.InputSystem;

public class BirdLancher : MonoBehaviour, BirsActions.ILancherActions
{
    [SerializeField] private float launchSpeed = 5;
    [SerializeField] private int trajectoryPositions = 20;
    [SerializeField] private float trajectoryTimeStep = 0.1f;
    [SerializeField] private LineRenderer trajectoryLine;
    private BirsActions inputActions;
    private BirsActions.LancherActions lancherActions;
    private Vector3 mouseStartWorld;
    private Vector3 mousePosition;
    [SerializeField] private LayerMask quadLayer;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject birdPrefab;
    private bool isAiming;
    private bool isShooting;
    private Rigidbody birdRigidbody;

    void Start()
    {
        inputActions = new BirsActions();
        lancherActions = inputActions.Lancher;
        lancherActions.AddCallbacks(this);
        lancherActions.Enable();
    }



    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            DrawTrajectory();
        }
        if (isShooting)
        {
            if (birdRigidbody != null)
            {
                if (birdRigidbody.linearVelocity.magnitude < 0.1f)
                {
                    //bird stopped
                    isShooting = false;
                    birdRigidbody.isKinematic = true;
                    birdRigidbody.position = transform.position;
                }
            }
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
        if (isAiming)
        {
            lineRenderer.SetPosition(1, GetMousePositionWorld());
        }
    }

    public void OnMousePress(InputAction.CallbackContext context)
    {
        if(isShooting)
        {
            return;
        }

        if (context.started)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Bird"))
                {
                    mouseStartWorld = hit.point;
                    lineRenderer.SetPosition(0, mouseStartWorld);
                    lineRenderer.SetPosition(1, mouseStartWorld);
                    print(mouseStartWorld);
                    isAiming = true;
                    trajectoryLine.enabled = true;
                    lineRenderer.enabled = true;
                    birdRigidbody = hit.collider.GetComponent<Rigidbody>();

                }
            }
        }

        if (isAiming && context.canceled)
        {
            Vector3 currentMouseWorlds = GetMousePositionWorld();
            Vector3 delta = mouseStartWorld - currentMouseWorlds;
            birdRigidbody.isKinematic = false;
            birdRigidbody.linearVelocity = delta * launchSpeed;
            isAiming = false;
            trajectoryLine.enabled = false;
            lineRenderer.enabled = false;
            isShooting = true;
        }
    }

    private Vector3 GetMousePositionWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private void DrawTrajectory()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Vector3 delta = mouseStartWorld - GetMousePositionWorld();
        Vector3 velocity = delta * launchSpeed;

        trajectoryLine.positionCount = trajectoryPositions;
        for (int i = 0; i < trajectoryPositions; i++)
        {
            //pos(t) = startPos + v*t + gravity * t * t/2
            float t = trajectoryTimeStep * i;
            Vector3 position = birdRigidbody.position + velocity * t + Physics.gravity * t * t / 2;
            trajectoryLine.SetPosition(i, position);
        }

    }

}
