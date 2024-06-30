using UnityEngine;

public class VictoryPoint : MonoBehaviour
{
    // private const string NEXT_SCENE = "Menu Scene";
    public bool atVictoryPoint;
    public Canvas indicator;

    void Start()
    {
        atVictoryPoint = false;
        // indicator appears only when player is near victory point
        indicator.transform.position = gameObject.transform.position;
        indicator.gameObject.SetActive(false);
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
