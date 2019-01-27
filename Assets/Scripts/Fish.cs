using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    List<GameObject> fish = new List<GameObject>();
    List<float> speeds = new List<float>();

    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            fish.Add(this.transform.GetChild(i).gameObject);
            speeds.Add(Random.Range(10,50)*0.001F);
            fish[i].transform.localScale = new Vector3(speeds[i]*80.1F, speeds[i]*80.1F, 1.0F);
        }
    }

    void FixedUpdate()
    {
        if(fish.Count>0)
        {
            for (int i = 0; i < fish.Count; i++)
            {
                if(fish[i].transform.position.x>(CameraEffects.instance.cam.transform.position.x-8.0F))
                    fish[i].transform.position -= new Vector3(speeds[i],0,0);
                else {
                    fish[i].transform.position = new Vector3(CameraEffects.instance.cam.transform.position.x+12.0F,Random.Range(-80,60)*0.1F,0);
                }

            }
        }
    }
}
