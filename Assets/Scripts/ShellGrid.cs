using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellGrid : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;

    [SerializeField]
    public bool isEnabled;
    void Start()
    {
        image = this.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        Color color = image.color;
        if (isEnabled)
        {
            color.a = 1;
        }
        else
        {
            color.a = .1f;
        }
        image.color = color;
    }
}
