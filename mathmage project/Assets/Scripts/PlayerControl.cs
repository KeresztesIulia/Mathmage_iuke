using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour, ISaveable
{
    [SerializeField] Rigidbody player;
    Camera playerCamera;
    [Header("Movement")]
    [SerializeField] float movementSpeedNormal = 2;
    [SerializeField] float movementSpeedFast = 5;

    [Header("Rotation")]
    [SerializeField] float baseRotationSpeed = 20;
    [SerializeField] Vector2 cameraRotationLimitsNormal;
    [SerializeField] Vector2 cameraRotationLimitsFlipped;

    [Header("Other")]
    [SerializeField] float gravityRotationTime = 1;

    
    [SerializeField] InteractHandler interactHandler;
    string actualUser;
    float rotationSpeed;

    // gravity change stuff
    bool canChangeGravity = false;
    // bool rotating = false;
    int gravityRotationDirection = -1;
    Vector3 gravityStartRotation;
    float gravityEndRotationX;
    float timeInRotation = 10;
    


    void Start()
    {
        //player = GetComponent<Rigidbody>();
        playerCamera = player.GetComponentInChildren<Camera>();
        actualUser = PlayerPrefs.GetString(GameFunctions.usernameString);
    }

    void FixedUpdate()
    {
        if (player != null && playerCamera != null && GameFunctions.PlayerActive)
        {
            rotationSpeed = baseRotationSpeed * PlayerPrefs.GetInt(actualUser + "MouseSensitivity");
            MovePlayer();
            MoveCamera();
            //FindInteractable();
           // RotateForGravity();
        }
    }

    private void Update()
    {
        CheckForGravityChange();
        RotateForGravity();
    }

    private void OnEnable()
    {
        FloorCollider.EnteredFloor3 += delegate { canChangeGravity = true; };
        FloorCollider.LeftFloor3 += delegate
            {
                canChangeGravity = false;
                if (gravityRotationDirection == 1) ChangeGravity();
            };
    }

    private void OnDisable()
    {
        FloorCollider.EnteredFloor3 -= delegate { canChangeGravity = true; };
        FloorCollider.LeftFloor3 += delegate
        {
            canChangeGravity = false;
            if (gravityRotationDirection == 1) ChangeGravity();
        };
    }

    void MovePlayer()
    {
        float movementSpeed;
        if (Input.GetButton("Run"))
        {
            movementSpeed = movementSpeedFast;
        }
        else
        {
            movementSpeed = movementSpeedNormal;
        }

        if (timeInRotation > 1) 
        {

            Vector3 rotation = new Vector3(0, Input.GetAxisRaw("Mouse X"), 0);
            Vector3 rotationAmount = rotation * rotationSpeed * Time.fixedDeltaTime;
            player.transform.Rotate(rotationAmount, Space.Self);
        }

        Vector3 direction = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical") /* -gravityRotationDirection*/).normalized);
        Vector3 moveAmount = direction * movementSpeed * Time.fixedDeltaTime;
        player.MovePosition(player.position + moveAmount);
    }

    void MoveCamera()
    {
        Vector3 rotation = new Vector3(-Input.GetAxisRaw("Mouse Y"), 0, 0);
        Vector3 rotationAmount = rotation * rotationSpeed * Time.fixedDeltaTime;
        playerCamera.transform.Rotate(rotationAmount, Space.Self);
        Vector3 newRotation = playerCamera.transform.localRotation.eulerAngles;
        Vector2 limits = cameraRotationLimitsNormal;
        if (newRotation.z > 1 && gravityRotationDirection == -1)
        {
            if (newRotation.x > limits.y) newRotation.x = limits.y;
            if (newRotation.x < limits.x) newRotation.x = limits.x;

        }
        else if (gravityRotationDirection == 1)
        {
            limits = cameraRotationLimitsFlipped;
            if (newRotation.x > limits.x && newRotation.x < limits.y)
                if (Mathf.Abs(newRotation.x - limits.x) < Mathf.Abs(newRotation.x - limits.y)) newRotation.x = limits.x;
                else newRotation.x = limits.y;


        }
        playerCamera.transform.localRotation = Quaternion.Euler(newRotation);
    }

    // Raycasting to see if there's an interactable object in front of the player
    // The interactHandler of the scene handles the result
    //void FindInteractable()
    //{
    //    Ray interacter = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    //    RaycastHit hitData;
    //    Debug.DrawRay(interacter.origin, interacter.direction * 4); // DEBUG!!!

    //    if (Physics.Raycast(interacter, out hitData, 4))
    //    {
    //        IInteractable interactable = hitData.collider.GetComponent<IInteractable>();
    //        if (interactable == null)
    //        {
    //            interactable = hitData.collider.GetComponentInParent<IInteractable>();
    //        }
    //        interactHandler.Interactable = interactable;
    //    }
    //    else
    //    {
    //        interactHandler.Interactable = null;
    //    }
    //}

    void CheckForGravityChange()
    {
        if (canChangeGravity && GameFunctions.F3HasGravityDevice && Input.GetKeyDown(KeyCode.G) && timeInRotation > gravityRotationTime / 2)
        {
            ChangeGravity();
        }
    }

    void ChangeGravity()
    {
        Physics.gravity *= -1;
        gravityRotationDirection = (int)Mathf.Sign(Physics.gravity.y);
        gravityStartRotation = transform.rotation.eulerAngles;
        gravityEndRotationX = 180;
        timeInRotation = 0;
    }

    void RotateForGravity()
    {
        if (timeInRotation > gravityRotationTime) return;
        timeInRotation += Time.deltaTime;
        float newRotx = gravityStartRotation.x + (gravityEndRotationX - gravityStartRotation.x) * Mathf.Clamp01(timeInRotation / gravityRotationTime);
        transform.rotation = Quaternion.Euler(newRotx, gravityStartRotation.y, gravityStartRotation.z);
    }


    public List<object> SaveData()
    {
        Vector3 saveRotation = player.rotation.eulerAngles;
        if (gravityRotationDirection == 1) saveRotation.x = 180;
        if (gravityRotationDirection == -1) saveRotation.x = 0;
        return new List<object>
        {
            player.position.x,
            player.position.y,
            player.position.z,
            saveRotation.x,
            saveRotation.y,
            saveRotation.z
        };
    }

    public void LoadData(List<object> data)
    {
        player.position = new Vector3((float)data[0], (float)data[1], (float)data[2]);
        player.rotation = Quaternion.Euler(new Vector3((float)data[3], (float)data[4], (float)data[5]));
    }
}
