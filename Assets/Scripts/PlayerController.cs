using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10f;
    public float jumpForce = 700;
    bool facingRight = true;
    public Rigidbody2D rigid2D;

    //animation
    public Animator legAnim;
    public Animator bodyAnim;
    public Animator headAnim;

    //controls
    float move = 0;
    float aim = 0;
    

    //Falling shizzles
    bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

	// Use this for initialization
	void Start () {
        if (legAnim == null)
            legAnim = transform.FindChild("Legs").GetComponent<Animator>();
        if (bodyAnim == null)
            bodyAnim = transform.FindChild("Body").GetComponent<Animator>();
        if (rigid2D == null)
            rigid2D = gameObject.GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        legAnim.SetBool("Grounded", grounded);
        legAnim.SetFloat("vSpeed", rigid2D.velocity.y);

        //controlMovement

        legAnim.SetFloat("Speed", Mathf.Abs(move));

        rigid2D.velocity = new Vector2(move * maxSpeed, rigid2D.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        //control aiming;
        //bodyAnim.SetFloat("vAim", aim);

	}

    void Update()
    {
        //get key presses
        if (grounded && Input.GetKeyDown(KeyCode.C))
        {
            legAnim.SetBool("Grounded", false);
            rigid2D.AddForce(new Vector2(0, jumpForce));
        }
        move = Input.GetAxis("Horizontal");
        aim = Input.GetAxis("Vertical");
        bodyAnim.SetFloat("vAim", aim);


        if (Input.GetKeyDown(KeyCode.X))
        {
            bodyAnim.SetTrigger("Fire");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
