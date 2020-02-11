﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class custom_fps : MonoBehaviour
{
    Vector2 pub_rotation = Vector2.zero;
    [Range(0.5f, 10.0f)]
    public float cameraSpeed = 2f;
    [Range(0.5f, 100.0f)]
    public float moveSpeed = 2f;

    private bool noLook = false;
    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        //Move();
        ForceCollide();
        MouseLook();
        EventKeys();
    }

    void EventKeys()
    {
        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine("Forward");

        if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine("Back");

        if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine("Left");

        if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine("Right");

        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine("Space");

        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCoroutine("Ctrl");

        if (Input.GetKeyDown(KeyCode.LeftShift))
            LShift();

        if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = moveSpeed / 2;
    }

    IEnumerator Forward()
    {
        for (; ; )
        {
            if (Input.GetKeyUp(KeyCode.W))
                break;
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator Back()
    {
        for (; ; )
        {
            if (Input.GetKeyUp(KeyCode.S))
                break;
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator Left()
    {
        for (; ; )
        {
            if (Input.GetKeyUp(KeyCode.A))
                break;
            Vector3 forward = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 left = Vector3.Cross(forward.normalized, up.normalized);
            transform.position += left * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator Right()
    {
        for (; ; )
        {
            if (Input.GetKeyUp(KeyCode.D))
                break;
            Vector3 forward = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 right = Vector3.Cross(forward.normalized, up.normalized);
            transform.position -= right * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator Space()
    {
        for (; ; )
        {
            if (Input.GetKeyUp(KeyCode.Space))
                break;
            transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed / 100), transform.position.z);
            yield return null;
        }
    }
    IEnumerator Ctrl()
    {
        for (; ; )
        {
            if (Input.GetKeyUp(KeyCode.LeftControl))
                break;
            transform.position = new Vector3(transform.position.x, transform.position.y - (moveSpeed / 100), transform.position.z);
            yield return null;
        }
    }

    void LShift()
    {
        moveSpeed += moveSpeed;
    }
    void ForceCollide()
    {
        if (transform.position.y <= 0.6f)
        {
            //LOCKS Y POSITION ON COLLIDE:
            transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
        }
    }
    void MouseLook()
    {
        //Pause mouselook key
        if (Input.GetKeyDown(KeyCode.P))
            noLook = !noLook;

        if (!noLook)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pub_rotation.y += Input.GetAxis("Mouse X");
            pub_rotation.x += Input.GetAxis("Mouse Y") - (Input.GetAxis("Mouse Y") * 2);
            transform.eulerAngles = pub_rotation * cameraSpeed;
        }
        else
            Cursor.lockState = CursorLockMode.None;
    }

    /*
    void Move() //DEPRECIATED:
    {
        float horizontalPlaceholder = 0;
        float verticalPlaceholder = 0;
        if (Input.GetAxis("Horizontal") > 0f)
        {
            horizontalPlaceholder = 1;
        }
        if (Input.GetAxis("Vertical") > 0f)
        {
            verticalPlaceholder = 1;
        }
        if (Camera.current != null)
        {
            Camera modCam = Camera.current;
            modCam.transform.Translate(new Vector3(horizontalPlaceholder * 0.2f, 0.0f, verticalPlaceholder * 0.2f));
        }
    }*/
}
