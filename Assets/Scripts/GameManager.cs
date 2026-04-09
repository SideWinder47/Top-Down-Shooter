using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI lifeText;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private GameObject gameoverScreen;
	[SerializeField] private GameObject titleScreen;
	
	public int score;
	public int lives;
	public bool isGameActive;
	
	public static GameManager Instance;

	void Awake()
	{
		Instance = this;
	}
	
	public void StartGame()
	{
		titleScreen.SetActive(false);
		lifeText.gameObject.SetActive(true);
		scoreText.gameObject.SetActive(true);
		isGameActive = true;
		score = 0;
		lives = 3;
	}
	
	public void RestartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void UpdateScore (int scoreToAdd)
	{
		score += scoreToAdd;
		scoreText.text = "Score: " + score;
	}
	
	public void UpdateLife (int lifeChange)
	{
		lives += lifeChange;
		lifeText.text = "Lives: " + lives;
		
		if (lives == 0)
		{
			GameOver();
		}
	}
	
	private void GameOver()
	{
		gameoverScreen.SetActive(true);
		isGameActive = false;
	}
	
	
}
