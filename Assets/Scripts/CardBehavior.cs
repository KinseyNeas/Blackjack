using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CardBehavior : MonoBehaviour
{
    private int value;
    private bool ace = false;
    private Sprite cardFace;
    private Sprite cardBack;
    private bool isHidden = false;
    private Animator cardAnimator;
    private Vector3 position;
    void Start(){
       cardAnimator = GetComponent<Animator>();
    }
    void Update(){
        if(cardBack != null && cardBack.name != gameObject.GetComponent<SpriteRenderer>().sprite.name && isHidden){
            gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;
        }
        Debug.Log("Card position in CARD ASSET: " + position);
        Debug.Log("ACTUAL Card position: " + transform.position);
    }
    public int GetValue(){
        return value;
    }
    public void SetValue(int newValue){
        value = newValue;
    }
    public Sprite GetFaceSprite()
    {
        return cardFace;
    }
    public Sprite GetBackSprite()
    {
        return cardBack;
    }
    public void SetSprite(Sprite newSprite)
    {
        cardFace = newSprite;
        isHidden = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
    public void SetBackSprite(Sprite newSprite){
        cardBack = newSprite;
    }
    public void SetAce(){
        ace = true;
    }
    public bool IsAce(){
        return ace;
    }
    public void SetPosition(Vector3 newPosition){
         position = newPosition;
         transform.position = position;
         Debug.Log("Card position after set: " + transform.position);
    }
    // public void MoveCardToPosition(){
    //      gameObject.transform.position = position;
    // }
    public bool IsHidden(){
        return isHidden;
    }
    public void SetIsHidden(bool hidden){
        isHidden = hidden;
    }
    public void Flip(){
        cardAnimator = GetComponent<Animator>();
        cardAnimator.SetTrigger("FlipCard");
    }
    public void FlipSprite(){
        if(isHidden){
            gameObject.GetComponent<SpriteRenderer>().sprite = cardFace;
            isHidden = false;
        } else {
            gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;
            isHidden = true;
        }
    }
    // public void DealCard(){
    //     cardAnimator = GetComponent<Animator>();
    //     cardAnimator.SetTrigger("DealCard");
    // }
}
