using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    // Reference to the Animator component
    public Animator birdAnimator;

    // Reference to the Controller Animator
    private Animator bcAnimator;

    // Set this in the Inspector: the bird trigger
    public Collider birdTrigger;

    // Set this in the Inspector: the bird's collider
    public Collider birdCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component attached to the same GameObject
        bcAnimator = GetComponent<Animator>();
    }

   

    // Update is called once per frame
    void Update()
    {
        // Check for collision between bird and tree
        if (birdTrigger && birdCollider)
        {
            bool isColliding = birdCollider.bounds.Intersects(birdTrigger.bounds);

            // Set the "flightEnded" parameter in the Animator
            birdAnimator.SetBool("flightEnded", true);
        }
    }

    public void StartBird()
    {
        bcAnimator = GetComponent<Animator>();
        birdAnimator.SetBool("isFlying", true);
        bcAnimator.SetBool("isFlying", true);
    }
}
