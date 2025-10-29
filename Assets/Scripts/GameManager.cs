using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerGameObject;
    public GameObject monsterGameObject;
    public Transform playerSpawnLocation;
    public Transform monsterSpawnLocation;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI collectibleText;

    public GameObject collectiblesContainer;
    public GameObject heartsContainer;

    List<GameObject> collectibles = new List<GameObject>();
    List<GameObject> heartIcons = new List<GameObject>();

    public static int score = 0;
    public static int lives = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in collectiblesContainer.transform)
        {
            collectibles.Add(child.gameObject);
        }
        foreach (Transform child in heartsContainer.transform)
        {
            heartIcons.Add(child.gameObject);
        }
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= 8)
        {
            monsterGameObject.SetActive(false);
            winText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetGame();
            }
        }
        if (lives <= 0)
        {
            playerGameObject.SetActive(false);
            loseText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetGame();
            }
        }
    }

    public void ResetGame()
    {
        SetScore(0);
        Respawn();
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        monsterGameObject.SetActive(true);
        playerGameObject.SetActive(true);
        SetLife(3);

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

    public void SetLife(int amount)
    {
        lives = amount;

        foreach (GameObject g in heartIcons)
        {
            g.SetActive(false);
        }
        for (int i = 0; i < lives; i++)
        {
            heartIcons[i].SetActive(true);
        }
    }

    public void SetScore(int amount)
    {
        score = amount;
        collectibleText.text = score.ToString() + " / " + collectibles.Count;
    }


}
