using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bag : MonoBehaviour
{
    public Canvas canvas;
    
    private Transform bag;
    private const int itemNum = 10;

    public Image[] itemImages = new Image[itemNum];
    public Text[] countsText = new Text[itemNum];
    public int[] counts = new int[itemNum];
    public BaseItem[] item = new BaseItem[itemNum];
    BaseItem egg = new BaseItem(0, "egg");

    void Awake()
    {
        Load();
    }

    // Use this for initialization
    void Start ()
    {
        bag = canvas.transform.GetChild(0);
        for (int i = 0; i < itemNum; i++)
            counts[i] = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //蒐集物品
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);

            if (hit.collider.tag == "Item")
            {
                ObjectIten item = (ObjectIten)hit.collider.gameObject.GetComponent<ObjectIten>();
                if (item != null)
                {
                    item.isChecked = true;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Additem(item);
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
         
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            bag.gameObject.SetActive(bag.gameObject.active? false : true);
            Refresh();
        }       
	}

    void Load()
    {
    }

    public void Additem(ObjectIten objItem)
    {
        BaseItem itemToAdd = new BaseItem(objItem);
        Debug.Log(itemToAdd.name);
        bool isFind = false;
        for(int i = 0; i < itemNum; i++)
        {
            if (item[i] == itemToAdd)
            {
                counts[i]++;
                isFind = true;
            }
            break;
        }
        if(isFind == false)
        {
            for (int i = 0; i < itemNum; i++)
            {
                if (item[i] == null)
                {
                    counts[i]++;
                    item[i] = itemToAdd;
                }
                break;
            }
        }
        Refresh();
    }

    void Refresh()
    {
        for (int i = 0; i < itemNum; i++)
        {
            if (item[i] == null)
            {
                countsText[i].text = "";
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
            }
            else
            {
                countsText[i].text = counts[i] < 2 ? "" : counts[i].ToString();
                string path = "Picture/" + item[i].name;
                itemImages[i].sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                itemImages[i].enabled = true;
            }
        }
    }
}


