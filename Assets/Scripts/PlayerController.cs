using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController: MonoBehaviour
{
	private float speed = 5f;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>(); 
	}

	void FixedUpdate()
	{
		bool forward = Input.GetKey(KeyCode.UpArrow);
		bool right = Input.GetKey(KeyCode.RightArrow);
		bool left = Input.GetKey(KeyCode.LeftArrow);
		bool back = Input.GetKey(KeyCode.DownArrow);

		if (forward) MoveForward();
		if (right) MoveRight();
		if (left) MoveLeft();
		if (back) MoveBack();

	}

	void MoveForward()
	{
		this.transform.eulerAngles = new Vector3(0, 0, 0);
		 rb.MovePosition(transform.position + speed * Vector3.forward * Time.deltaTime);
	}

	void MoveBack()
	{
		this.transform.eulerAngles = new Vector3(0, 180, 0);
		 rb.MovePosition(transform.position + speed * Vector3.back * Time.deltaTime);
	} 

	void MoveRight()
	{
		this.transform.eulerAngles = new Vector3(0, 90, 0);
		 rb.MovePosition(transform.position + speed * Vector3.right *Time.deltaTime);
	}
		
	void MoveLeft()
	{
		this.transform.eulerAngles = new Vector3(0, -90, 0);
		rb.MovePosition(transform.position + speed * Vector3.left * Time.deltaTime);
	} 

	void OnTriggerEnter(Collider other)
	{
	}
}