using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    public int gridLength, gridWidth;
    public float x_space, y_space;

    public GameObject _grass, showGrass1, showGrass2, plantIcon;
    public GameObject[] currentGrid;
    public Transform gridPos;

    //Field Creation....//
    GameObject hitGrass;
    public GameObject field, crop;
    RaycastHit2D _Hit;
    public bool creatingFields, canPlant, canHarvest;
    //End........//
    public bool gotGrid;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gridLength * gridWidth; i++)
        {
            GameObject grass = Instantiate(_grass) as GameObject;
            grass.transform.parent = this.transform;
            grass.transform.position = new Vector3(x_space + (x_space * (i % gridLength)), y_space + (y_space * (i / gridWidth)), 0);
        }

        this.transform.position = gridPos.position;
        this.transform.parent = gridPos;

        showGrass1.SetActive(false);
        showGrass2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gotGrid == false) 
        {
            currentGrid = GameObject.FindGameObjectsWithTag("Grass");
            gotGrid = true;
        }

        if(Input.GetMouseButtonDown(0))
        {
            _Hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if(creatingFields == true) 
            {
                if(_Hit.collider.tag == "Grass")
                {
                    Debug.Log("HitGrass");
                    hitGrass = _Hit.transform.gameObject;
                    Instantiate(field, hitGrass.transform.position, Quaternion.identity);
                    Destroy(hitGrass);
                }
            }
        }

        if(canPlant == true)
        {
            plantIcon.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Move over the cleared field to plant";
        }

        else if (canPlant == false && canHarvest == false)
        {
            plantIcon.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Click On Grass To Clear Fields";
        }

        if(canHarvest)
        {
            plantIcon.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Move over the grown plant to harvest";
        }
    }

    public void OnCreateFields()
    {
        creatingFields = true;
        plantIcon.SetActive(true);
        canPlant = false;
        canHarvest = false;
    }

    public void ReturnNormal()
    {
        creatingFields = false;
    }

    public void PlantCrop(GameObject hit)
    {
        Debug.Log("PlantCrop");
        hitGrass = hit.transform.gameObject;
        Instantiate(crop, hitGrass.transform.position, Quaternion.identity);
        Destroy(hitGrass);
    }

    public void CanPlant()
    {
        canPlant = true;
        creatingFields = false;
        canHarvest = false;
    }

    public void Harvest()
    {
        canPlant = false;
        creatingFields = false;
        canHarvest = true;
    }
}
