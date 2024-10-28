using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    public AudioClip footstepClip; // Reference to the single footstep sound
    public float stepInterval = 0.5f;

    private AudioSource audioSource;
    private float stepTimer;
    private CharacterController characterController;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = footstepClip; // Set the specific footstep clip
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded && characterController.velocity.magnitude > 0)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0;
            }
        }
    }

    void PlayFootstep()
    {
        if (footstepClip != null)
        {
            audioSource.PlayOneShot(footstepClip); // Play the footstep clip
        }
    }
}