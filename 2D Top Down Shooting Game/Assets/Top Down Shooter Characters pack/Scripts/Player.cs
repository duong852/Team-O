using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//next sprint, change player movement to its own class.
public class Player : MonoBehaviour
{
    private int health;
    //add gun class next sprint
   // Gun heldGuns = new Gun[3];
    public float movespeed = 3f;
    public Rigidbody2D characterBody;
    public Transform playerRotation;
    public float rotateSpeed = 20f;
    public Animator animate;
    private bool setMove = false, setBuckMove = false;
    Vector2 move;
    // Start is called before the first frame update
    void Start()
    {
        health = 30;
        playerRotation = GetComponent<Transform>();
        characterBody = GetComponentInParent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        faceCursor();
        getMovement();
        

    }

    void FixedUpdate()
    {
        characterBody.MovePosition(characterBody.position + move * movespeed * Time.fixedDeltaTime*15);

    }

    void faceCursor()
    {
        Vector3 mouse_pos = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = transform.position.y;
        //mousePointer.transform.position = mousePos;
        Vector3 player_pos = Camera.main.WorldToScreenPoint(playerRotation.transform.position);
        mouse_pos.x = mouse_pos.x - player_pos.x;
        mouse_pos.y = mouse_pos.y - player_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        playerRotation.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle - 90)), rotateSpeed);
    }
    void getMovement()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        animate.SetBool("Move", setMove);
        animate.SetBool("MoveBuck", setBuckMove);
    }
}




