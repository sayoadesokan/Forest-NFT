using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playermovement : MonoBehaviour
{
    public float movespeed = 5f;

    public Rigidbody2D rb;
    public Animator animator; 
    public int harvestedCrops;
    public Text curCropNumText;
    GameGrid farmGrid;

    Vector2 movement; 

    public float minX;
    public float maxX;
    public float minY;   
    public float maxY;

    void Start() 
    {
        farmGrid = FindObjectOfType<GameGrid>();
    }

    // Update is called once per frame
    void Update()
    {

        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        curCropNumText.text = harvestedCrops.ToString("00");

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }

    void OnTriggerStay2D(Collider2D other) //When PlayerTriggers a grid
    {
        if(other.gameObject.tag == "Field")//Check if its is a Field
        {
            Debug.Log("planting");
            if (farmGrid.canPlant == true) //If Player taps "Plant" button and CanPlant is On
            {
                Debug.Log("clicking p");
                farmGrid.PlantCrop(other.gameObject);//PlantCrop  '
            }
        }

        if(other.gameObject.tag == "Crop" && farmGrid.canHarvest)
        {
            if(other.gameObject.GetComponent<Crop>()._canHarvest == true) //If Player taps "Plant" button and CanPlant is On
            {
                Debug.Log("crop planted");
                other.gameObject.GetComponent<Crop>().Harvest();
                harvestedCrops ++;
            }
        }
    }
}
