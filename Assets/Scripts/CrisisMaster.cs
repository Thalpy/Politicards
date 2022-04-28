using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used to store the list of possible crisises.
/// It's supposed to be added to in the unity editor.
/// </summary>

public class CrisisMaster : MonoBehaviour
{
    [SerializeField]
    public List<Crisis> crisises = new List<Crisis>();
    ActiveCrisis[] activeCrisses = new ActiveCrisis[3];
    
    //getter and setter for ActiveCrisis
    public ActiveCrisis[] ActiveCrisses
    {
        get { return activeCrisses; }
        set { activeCrisses = value; }
    }

    public CrisisBox crisisBox;

    public UnityEvent PlayerPlayedCardEvent = new UnityEvent();
    //TODO:
    // Track cards applied to events
    public List<CrisisBox> crisisBoxes = new List<CrisisBox>();

    // Start is called before the first frame update
    void Start()
    {
        //get the kirby crisis
        Crisis kirby = getCrisis("Kirby's Fooking Pissed");
        //Active the crisis
        ActivateCrisis(kirby);
    }

    public Crisis getCrisis(string name)
    {
        foreach (Crisis crisis in crisises)
        {
            if (crisis.Name == name)
            {
                return crisis;
            }
        }
        Debug.LogWarning("Crisis not found: " + name);
        return null;
    }

    public Crisis FindCrisisFromCard(Card card){
        foreach (ActiveCrisis crisis in activeCrisses){
            foreach(Card p_card in crisis.playerCards){
                if(p_card == card){
                    return crisis.crisis;
                }
            }
            foreach(Card ai_card in crisis.AICards){
                if(ai_card == card){
                    return crisis.crisis;
                }
            }
        }
        return null;
    }

    public ActiveCrisis FindActiveCrisisFromCard(Card card){
        foreach (ActiveCrisis crisis in activeCrisses){
            foreach(Card p_card in crisis.playerCards){
                if(p_card == card){
                    return crisis;
                }
            }
            foreach(Card ai_card in crisis.AICards){
                if(ai_card == card){
                    return crisis;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Does new turn stuff like check flags and the like
    /// </summary>
    public void NewTurn()
    {
        foreach (ActiveCrisis crisis in activeCrisses)
        {
            crisis.crisis.NewTurn();
            //for each card in the crisis
            foreach (Card card in crisis.playerCards)
            {
                //check if the card has a new turn trigger 
                card.CheckTrigger("Investment");
            }
        }
    }            

    //determines if a new crisis can be added
    public bool CanAddCrisis()
    {
        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    // activates a crisis
    public void ActivateCrisis(Crisis crisis)
    {
        if(!CanAddCrisis())
        {
            Debug.Log("CrisisMaster: All crises are active when we tried to activate a new one.");
            Debug.Break();
            return;
        }

        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] == null)
            {
                activeCrisses[i] = new ActiveCrisis(crisis, crisisBox);
                GameMaster.dialoguePlayer.StartDialogue(crisis.dialogues);
                return;
            }
        }
    }

    internal void RemoveCrisis(Crisis crisis)
    {
        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] != null && activeCrisses[i].crisis == crisis)
            {
                activeCrisses[i] = null;
                return;
            }
        }
    }

    /// <summary>
    /// This ends the crisis and removes it from the active list
    /// </summary>
    /// <param name="crisis">The crisis to end</param>
    public void EndCrisis(Crisis crisis)
    {
        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] != null && activeCrisses[i].crisis == crisis)
            {
                activeCrisses[i].EndCrisis();
                activeCrisses[i] = null;
                return;
            }
        }
    }

    public bool isActiveCrisis(Crisis crisis)
    {
        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] != null && activeCrisses[i].crisis == crisis)
            {
                return true;
            }
        }
        return false;
    }

    public void ApplyCard(Card card, Crisis crisis, int index, bool player)
    {
        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] != null && activeCrisses[i].crisis == crisis)
            {
                activeCrisses[i].ApplyCard(card, index, player);
                if (player){PlayerPlayedCardEvent.Invoke();} //if player, then invoke the player played card event
                if (!player){GameMaster.NextTurn();}
                return;
            }
        }
    }

    //Checks if you can add a card to a specific crisis or not
    public bool CanAddCard(Crisis crisis, int index, bool player = true)
    {
        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] != null && activeCrisses[i].crisis == crisis)
            {
                return activeCrisses[i].CanAddCard(index, player);
            }
        }
        return false;
    }

    //Lists the active crises progress for use with the editor
    public void SpeakActiveCrisisProgress()
    {
        string[] progress = new string[activeCrisses.Length];
        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] != null)
            {
                progress[i] = activeCrisses[i].crisis.Name + ": " + activeCrisses[i].crisis.SpeakProgress();
            }
            else
            {
                progress[i] = "Crisis slot " + i + " is empty";
            }
        }
        foreach(string entry in progress)
        {
            Debug.Log(entry);
        }
    }

    public CrisisBox GetFreeCrisisBox()
    {
        foreach(CrisisBox box in crisisBoxes)
        {
            if(box.crisis == null)
            {
                return box;
            }
        }
        return null;
    }

}

[System.Serializable]
public class ActiveCrisis
{
    public Crisis crisis;
    public Timer timer;
    public Card[] playerCards = new Card[3];
    public Card[] AICards = new Card[3];
    public CrisisBox crisisBox;

    public ActiveCrisis(Crisis _crisis, CrisisBox _crisisBox)
    {
        crisis = _crisis.Copy();
        timer = new Timer(crisis.DayLength, crisis.EndCrisis);
        crisis.StartCrisis();
        crisisBox = _crisisBox;
        crisisBox.ChangeEvent(crisis);
    }

    public void ApplyCard(Card card, int index, bool player)
    {
        if (player)
        {
            if (playerCards[index] != null)
            {
                Debug.LogWarning("CrisisMaster: Player card already set");
                return;
            }
            playerCards[index] = card;
        }
        else
        {
            if (AICards[index] != null)
            {
                Debug.LogWarning("CrisisMaster: AI card already set");
                Debug.Break();
                return;
            }
            AICards[index] = card;
        }
    }

    //Checks if you can add a card to an index or not
    public bool CanAddCard(int index, bool player)
    {
        if (player)
        {
            if (playerCards[index] != null)
            {
                return false;
            }
        }
        else
        {
            if (AICards[index] != null)
            {
                return false;
            }
        }
        return true;
    }

    public Card GetLastPlayedAICard(){
        for (int i = 2; i < AICards.Length; i--)
        {
            if (AICards[i] != null)
            {
                return AICards[i];
            }
        }
        return null;
    }

    public void EndCrisis()
    {
        crisis.EndCrisis();
        crisisBox.EndCrisis();
        crisisBox = null;
        crisis = null;
    }

    /// <summary>
    /// a function to get all the cards so far played on the crisis.
    /// required for the AI to evaluate the crisis progress
    /// </summary>
    /// <returns>A list of all the cards played on the crisis</returns>
    public List<Card> GetAllPlayedCards()
    {
        List<Card> playedCards = new List<Card>();
        foreach (Card card in playerCards)
        {
            if (card != null)
            {
                playedCards.Add(card);
            }
        }
        foreach (Card card in AICards)
        {
            if (card != null)
            {
                playedCards.Add(card);
            }
        }
        return playedCards;
    }
}
