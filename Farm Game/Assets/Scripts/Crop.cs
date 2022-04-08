using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crop : MonoBehaviour
{
    public string cropName;
    public bool geminating, maturing, _canHarvest, showTimer;
    public float geminatingTime, maturingTime;
    public Image Timer;
    public int stateID;
    public float time;
    public Transform timerPos;
    Image UIused;
    public float timeToState, curStateTime;
    public Sprite geminatedSprite, maturedSprite;
    public GameObject field;
    public SpriteRenderer renderer;
    GameObject curSprite;
    Renderer rd;

    // Start is called before the first frame update
    void Start()
    {
        geminating = true;
        showTimer = true;
        stateID = 0;
        curStateTime = geminatingTime;
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToState <= 0)
        {
            timeToState = curStateTime;
        }
        else
        {
            timeToState -= Time.deltaTime;  
        }

        if(geminating && stateID == 0)
        {
            isGeminating();
            
            if(timeToState <= 0)
            {
                curStateTime = maturingTime;
                geminating = false;
                showTimer = true;
            } 
        }

        else if(!geminating && stateID == 0)
        {
            maturing = true;
            stateID ++;
        }

        if(maturing && stateID == 1)
        {
            isMaturing();
    
            if(timeToState <= 0)
            {
                curStateTime = 0f;
                maturing = false;    
                showTimer = true;
            }
        }

        else if(!maturing && stateID == 1)
        {
            _canHarvest = true;
            UIused.gameObject.SetActive(false);
            stateID ++;
        }

        if(_canHarvest && stateID == 2)
        {
            canHarvest();
        }

        
        //Timer Function

        if(UIused.fillAmount >= 1 && timeToState <= 0)
        {
            UIused.fillAmount = 0f;
        }

        time = UIused.fillAmount;

        UIused.fillAmount = Mathf.Abs(1 - (timeToState / curStateTime));
        UIused.transform.position = Camera.main.WorldToScreenPoint(timerPos.position);
    }

    public void isGeminating()
    {
        if(showTimer == true)
        {
            UIused = Instantiate(Timer, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
            showTimer = false;
        }
        
    }

    public void isMaturing()
    {
        if(showTimer == true)
        {
            renderer.sprite = geminatedSprite;
            showTimer = false;
        }
        
    }

    public void canHarvest()
    {
        if(showTimer == true)
        {
            renderer.sprite = maturedSprite;
            showTimer = false;
        }  
    }

    public void Harvest()
    {
        Debug.Log("Harvested");
        Instantiate(field, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
