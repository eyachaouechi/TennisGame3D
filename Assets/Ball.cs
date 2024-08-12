using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour
{
    Vector3 initialPos; // ball's initial position
    public string hitter;
    int playerScore;
    int botScore;
    [SerializeField] Text playerScoreText;
    [SerializeField] Text botScoreText;
    [SerializeField] GameObject gameOverText;
    public bool playing = true;
    [SerializeField] private AudioSource gameOverAudioSource;
    [SerializeField] private AudioSource gameAudioSource;
    public Button restartButton;
    private void Start()
    {
        initialPos = transform.position; // default it to where we first place it in the scene
        playerScore=0;
        botScore=0;
        gameOverText.SetActive(false);
        gameOverAudioSource.Pause();
        gameAudioSource.Play();
        restartButton.gameObject.SetActive(false);
}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Out")) 
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            transform.position = initialPos; // reset it's position 
            GameObject.Find("player").GetComponent<Player>().Reset();
if (playing){
if(hitter == "player"){
botScore++;

}
else if (hitter == "bot"){
playerScore++;

}
if (botScore >= 3)
        {
            GameOver();
        }
playing = false;
updateScores();
        }
    }

}
 private void OnTriggerEnter(Collider other){
if(other.CompareTag("Out") && playing){
if(hitter == "player"){
playerScore++;

}
else if (hitter == "bot"){
botScore++;

}
if (botScore >= 3)
        {
            GameOver();
        }
playing = false;
updateScores();
}
}
void updateScores(){
playerScoreText.text = "Joueur : " + playerScore;
botScoreText.text = "Bot : " + botScore;
}
private void GameOver()
    {
        gameOverText.SetActive(true);
        playing = false;
        playerScoreText.enabled = false;
        botScoreText.enabled = false;
        gameOverAudioSource.Play();
        gameAudioSource.Pause();
        Time.timeScale = 0f;
        restartButton.gameObject.SetActive(true);
    }

public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}