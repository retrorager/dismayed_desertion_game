using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Thanks for playing :)");
            Application.Quit();
        }
    }
}
