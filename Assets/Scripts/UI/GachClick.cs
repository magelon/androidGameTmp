using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GachClick : MonoBehaviour
{
    private bool equipted;
    public static GameObject current;
    //public GameObject tick;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("want a spin?");
    }

    public void OnClick(GameObject g)
    {
        if(g.transform.Find("lock").GetComponent<Image>().enabled == false)
        {
            current = g;
            PlayerPrefs.SetString("currentEquip",g.name);
            //can equip
            if (equipted == false)
            {
                g.transform.Find("tick").GetComponent<Image>().enabled = true;
                equipted = true;
            }
            else
            {
                g.transform.Find("tick").GetComponent<Image>().enabled = false;
                equipted = false;
            }
            //g.transform.Find("tick").GetComponent<Image>().enabled = false;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        current = GameObject.Find(PlayerPrefs.GetString("currentEquip"));
        if (current)
        {
            current.transform.Find("tick").GetComponent<Image>().enabled = true;
        }
       
    }

    void Update()
    {
        if (current==transform.gameObject&&!equipted)
        {
            current.transform.Find("tick").GetComponent<Image>().enabled = true;
        }
        if (current != transform.gameObject)
        {
            transform.Find("tick").GetComponent<Image>().enabled = false;
        }
        if (current == transform.gameObject && !equipted)
        {
            if (PlayerPrefs.GetString("currentEquip") != null)
            {
                PlayerPrefs.SetString("currentEquip", null);
            }
            
        }
        
    }
}
