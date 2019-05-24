using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
	public float speed = 6f;			//The speed that the player will move.
	public float turnSpeed = 60f;		
	public float turnSmoothing = 15f;

	private Vector3 movement;
	private Vector3 turning;
	private Animator anim;
	private Rigidbody playerRigidbody;

	void Awake()
	{
		//Get references
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		//Store input axes
		float lh = Input.GetAxisRaw("Horizontal");
		float lv = Input.GetAxisRaw ("Vertical");

        Move (lh, lv);

		Animating (lh, lv);

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

	void Move(float lh, float lv)
	{
		//Move the player
		movement.Set (lh, 0f, lv);

        //movement = Camera.main.transform.TransformDirection(movement);
        movement = movement.normalized * speed * Time.deltaTime;


        playerRigidbody.MovePosition(transform.position + movement);

		if(lh != 0f || lv != 0f)
		{
			Rotating(lh, lv);
		}

	}

	void Rotating(float lh, float lv)
	{
		Vector3 targetDirection = new Vector3(lh, 0f, lv);

		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

		Quaternion newRotation = Quaternion.Lerp (GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);

		GetComponent<Rigidbody>().MoveRotation(newRotation);
	}

	void Animating(float lh, float lv)
	{

        if(Input.GetKeyDown(KeyCode.K))
        {
            anim.SetBool("IsAttacking", true);
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("IsJumping", true);
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsAttacking", false);
                playerRigidbody.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
            }
            else
            {
                if (lh != 0f || lv != 0f)
                {
                    if (Input.GetKey(KeyCode.RightShift) || (Input.GetKey(KeyCode.LeftShift)))
                    {
                        anim.SetBool("IsRunning", true);
                        anim.SetBool("IsWalking", false);
                        anim.SetBool("IsIdle", false);
                        anim.SetBool("IsJumping", false);
                        anim.SetBool("IsAttacking", false);
                        speed = 2f;
                    }
                    else
                    {
                        anim.SetBool("IsWalking", true);
                        anim.SetBool("IsRunning", false);
                        anim.SetBool("IsIdle", false);
                        anim.SetBool("IsJumping", false);
                        anim.SetBool("IsAttacking", false);
                        speed = 0.5f;
                    }
                }
                else
                {
                    anim.SetBool("IsIdle", true);
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsRunning", false);
                    anim.SetBool("IsJumping", false);
                    anim.SetBool("IsAttacking", false);
                }

            }

        }

    }
}
