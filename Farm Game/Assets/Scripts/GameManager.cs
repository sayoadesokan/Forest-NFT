using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject openanimation; //curtain dark mode animation
    // Start is called before the first frame update
    void Start()
    {
        openanimation.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(openanimclose());
    }

    IEnumerator openanimclose()
    {
        yield return new WaitForSeconds(2f);

        openanimation.SetActive(false);
    }
}
