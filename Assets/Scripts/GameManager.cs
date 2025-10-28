using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject playerGameObject;
    public GameObject monsterGameObject;
    public Transform playerSpawnLocation;
    public Transform monsterSpawnLocation;

    public GameObject winText;
    public GameObject loseText;

    public GameObject collectiblesContainer;

    public List<GameObject> collectibles = new List<GameObject>();

    public static int score = 0;
    public static int lives = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in collectiblesContainer.transform)
        {
            collectibles.Add(child.gameObject);
        }
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= 8)
        {
            monsterGameObject.SetActive(false);
            winText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetGame();
            }
        }
        if (lives <= 0)
        {
            playerGameObject.SetActive(false);
            loseText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetGame();
            }
        }
    }

    public void ResetGame()
    {
        score = 0;
        Respawn();
        winText.SetActive(false);
        loseText.SetActive(false);
        monsterGameObject.SetActive(true);
        playerGameObject.SetActive(true);
        lives = 3;

        foreach (GameObject c in collectibles)
        {
            c.SetActive(true);
        }

    }

    public void Respawn()
    {
        playerGameObject.transform.position = playerSpawnLocation.position;
        playerGameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        monsterGameObject.transform.position = monsterSpawnLocation.position;
    }


}
