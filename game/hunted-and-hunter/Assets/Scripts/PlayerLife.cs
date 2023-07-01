using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    bool isDead = false;
    PlayerController playerController;

    [SerializeField] AudioSource deathSound;

    private void Update() {
        if(transform.position.y < -1f && !isDead)
        {
            Die();
        }    
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Enemy Body"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerMovement>().enabled = false;
            Die();
        }
    }

    void Die()
    {      
        Invoke(nameof(ReloadLevel), 1.3f);
        isDead = true;
        deathSound.Play();
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // playerController.EnableCharacterMovement(playerController.getActiveCharacter());
    }
}
