using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame(string playerRole)
    {
        if(playerRole == "Fox"){
            SceneManager.LoadScene("FoxScene");
        } else if(playerRole == "Rabbit") {
            SceneManager.LoadScene("RabbitScene");
        } else {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
