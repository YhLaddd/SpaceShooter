using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{    // MOVEMENT these floats for forwardspeed etc
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 15f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    // ACCELERATION
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    // ROTATE / MOVE W MOUSE
    public float rotateSpeed = 90f;
    // FINDS LOC OF MOUSE
    private Vector2 lookInput, screenCenter, mouseDistance;
    // ABLE TO ROTATE AND DO ROLLS
    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    void Start()
    {
        // FINDS THE SCREEN WIDTH
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        //locks cursor
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //MOUSE MOVEMENT / ROTATION
        //FINDS WHERE MOUSE IS EVERY FRAME
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        //WORKS OUT HOW MUCH DISTANCE FROM THE CENTRE THE MOUSE IS
        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        //ROTATES THE SHIP TO SELF & adds rolls in
        transform.Rotate(-mouseDistance.y * rotateSpeed * Time.deltaTime, mouseDistance.x * rotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);
        // this locks the roation so it doesnt speed up when mouse is off screen.
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        //ROLL
        //find input keys in project prefs
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);


        //MOVEMENT VERTICAL & HORIZONTAL
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxis("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        //HOVER BUTTON IN PROJECT PREFENCES & INPUTS
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);
        //apply the speeds to ship to allow it to move forward, backwards side to side up and down.
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;
    }
}
