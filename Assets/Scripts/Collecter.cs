using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collecter : MonoBehaviour
{

    public GameObject player;
    int height;
    int height2=0;

    private bool finishLine = false;

    public TextMeshProUGUI coinCOunt;

    private int gameScore = 1;

    private int bonusPoint = 1;

    private PlayerController playerContr;

    [SerializeField] private AudioSource musicBox;
    [SerializeField] private AudioSource musicCoin;
    [SerializeField] private AudioSource musicDroped;
    [SerializeField] private AudioSource musicfinishedGame;

    private Camera cam;
    public Vector3 offSet;
    private void Start()
    {
        playerContr = GetComponentInParent<PlayerController>();
        cam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collect the cubes
        if (other.gameObject.CompareTag("Collect") && other.gameObject.GetComponent<CollectableCubes>().isCollected ==false)
        {
            musicBox.Play();
            height++;
            other.gameObject.GetComponent<CollectableCubes>().isCollected = true;
            other.gameObject.GetComponent<CollectableCubes>().setIndex(height);
            other.gameObject.transform.parent = player.transform.GetChild(2);
            cam.fieldOfView += 2;

        }
        // Collect the Coins
        if (other.gameObject.CompareTag("Coin"))
        {
            musicCoin.Play();
            CoinScript.instance.myCoins += 1;
            coinCOunt.text = CoinScript.instance.myCoins.ToString();
            other.gameObject.SetActive(false);
        }

        // Set Finish
        if (other.gameObject.CompareTag("finish"))
        {
            musicfinishedGame.Play();
            finishLine = true; 
        }
       
        if (other.gameObject.CompareTag("Blocking"))
        {
         
            // user losed and couldn't get a score.
            if (gameObject.transform.parent.GetChild(2).childCount == 0 && finishLine == false)
            {
                LevelController.instance.GameOver();
            }
        }


        if (other.gameObject.CompareTag("try"))
        {
           
            if (gameObject.transform.parent.GetChild(2).childCount > 0 && finishLine==true)
            {
                // Start Scoring
                offSet += new Vector3(0, 1, 0);
                bonusPoint++;
                Debug.Log("oluyormu ");

            }
            if (gameObject.transform.parent.GetChild(2).childCount == 0 && finishLine == true)
            {
                // after finish line user have no box to continou.
                gameScore = gameScore * bonusPoint * CoinScript.instance.myCoins; 
                finishTheGame();
                LevelController.instance.ChangeScore(gameScore);
                // total score Math

            }

        }

    }

    private void Update()
    {
        cam.transform.localPosition = transform.position+ offSet;
        // Set the height of Collector always on the ground
        player.transform.position = new Vector3(transform.position.x, height + 1, transform.position.z);
        transform.localPosition = new Vector3(0, -height, 0);  

    }

    public void HeightMinus()
    {
        musicDroped.Play();
        height--;
    }

    
    private void finishTheGame()
    {
        height--;
        LevelController.instance.FinishGame();
        playerContr.GetComponent<MeshRenderer>().enabled = false;

    }

}
