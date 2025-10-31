using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool demoMode;
    public GameObject playerGameObject;
    public GameObject monsterGameObject;
    public Transform playerSpawnLocation;
    public Transform monsterSpawnLocation;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI collectibleText;
    public TextMeshProUGUI timerText;

    public GameObject collectiblesContainer;
    public GameObject heartsContainer;

    public CinemachineCamera cinemachineCamera;

    public AnimationCurve camZoomCurve;



    List<GameObject> collectibles = new List<GameObject>();
    List<GameObject> heartIcons = new List<GameObject>();

    public static int score = 0;
    public static int lives = 3;
    public static float timer = 0;
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
        if (!demoMode)
        {
            if (score >= 8)
            {
                monsterGameObject.SetActive(false);
                winText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(0);
                }
            }
            else if (lives <= 0)
            {
                playerGameObject.SetActive(false);
                //monsterGameObject.SetActive(false);
                loseText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(0);
                }
            }
            else
            {
                timer += Time.deltaTime;
                int mins = Mathf.FloorToInt(timer / 60);
                int secs = Mathf.FloorToInt(timer % 60);
                if (secs < 10)
                {
                    timerText.text = mins + ":0" + secs;
                }
                else
                {
                    timerText.text = mins + ":" + secs;
                }
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
        }
        
    }

    public void ResetGame()
    {
        timer = 0;
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
        playerGameObject.SetActive(true);
        SetCameraTarget(playerGameObject);
        SetCameraSize(6, 0.1f);

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

    public void SetCameraSize(float size, float length = 1)
    {
        StartCoroutine(ZoomCamOverTime(size, length));
    }

    public void SetCameraTarget(GameObject target)
    {
        cinemachineCamera.Target.TrackingTarget = target.transform;
    }

    IEnumerator ZoomCamOverTime(float size, float length)
    {
        float t = 0;
        float startingSize = cinemachineCamera.Lens.OrthographicSize;
        while (cinemachineCamera.Lens.OrthographicSize != size)
        {
            cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(startingSize, size, t / length);
            t += Time.deltaTime;
            yield return null;
        }
    }


}
