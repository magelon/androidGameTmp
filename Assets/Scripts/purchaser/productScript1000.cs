using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class productScript1000 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyComplete(UnityEngine.Purchasing.Product product)
    {
        GameData.getInstance().coin += 1000;
        PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
        GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
       
    }

    public void byFailed(UnityEngine.Purchasing.Product product,UnityEngine.Purchasing.PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase failed");
    }
}
