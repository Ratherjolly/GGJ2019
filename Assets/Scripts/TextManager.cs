using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{

    private int pooledTextCount = 40;
    private int availableTextObjs;

    public TextMeshPro[] ALL_Text;

    private List<GameObject> TextObjs;
    public GameObject TextObj;
    public static TextManager instance;

    private static readonly Quaternion QUATERNION_IDENTITY = new Quaternion(0, 0, 0, 1);

    void Awake()
    {
        instance = this;
        availableTextObjs = (pooledTextCount - 1);
        //Text===================================
        TextObjs = new List<GameObject>();
        for (int i = 0; i < pooledTextCount; i++)
        {
            GameObject obj = (GameObject)Instantiate(TextObj, gameObject.transform.position, QUATERNION_IDENTITY);
            TextObjs.Add(obj);
        }
        for (int j = 0; j < TextObjs.Count; j++)
        {   //This forloop looks pretty pointless, but I got weird stuff to happen in the past if I didn't run the initial instantiation.  Call me superstitious; that, or a horrible speller.
            TextObjs[j].transform.parent = gameObject.transform;
            TextObjs[j].SetActive(false);
        }
    }

    public void callText(Vector3 start,string text)
    {
        GameObject audObj = getActiveTextObj();
        TextNode ao = audObj.GetComponent<TextNode>();
        //ASSIGN
        ao.setText(text);
        //EXCECUTE
        StartCoroutine(ao.UseText(start,10.0F,text));
    }

    #region GETTERS / SETTERS ======================================

    public GameObject getActiveTextObj()
    {
        availableTextObjs -= 1;
        for (int i = 0; i < TextObjs.Count; i++)
        {
            if (!TextObjs[i].activeSelf)
            {
                TextObjs[i].SetActive(true);
                return TextObjs[i];
            }
        }
        return new GameObject();
    }
    public void addAvailableTextObjs()
    {
        if (availableTextObjs != (pooledTextCount - 1))
            availableTextObjs += 1;
    }

    #endregion
}
