using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMessage : MonoBehaviour
{
    [SerializeField] Text messageText;
    public void WonMessage()
    {
        messageText.text = "You Won!";
    }

     public void LostMessage()
    {
        messageText.text = "You Lost..";  
    }
}
