using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    // add range slider, set min = 0 max = 1 for movement factor
    float movementFactor;
    [SerializeField] float period = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        
        // continually growing over time
        float cycles = Time.time / period;

        // tau is equal to 2 pi - used rather than using radians - 1 tau is one complete cycle/lap around a circle
        const float tau = Mathf.PI * 2;

        // range -1 to 1
        float rawSineWave = Mathf.Sin(cycles * tau);

        // set range of movement factor to 0 - 1 (rawSineWave range -1 - 1)
        movementFactor = (rawSineWave + 1f) / 2f;


        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
        
    }
}
