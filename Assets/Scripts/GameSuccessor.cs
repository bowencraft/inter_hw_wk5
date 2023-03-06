using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSuccessor : MonoBehaviour
{

    public AudioClip SuccessSound;
    // Start is called before the first frame update
    void Start()
    {

        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = SuccessSound;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
