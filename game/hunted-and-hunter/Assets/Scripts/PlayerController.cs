using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public StartMenu startMenu;

    private void Start()
    {
    
    }

    public void OnFoxButtonClicked()
    {
        startMenu.StartGame("Fox");
        
    }

    public void OnRabbitButtonClicked()
    {
        startMenu.StartGame("Rabbit");
    }
}
