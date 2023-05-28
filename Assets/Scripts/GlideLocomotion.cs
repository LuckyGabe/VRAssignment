using UnityEngine;

public class GlideLocomotion : MonoBehaviour
{
    public Transform rigRoot;
    public float velocity = 5f; // meters per second
    public float rotationSpeed = 70f; // degrees per second
    private Vector3 targetMoveDirection = Vector3.zero;
    private float targetRotation = 0f;
    private float smoothingFactor = 10f; // Adjust value to get the desired smoothing
    public Transform trackedTransform; // camera or controller, null for thumbstick

    public Camera vrCamera; // Assign VR camera in the inspector
    private float normalFOV;
    private float comfortFOV = 20f; // Adjust value to get the desired FOV in comfort mode

    private void Start()
    {
        if (rigRoot == null) rigRoot = transform;
        normalFOV = vrCamera.fieldOfView;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        float forward = Input.GetAxis("XRI_Right_Primary2DAxis_Vertical");
        float sideways = Input.GetAxis("XRI_Right_Primary2DAxis_Horizontal");

        if (forward != 0f)
        {
            Vector3 newMoveDirection = trackedTransform.localRotation * Vector3.forward;
            newMoveDirection *= -forward * velocity * deltaTime;
            targetMoveDirection = Vector3.Lerp(targetMoveDirection, newMoveDirection, deltaTime * smoothingFactor);
            rigRoot.Translate(targetMoveDirection);
        }

        if (sideways != 0f)
        {
            float newRotation = sideways * rotationSpeed * deltaTime;
            targetRotation = Mathf.Lerp(targetRotation, newRotation, deltaTime * smoothingFactor);
            rigRoot.Rotate(0, targetRotation, 0);
        }

        if (forward != 0f || sideways != 0f)
        {
            // If the player is moving, reduce the FOV and apply the movement and rotation
            vrCamera.fieldOfView = Mathf.Lerp(vrCamera.fieldOfView, comfortFOV, deltaTime * smoothingFactor);
        }
        else
        {
            // If the player is not moving, restore the normal FOV
            vrCamera.fieldOfView = Mathf.Lerp(vrCamera.fieldOfView, normalFOV, deltaTime * smoothingFactor);
        }

    }
}
