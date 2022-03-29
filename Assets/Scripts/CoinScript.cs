using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public static CoinScript instance;

    public int myCoins;


    private void Awake()
    {
        myCoins = PlayerPrefs.GetInt("coin");
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, +1.5f);
    }


}
