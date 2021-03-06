﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextNode : MonoBehaviour
{
    private TextMeshPro tmp;
    private GameObject TM;
    private TextManager TMS;

    private static readonly Vector3 VECTOR_RESET = new Vector3(0, 0, 0);

    void Awake()
    {
        tmp = gameObject.GetComponent<TextMeshPro>();
        TM = GameObject.FindGameObjectWithTag("TextManager");
        TMS = TM.GetComponent<TextManager>();
    }

    void OnDisable()
    {
        //DEFAULTS==============
        if (tmp.text != string.Empty)
            tmp.text = string.Empty;

        if (tmp.color != Color.black)
            tmp.color = Color.black;

        if (this.gameObject.transform.position != VECTOR_RESET)
            this.gameObject.transform.position = VECTOR_RESET;

        TMS.addAvailableTextObjs();
    }

    public void setText(string text) {
        tmp.text = text;
    }

    public IEnumerator UseText(Vector3 start, float dist, string text)
    {
        this.transform.position = start;
        tmp.text = text;
        tmp.color = Random.ColorHSV();
        float temp = 0;
        bool isLeft = false;

        while (temp<dist)
        {
            if (isLeft)
                this.transform.position += new Vector3(-0.1F, 0.15F);
            else {
                this.transform.position += new Vector3(0.1F, 0.15F);
            }

            temp += 0.2F;
            if (tmp.fontSize > 10.0F)
                tmp.fontSize = 8.0F;
            else {
                tmp.fontSize = 12.0F;
            }
            yield return new WaitForSeconds(0.025F);
        }
        gameObject.SetActive(false);
        yield return null;
    }
}
