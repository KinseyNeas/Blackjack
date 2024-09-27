using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class StartScript : MonoBehaviour
{
    // Button to start and buttons to change card back color
    public Button startBtn, redCardBtn, blueCardBtn, greenCardBtn, chooseBackBtn, rulesBtn, exitBtn1, exitBtn2;
    // Card back sprites
    public Sprite redCard, blueCard, greenCard;
    // Current Game Deck Design
    public Sprite cardBack;
    // Assets for card background animation
    public Sprite[] cardSprites;
    public GameObject Card;
    private List<GameObject> cardArr = new List<GameObject>();
    public float radius;
    [SerializeField] private AudioClip buttonClickSound;
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(() => StartClicked());
        redCardBtn.onClick.AddListener(() => RedCardClicked());
        blueCardBtn.onClick.AddListener(() => BlueCardClicked());
        greenCardBtn.onClick.AddListener(() => GreenCardClicked());
        chooseBackBtn.onClick.AddListener(() => PlayButtonSound());
        rulesBtn.onClick.AddListener(() => PlayButtonSound());
        exitBtn1.onClick.AddListener(() => PlayButtonSound());
        exitBtn2.onClick.AddListener(() => PlayButtonSound());
        DisplayCards(radius,0,0,0.3f);
    }
    void Update(){
        if(cardArr.Count > 0){
            foreach(GameObject card in cardArr){
                card.GetComponent<CardBehavior>().SetBackSprite(cardBack);
            }
        }
    }
    private void StartClicked(){
        PlayButtonSound();
        GameManager.cardBack = cardBack;
        SceneManager.LoadScene("MainScene");
    }
    private void RedCardClicked(){
        PlayButtonSound();
        cardBack = redCard;
    }
    private void BlueCardClicked(){
        PlayButtonSound();
        cardBack = blueCard;
    }
    private void GreenCardClicked(){
        PlayButtonSound();
        cardBack = greenCard;
    }
    public void PlayButtonSound(){
        SoundFXManager.instance.PlaySoundFXClip(buttonClickSound,transform,0.5f);
    }
    private void DisplayCards(float radius, float x, float y, float scale){
        int num = cardSprites.Length;
        int idx = 0;
        for(double theta = 0; theta < 2*Math.PI; theta += 2*Math.PI/num){
            Vector3 pos = new Vector3((float)(radius*Math.Cos(theta)+x), (float)(radius*Math.Sin(theta)+y),0);
            Quaternion rot = Quaternion.Euler(0,0,(float)(theta*180/Math.PI-90));
            GameObject newCard = Instantiate(Card, pos, rot);
            // Debug.Log("Card " + idx + " has a position of " + pos + " and a rotation of " + rot);
            cardArr.Add(newCard);
            newCard.GetComponent<CardBehavior>().SetSprite(cardSprites[idx]);
            newCard.GetComponent<CardBehavior>().SetBackSprite(cardBack);
            if(idx%2 != 0) newCard.GetComponent<CardBehavior>().FlipSprite(); // 
            ++idx;
        }
        foreach(GameObject card in cardArr){
            card.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
