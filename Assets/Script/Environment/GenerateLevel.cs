using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] sections;
    public int zPos = 0;
    public bool creatingSection = false;
    public int secNum;

    void Update()
    {
        if (creatingSection == false)
        {
            creatingSection = true;
            StartCoroutine(GenerateSection());
        }
    }

    IEnumerator GenerateSection()
    {
        secNum = Random.Range(0, 3);
        Instantiate(sections[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 39;
        yield return new WaitForSeconds(2f);
        creatingSection = false;
    }
}