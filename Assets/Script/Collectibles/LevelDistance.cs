using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDistance : MonoBehaviour
{
    public GameObject disDisplay;
    public int disRun;
    private bool addingDis = false;

    void Update()
    {
        if (!addingDis)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }
    }

    IEnumerator AddingDis()
    {
        while (true)
        {
            disRun += 1;
            disDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = disRun.ToString();
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void StopDistanceCount()
    {
        StopCoroutine(AddingDis());
    }
}