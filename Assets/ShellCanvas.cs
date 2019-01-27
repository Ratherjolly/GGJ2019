using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShellCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    ShellGrid [] shellGrids;

    [SerializeField]
    public int shells = 0;
    void Start()
    {
        shellGrids = this.GetComponentsInChildren<ShellGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < shellGrids.Length; i++)
        {
            var shellGrid = shellGrids[i];
            shellGrid.isEnabled = shells > i;
        }
    }
}
