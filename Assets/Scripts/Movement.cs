using UnityEngine;

public class Movement : MonoBehaviour
{
    // Parameters - for tunig, usually set in editor
    [SerializeField] float thrustSpeed = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainJet;
    [SerializeField] ParticleSystem leftJet;
    [SerializeField] ParticleSystem rightJet;

    // Caches - references for readability or speed
    Rigidbody rb;
    AudioSource audioSource;

    // State - private instance (member variables)
    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }


    }

    void StartThrusting()
    {
        // add vertical force to rocket, determine thrust speed and make motion framerate independent
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainJet.isPlaying)
        {
            mainJet.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainJet.Stop();
    }

    void RotateLeft()
    {
        leftJet.Stop();
        RotateShip(rotateSpeed);
        if (!rightJet.isPlaying)
        {
            rightJet.Play();
        }
    }

    void RotateRight()
    {
        rightJet.Stop();
        RotateShip(-rotateSpeed);
        if (!leftJet.isPlaying)
        {
            leftJet.Play();
        }
    }
    void StopRotating()
        {
            rightJet.Stop();
            leftJet.Stop();
        }

    void RotateShip(float rotationThisFrame)
    {
        // freezing rotation to allow for manual rotation
        rb.freezeRotation = true;
        //rotate ship on z axis with Vector3.foward
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        // unfreeze rotation to let physics system regain control
        rb.freezeRotation = false;
    }
}
