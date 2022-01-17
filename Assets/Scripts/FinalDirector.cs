using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalDirector : MonoBehaviour
{
    Burger burger;
    Lettuce lettuce;
    Bread bread;
    Cheddar cheddar;
    GameObject gameOverScreen, gameOverText;
    bool isOverScreen = false;
    // Start is called before the first frame update
    void Start()
    {
        burger = GameObject.Find("BurgerBurger").GetComponent<Burger>();
        lettuce = GameObject.Find("BurgerLettuce").GetComponent<Lettuce>();
        bread = GameObject.Find("BurgerBread").GetComponent<Bread>();
        cheddar = GameObject.Find("BurgerCheddar").GetComponent<Cheddar>();

        burger.isObsessed = true;
        lettuce.isObsessed = true;
        bread.isObsessed = true;
        cheddar.isObsessed = true;

        GameObject end = GameObject.Find("EndScene");
        end.GetComponent<RawImage>().canvasRenderer.SetAlpha(0);
        GameObject endText = GameObject.Find("EndText");
        endText.GetComponent<Text>().canvasRenderer.SetAlpha(0);

        gameOverScreen = GameObject.Find("GameOver");
        gameOverText = GameObject.Find("GameOverText");

        gameOverScreen.SetActive(false);
        gameOverText.SetActive(false);

        FinalClown clownController1 = GameObject.Find("Clown1").GetComponent<FinalClown>();
        FinalClown clownController2 = GameObject.Find("Clown2").GetComponent<FinalClown>();
        FinalClown clownController3 = GameObject.Find("Clown3").GetComponent<FinalClown>();
        FinalClown clownController4 = GameObject.Find("Clown4").GetComponent<FinalClown>();

        Debug.Log(clownController1);
        Debug.Log(clownController2);
        Debug.Log(clownController3);
        Debug.Log(clownController4);
        clownController1.Init();
        clownController2.Init();
        clownController3.Init();
        clownController4.Init();
    }

    // Update is called once per frame
    void Update()
    {

        if (isOverScreen)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("GameScene");
            }
        }

    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameOverText.SetActive(true);
        gameOverScreen.GetComponent<RawImage>().canvasRenderer.SetAlpha(0.1f);
        Invoke("SetOverScreen", 3.0f);
        gameOverScreen.GetComponent<RawImage>().CrossFadeAlpha(1f, 3f, false);
    }

    void SetOverScreen()
    {
        isOverScreen = true;
    }
}
