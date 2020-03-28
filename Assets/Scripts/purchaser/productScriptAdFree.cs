using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class productScriptAdFree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("adFree") == 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void BuyComplete(UnityEngine.Purchasing.Product product)
    {
        PlayerPrefs.SetInt("adFree", 1);
        Destroy(this.gameObject);
       
    }

    public void byFailed(UnityEngine.Purchasing.Product product,UnityEngine.Purchasing.PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchase failed");
    }
}
