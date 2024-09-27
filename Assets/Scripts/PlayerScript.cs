using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.XR;

public class PlayerScript : MonoBehaviour
{
    private int handValue = 0;
    public List<GameObject> hand;
    public DeckBehavior deck;
    private int handIndex = 0;
    public Transform[] cardSlots;
    public bool isDealer;

    public void StartHand(){
        GetCard(false);
        
        if(isDealer) GetCard(true);
        else GetCard(false);
    }

    public void GetCard(bool isHidden){
        GameObject card = deck.DealCard();
        hand.Add(card);
        //Checking that if the card dealt is an ACE and it is one of the first two cards dealt, then it's value should be 11. If not, the value will remain 1.
        if(card.GetComponent<CardBehavior>().IsAce() && handIndex < 2){
            hand[handIndex].GetComponent<CardBehavior>().SetValue(11);
        }
        handValue += card.GetComponent<CardBehavior>().GetValue();
        if (isHidden) {
            card.GetComponent<CardBehavior>().SetIsHidden(true);
            card.gameObject.GetComponent<SpriteRenderer>().sprite = card.GetComponent<CardBehavior>().GetBackSprite();
        
        }
        card.gameObject.SetActive(true);
        handIndex++;
        ArrangeHand();
    }
    public int GetHandValue(){
        return handValue;
    }
    public int GetHandIndex(){
        return handIndex;
    }
    public void ResetHand(){
        handIndex = 0;
        handValue = 0;
        foreach(GameObject card in hand){
            card.SetActive(false);
            if(card.GetComponent<CardBehavior>().IsHidden()){
                card.GetComponent<CardBehavior>().FlipSprite();
            }
        }
        hand.Clear();
    }
    private void ArrangeHand(){
        int start;
        if(handIndex == 1) start = 5;
        else if(handIndex == 2) start = 4;
        else if(handIndex == 3) start = 3;
        else if(handIndex == 4 || handIndex == 7) start = 2;
        else if(handIndex == 5 || handIndex == 8 || handIndex == 9) start = 1;
        else start = 0;
        if(handIndex < 6){
            for(int i = 0; i < handIndex; ++i){
                hand[i].GetComponent<CardBehavior>().SetPosition(cardSlots[start+i*2].position);
                
            }
        } else {
            for(int i = 0; i < handIndex; ++i){
                hand[i].GetComponent<CardBehavior>().SetPosition(cardSlots[start+i*2].position);
            }
        }
    }
}
