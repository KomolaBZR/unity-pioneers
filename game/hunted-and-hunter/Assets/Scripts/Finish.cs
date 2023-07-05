using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Rabbit" && SceneManager.GetActiveScene().name == "RabbitScene")
        {
            SceneManager.LoadScene("EndRabbitScene");
        }
        else 
        {
            SceneManager.LoadScene("EndFoxScene");
        }
    }
}
