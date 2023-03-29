using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Vector2 moveInput;
    public float speed;

    private Vector3 playerVelocity;
    private bool isGrounded;
    public float gravity = -9.78f;
    public float jumpForce = 2f;

    public Camera cam;
    private Vector2 lookPos;
    private float xRotation = 0f;
    public float xSens = 30f;
    public float ySend = 30f;

    //https://www.youtube.com/watch?v=1tT2hz-tKTg

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
