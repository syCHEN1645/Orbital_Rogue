using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPoint : MonoBehaviour
{
    private const string NEXT_SCENE = "Menu Scene";
    private bool atVictoryPoint;
    public Canvas indicator;
    // Start is called before the first frame update
    void Start()
    {
        atVictoryPoint = false;
        // indicator appears only when player is near victory point
        indicator.transform.position = gameObject.transform.position;
        indicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (atVictoryPoint && Input.GetKeyDown(KeyCode.F)) {
            // press F to enter portal to next level
            SceneManager.LoadScene(NEXT_SCENE);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            atVictoryPoint = true;
            indicator.gameObject.SetActive(true);
        }
        
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            atVictoryPoint = false;
            indicator.gameObject.SetActive(false);
        }
    }
}
