using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene 

public class ObstacleBehaviour : MonoBehaviour
{
    
    public float waitTime = 2.0f;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
 
            Destroy(collision.gameObject);

            Invoke("ResetGame", waitTime);
        }
    }

    void ResetGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}