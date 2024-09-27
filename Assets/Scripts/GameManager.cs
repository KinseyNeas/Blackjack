using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Buttons on the screen
    public Button hitBtn;
    public Button standBtn;
    public Button dealBtn;
    [SerializeField] private AudioClip buttonClickSound;
    // Access player and dealer scripts
    public PlayerScript player;
    public PlayerScript dealer;
    // Access player and dealer hand totals
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI dealerScore;
    // Access the deck
    public GameObject deck;
    public static Sprite cardBack;
    // Winner banners
    public Canvas winnerBanner;
    void Start()
    {
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        dealBtn.onClick.AddListener(() => DealClicked());

        deck.GetComponent<DeckBehavior>().cardBack = cardBack;
    }
    void Update()
    {
        playerScore.text = player.GetHandValue().ToString();
    }
    private void DealClicked(){
        PlayButtonSound();
        winnerBanner.transform.GetChild(2).gameObject.SetActive(false);
        winnerBanner.transform.GetChild(1).gameObject.SetActive(false);
        winnerBanner.transform.GetChild(0).gameObject.SetActive(false);
        // Reset the player and dealer hands
        player.ResetHand();
        dealer.ResetHand();
        // Shuffle the deck
        deck.GetComponent<DeckBehavior>().Shuffle();
        // Pull new hands for the player and dealer
        player.StartHand();
        dealer.StartHand();
        // Hide dealer's 2nd card and set dealer score
        //dealer.hand[1].GetComponent<CardBehavior>().Flip();
        dealerScore.text = dealer.hand[0].GetComponent<CardBehavior>().GetValue().ToString();
        // Change button visibility
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
    }
    private void HitClicked(){
        PlayButtonSound();
        // Checks if player has less than 11 cards to draw
        if(player.GetHandIndex() <= 10) {
            player.GetCard(false);
        }
        // If the value of the player's hand is over 20, the round ends
        if(player.GetHandValue() > 20) {
            Invoke("EndRound",1);
        }
    }
    private void StandClicked(){
        PlayButtonSound();
        hitBtn.interactable = false;
        standBtn.interactable = false;
        // Flip 2nd card in dealer hand
        dealer.hand[1].GetComponent<CardBehavior>().Flip();
        dealerScore.text = dealer.GetHandValue().ToString();
        // Check if the dealer should keep pulling cards
        hitBtn.interactable = false;
        standBtn.interactable = false;
        Invoke("HitDealer",1.5f);
    }
    private void HitDealer(){
        // While dealer's hand is less than 17, keep pulling cards
        while(dealer.GetHandValue() < 17 && dealer.GetHandIndex() <= 10){
            dealer.GetCard(false);
            dealerScore.text = dealer.GetHandValue().ToString();
            if(dealer.GetHandValue() > 20) {
                Invoke("EndRound",1);
            }
        }
        Invoke("EndRound",1);
    }
    void EndRound(){
        hitBtn.interactable = true;
        standBtn.interactable = true;

        bool playerBust = player.GetHandValue() > 21;
        bool dealerBust = dealer.GetHandValue() > 21;
        // bool playerBJ = player.GetHandValue() == 21;
        // bool dealerBJ = dealer.GetHandValue() == 21;
        // bool endRound = true;

        if (playerBust && dealerBust) {
            Debug.Log("Both Bust");
        } else if (playerBust || (!dealerBust && dealer.GetHandValue() > player.GetHandValue())) {
            Debug.Log("Dealer Wins!"); 
            winnerBanner.transform.GetChild(0).gameObject.SetActive(true);
        } else if (dealerBust || (!playerBust && player.GetHandValue() > dealer.GetHandValue())) {
            Debug.Log("You Win!");    
            winnerBanner.transform.GetChild(1).gameObject.SetActive(true);
        } else if (dealer.GetHandValue() == player.GetHandValue()) {
            Debug.Log("There is a tie");
            winnerBanner.transform.GetChild(2).gameObject.SetActive(true);
        }
        
        hitBtn.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(false);
        dealBtn.gameObject.SetActive(true);
    }
    public void PlayButtonSound(){
        SoundFXManager.instance.PlaySoundFXClip(buttonClickSound,transform,0.5f);
    }
}
