using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyIntEvent : UnityEvent<int>
{
}

public class PlayerMovementController2D : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer FireWeapon;
    [SerializeField]
    SpriteRenderer Vacuum;

    public static Vector2 lastWantedDirection = Vector2.zero;

    float movementSpeed;
    public float normalMovementSpeed = 1f;
    public float vacuumMovementSpeed = .5f;
    public float AngularSpeed = 1f;
    // IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    Vector2 LastDirection;
    Transform OwnTransform;
    Vector3 MousePosition;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        // isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        OwnTransform = transform;

        BoidsManager.Instance.Predators.Add(gameObject);
        movementSpeed = normalMovementSpeed;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementSpeed = normalMovementSpeed;
        // isoRenderer.AverageMaxSpeed = movementSpeed;

        Vector2 direction = new Vector2(Input.GetAxisRaw("RightHorizontal"), Input.GetAxisRaw("RightVertical"));
        direction = direction * AngularSpeed;

        Vector2 currentPos = rbody.position;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        currentVel = movement * Time.fixedDeltaTime;
        Vector2 newPos = currentPos + currentVel;
        rbody.MovePosition(newPos);

        if (direction != Vector2.zero)
        {
            if(LastDirection != direction)
            { 
                LastDirection = direction;
                // isoRenderer.SetDirection(direction, movement);
                //OwnTransform.rotation = Quaternion.Lerp(OwnTransform.rotation, Quaternion.LookRotation(LastDirection), Time.deltaTime * AngularSpeed);
            }
        }
        else if(MousePosition != Input.mousePosition)
        {
            direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            //OwnTransform.rotation = Quaternion.Lerp(OwnTransform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * AngularSpeed);
            MousePosition = Input.mousePosition;
            // isoRenderer.SetDirection(direction, movement);
        }

        if(direction != Vector2.zero)
        {
            lastWantedDirection = direction;
        }
    }
    Vector2 currentVel;
    public void SwitchWeapon(int WeaponIndex)
    {
        if(FireWeapon != null && WeaponIndex == 0 && !FireWeapon.enabled)
        {
            FireWeapon.enabled = true;
            Vacuum.enabled = false;
            Vacuum.GetComponent<VacuumWeapon>().EndVacume();
        }
        else if(Vacuum != null && WeaponIndex == 1 && !Vacuum.enabled)
        {
            FireWeapon.enabled = false;
            Vacuum.enabled = true;
        }
    }

    public void SlowDown(bool value)
    {
        movementSpeed = value ? vacuumMovementSpeed : normalMovementSpeed;
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical(GUI.skin.box);
        UIDebugDrawLabelValue("Current Vel", currentVel.ToString());
        UIDebugDrawLabelValue("Mov Speed", movementSpeed.ToString());
        UIDebugDrawLabelValue("Max Delta Vel", (movementSpeed * Time.fixedDeltaTime).ToString());
        GUILayout.EndVertical();
    }

    private void UIDebugDrawLabelValue(string label, string value)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label(label + ":");
            GUILayout.Label(value);
        }
        GUILayout.EndHorizontal();
    }
}
