using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite cardBack;
    public GameObject Card;
    public Sprite[] cardSprites;
    public List<GameObject> Deck = new List<GameObject>();
    private int currentIndex = 0;
    void Start()
    {
        SetUpDeck();
    }

    void SetUpDeck(){
        int value = 0;
        for(int i = 0; i < cardSprites.Length; ++i){
            GameObject newCard = Instantiate(Card, new Vector3(0,0,0), Quaternion.identity);
            newCard.SetActive(false);
            // Value of card
            value = (i+1)%13;
            // Check if the card is an ACE
            if(value == 1){
                newCard.GetComponent<CardBehavior>().SetAce();
            } 
            // Check if the card is a FACE card
            if (value > 10 || value == 0){
                value = 10;
            }
            // Add card value and sprite to the deck
            newCard.GetComponent<CardBehavior>().SetValue(value);
            newCard.GetComponent<CardBehavior>().SetSprite(cardSprites[i]);
            newCard.GetComponent<CardBehavior>().SetBackSprite(cardBack);

            Deck.Add(newCard);
        }
    }
    public void Shuffle(){
        for(int pos = Deck.Count - 1; pos > 0; --pos){
            // Generates random position in deck
            int rpos = Mathf.FloorToInt(Random.Range(0.0f,1.0f)* Deck.Count - 1) + 1;
            
            // Swaps card in current position with card in random position
            GameObject card = Deck[pos];
            Deck[pos] = Deck[rpos];
            Deck[rpos] = card;

        }
        currentIndex = 1;
    }

    public GameObject DealCard(){
        GameObject card = Deck[currentIndex];
        currentIndex++;
        return card;
    }
}
