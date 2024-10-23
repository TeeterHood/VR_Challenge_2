using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkInPlaceLocomotion : MonoBehaviour
{
    public CharacterController characterController;

    [SerializeField] GameObject leftHand, rightHand;

    Vector3 previousPosLeft, previousPosRight;
    Vector3 gravity = new Vector3(0, -9.8f, 0);
    Vector3 velocity = Vector3.zero; // Velocity variable to manage movement

    [SerializeField] float speed = 4;
    [SerializeField] float acceleration = 5; // Acceleration factor
    [SerializeField] float deceleration = 5; // Deceleration factor
    [SerializeField] float minVelocity = 0.1f; // Minimum velocity threshold

    // Start is called before the first frame update
    void Start()
    {
        SetPreviousPos();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 leftHandVelocity = leftHand.transform.position - previousPosLeft;
        Vector3 rightHandVelocity = rightHand.transform.position - previousPosRight;
        float totalVelocity = leftHandVelocity.magnitude + rightHandVelocity.magnitude;

        // Determine if the user is moving their hands
        if (totalVelocity >= 0.01f)
        {
            // Get the forward direction based on the camera's orientation
            Vector3 direction = Camera.main.transform.forward;

            // Update the velocity based on acceleration
            velocity += direction.normalized * speed * Time.deltaTime * acceleration;

            // Ensure the velocity does not exceed a maximum speed
            if (velocity.magnitude > speed)
            {
                velocity = velocity.normalized * speed;
            }
        }
        else
        {
            // Decelerate smoothly when hands are not moving
            velocity -= velocity.normalized * deceleration * Time.deltaTime;

            // Stop the movement if the velocity is below the minimum threshold
            if (velocity.magnitude < minVelocity)
            {
                velocity = Vector3.zero;
            }
        }

        // Move the character using the calculated velocity only if grounded
        if (characterController.isGrounded)
        {
            characterController.Move(velocity * Time.deltaTime);
        }

        // Apply gravity only if the player is not grounded
        if (!characterController.isGrounded)
        {
            characterController.Move(gravity * Time.deltaTime);
        }

        // Update previous positions for next frame's calculation
        SetPreviousPos();
    }

    void SetPreviousPos()
    {
        previousPosLeft = leftHand.transform.position;
        previousPosRight = rightHand.transform.position;
    }
}
