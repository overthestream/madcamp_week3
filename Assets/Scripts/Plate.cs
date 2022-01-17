using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    Bread bread;
    Burger burger;
    Lettuce lettuce;
    Cheddar cheddar;
    GameObject playerCamera, end, endText;
    FinalClown clown1, clown2, clown3, clown4;
    // Start is called before the first frame update
    void Start()
    {
        bread = GameObject.Find("BurgerBread").GetComponent<Bread>();
        burger = GameObject.Find("BurgerBurger").GetComponent<Burger>();
        lettuce = GameObject.Find("BurgerLettuce").GetComponent<Lettuce>();
        cheddar = GameObject.Find("BurgerCheddar").GetComponent<Cheddar>();
        playerCamera = GameObject.Find("Capsule");

        clown1 = GameObject.Find("Clown1").GetComponent<FinalClown>();
        clown2 = GameObject.Find("Clown2").GetComponent<FinalClown>();
        clown3 = GameObject.Find("Clown3").GetComponent<FinalClown>();
        clown4 = GameObject.Find("Clown4").GetComponent<FinalClown>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bread.getIsObsessed() && burger.getIsObsessed() && lettuce.getIsObsessed() && cheddar.getIsObsessed() && IsPlayerInNear(2.0f) && Input.GetKeyDown(KeyCode.E))
        {
            bread.setIsOver();
            burger.setIsOver();
            lettuce.setIsOver();
            cheddar.setIsOver();

            bread.transform.position = this.transform.position + new Vector3(0, 0.1f, 0); 
            burger.transform.position = this.transform.position + new Vector3(0, 0.07f, 0);
            lettuce.transform.position = this.transform.position + new Vector3(0, 0.1f, 0);
            cheddar.transform.position = this.transform.position + new Vector3(0, 0.1f, 0);

            clown1.Die();
            clown2.Die();
            clown3.Die();
            clown4.Die();

            Invoke("ChangeToEndScene", 2.0f);
        }
    }

    bool IsPlayerInNear(float threshold)
    {
        if ((playerCamera.transform.position - transform.position).magnitude < threshold)
            return true;
        else return false;
    }

    void ChangeToEndScene()
    {
        end = GameObject.Find("EndScene");
        end.GetComponent<RawImage>().CrossFadeAlpha(1, 1.5f, false);

        endText = GameObject.Find("EndText");
        endText.GetComponent<Text>().CrossFadeAlpha(1, 1.5f, false);
    }
}
