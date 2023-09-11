using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movimiento basico
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;

    public CharacterController player;

    //movimiento relativo a la camara
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    //velocidad de movimiento
    public float speed;
    // The catch up rotation speed of the capsule to the camPivot using slerp
    public float capsuleRotSpeed;

    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;


    void Start()
    {
        player = GetComponent<CharacterController>();
    }
    void Update()
    {
        //movimiento diagonal corregido
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        //relativo a la camara
        camDirection();

        player.transform.LookAt(player.transform.position + movePlayer);

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * speed;

        SetGravity();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer = movePlayer * speed;

        if (movePlayer.magnitude > 0)
        {
            player.transform.LookAt(player.transform.position + movePlayer);
        }

        SetGravity();

        player.Move(movePlayer * Time.deltaTime);

        //mostrar velicidad en la consola
        Debug.Log(player.velocity.magnitude);
    }
    
    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }
}
