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
    List<Crisis> crisesToLoad = new List<Crisis>();
    public List<Crisis> storyCrisises = new List<Crisis>();
    ActiveCrisis[] activeCrisses = new ActiveCrisis[3];
    
    //getter and setter for ActiveCrisis
    public ActiveCrisis[] ActiveCrisses
    {
        get { return activeCrisses; }
        set { activeCrisses = value; 
        Debug.Log("Active Crisises was edited!!"); }
    }

    public CrisisBox crisisBox;

    public UnityEvent PlayerPlayedCardEvent = new UnityEvent();
    //TODO:
    // Track cards applied to events
    public List<CrisisBox> crisisBoxes = new List<CrisisBox>();
    public string DEBUGCRISIS = "Kirby";
    int storyIndex = 0;

    private void Start() {
        //run this function 2 seconds later
        Invoke("TryStartCrisis", 2);
        crisesToLoad = new List<Crisis>(crisises);

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


    public void StartCrisisFromText(string crisisName)
    {
        Crisis crisis = getCrisis(crisisName);
        if (crisis == null)
        {
            Debug.LogWarning("Crisis not found: " + crisisName);
            return;
        }
        ActivateCrisis(crisis);
    }

    public Crisis FindCrisisFromCard(Card card){
        foreach (ActiveCrisis crisis in activeCrisses){
            if (crisis == null) {continue;}
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

    //Return a random active ActiveCrisis
    public ActiveCrisis GetRandomActiveCrisis()
    {
        //DO a controlled loop
        int index = Random.Range(0, activeCrisses.Length);
        int i = 0;
        while (activeCrisses[index] == null)
        {
            index = Random.Range(0, activeCrisses.Length);
            i++;
            if (i > 100)
            {
                Debug.LogError("Could not find a random active crisis");
                return null;
            }
        }
        return activeCrisses[index]; 
    }

    public Crisis GetRandomCrisis(){
        if(crisesToLoad.Count == 0){
            crisesToLoad = new List<Crisis>(crisises);
        }
        int randomIndex = Random.Range(0, crisesToLoad.Count);
        Crisis crisis = crisesToLoad[randomIndex];
        crisesToLoad.RemoveAt(randomIndex);
        return crisis;
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
        for(int i = 0; i < activeCrisses.Length; i++){

            //plz :( if you're dealing a statically sized array of objects which isn't pre-populated you need to do null checks!)
            if (activeCrisses[i] == null){continue;}
            if(!activeCrisses[i].crisis.NewTurn()){
                activeCrisses[i].EndCrisis();
                //activeCrisses[i] = null;
                continue;

            };
            //for each card in the crisis
            if(activeCrisses[i] != null){
                foreach (Card card in activeCrisses[i].playerCards)
                {
                    if(card == null){continue;}
                    //check if the card has a new turn trigger 
                    //TODO: null check
                    if (card == null){continue;}
                    card.CheckTrigger("Investment");
                }
            }
        }
        CheckEndings();
        TryStartCrisis();
    }            

    public void TryStartCrisis(){
        if(CanAddCrisis() == false){
            return;
        }
        Crisis crisis = null;
        if(HasStoryCrisis()){
            crisis = GetRandomCrisis();
            if(crisis != null){
                ActivateCrisis(crisis);
                return;
            }    
        }
        crisis = GetStoryCrisis();
        if(crisis != null){
            ActivateCrisis(crisis);
            return;
        }
    }
    
    public void CheckEndings(){
        Faction faction = GameMaster.factionController.CheckIfFactionsOver50();
        if(faction != null){
            faction.EndGame();
        }
    }

    public bool HasStoryCrisis(){
        if(storyIndex == 100){
            return true;
        }
        foreach(ActiveCrisis Acrisis in activeCrisses){
            // if Acrisis is in the storyCrisises list
            if(Acrisis == null){
                continue;
            }
            foreach(Crisis crisis in storyCrisises){
                if(Acrisis.crisis.Name == crisis.Name){
                    return true;
                }
            }
        }
        return false;
    }

    public Crisis GetStoryCrisis(){
        if(storyIndex >= storyCrisises.Count){
            GameMaster.dialoguePlayer.EndGame();
            storyIndex = 100;
            return null;
        }
        Crisis crisis = storyCrisises[storyIndex];
        storyIndex++;
        return crisis;
        
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
        CrisisBox targetBox = null;
        if(crisis.location != "None"){
            //for eahc crisis box
            foreach(CrisisBox crisisBox in crisisBoxes){
                //if the crisis box is the right location
                if(crisisBox.location == crisis.location){
                    targetBox = crisisBox;
                }
            }
        }
        if(targetBox == null){
            targetBox = PickACrisisBox();
        }

        for (int i = 0; i < activeCrisses.Length; i++)
        {
            if (activeCrisses[i] == null)
            {
                activeCrisses[i] = new ActiveCrisis(crisis, targetBox);
                GameMaster.dialoguePlayer.StartDialogue(crisis.dialogues);
                GameMaster._JL_EventMover.SetSingleActive(targetBox.gameObject);
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

    // /// <summary>
    // /// This ends the crisis and removes it from the active list
    // /// </summary>
    // /// <param name="crisis">The crisis to end</param>
    // public void EndCrisis(Crisis crisis)
    // {
    //     for (int i = 0; i < activeCrisses.Length; i++)
    //     {
    //         if (activeCrisses[i] != null && activeCrisses[i].crisis == crisis)
    //         {
    //             activeCrisses[i].EndCrisis();
    //             activeCrisses[i] = null;
    //             return;
    //         }
    //     }
    // }

    public void DisableCrisisBox(Crisis crisis){
        foreach(CrisisBox crisisBox in crisisBoxes){
        //if the crisis box is the right location
            if(crisisBox.crisis == crisis){
                crisisBox.EndCrisis();
            }
        }
    }

    public CrisisBox PickACrisisBox()
    {
        
        //creates a cached copy of crisis boxes
        List<CrisisBox> crisisBoxesCopy = new List<CrisisBox>();
        foreach(CrisisBox crisisBox in crisisBoxes){
            if(crisisBox.gameObject.active == false){
                crisisBoxesCopy.Add(crisisBox);
            }
        }
        int index = Random.Range(0, crisisBoxesCopy.Count);
        return crisisBoxesCopy[index]; 
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
        string[] progress = new string[3];
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
    Crisis Crisis;

    public Crisis crisis{
        get{return Crisis;}
        set{Crisis = value;
        Debug.LogWarning("CrisisMaster: Set crisis to new value");
        }
    }

    //public Timer timer;
    public Card[] playerCards = new Card[3];
    public Card[] AICards = new Card[3];
    public CrisisBox crisisBox;

    public ActiveCrisis(Crisis _crisis, CrisisBox _crisisBox)
    {
        crisis = _crisis.Copy();
        //timer = new Timer(crisis.DayLength, crisis.EndCrisis);
        crisis.StartCrisis();
        crisisBox = _crisisBox;
        crisisBox.ChangeEvent(crisis);
        //for each factionProgress in crisis
        foreach(KeyValuePair<Faction, int> entry in crisis.factionProgress){
            if(entry.Value != 0){
                Debug.LogWarning("Crisis had inpropiate faction progress!");
            }
        }
        //loop 3 times
        for (int i = 0; i < 3; i++)
        {
            TargetCrisis tg = GetTargetFromIndex(i);
            if(tg.gameObject.transform.childCount > 0)
            {
                Debug.LogWarning("Crisis has children!");
            }
            foreach(Transform child in tg.transform){
                child.gameObject.SetActive(false);
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }
    }

    public void NextTurn()
    {
        //timer.NextTurn();
        //crisis.NextTurn();
    }

    public void ApplyCard(Card card, int index, bool player)
    {
        #if DEBUG
        if(player){
            Debug.Log("PLAYER: Applying card " + card.Name + " to crisis " + crisis.Name + "Crisis has " + crisis.activeTurns + " of " + crisis.DayLength + " turns left");
        }
        else
        {
            Debug.Log("AI: Applying card " + card.Name + " to crisis " + crisis.Name + "Crisis has " + crisis.activeTurns + " of " + crisis.DayLength + " turns left");
        }
        
        #endif

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

    public Card GetLastPlayedCard(bool player = true){
        for (int i = 2; i > -1; i--)
        {
            if(player == true){
                if(playerCards[i] != null){
                    return playerCards[i];
                }
            }
            else{
                if(AICards[i] != null){
                    return AICards[i];
                }
            }
        }
        return null;
    }

    public TargetCrisis GetTargetFromIndex(int index)
    {
        //TargetCrisis target;
        //get the transform of the crisis box
        Transform boxTransform = crisisBox.transform;

        //get the "card slots" child
        Transform cardSlots = boxTransform.GetChild(5);

        //get the target

        foreach(TargetCrisis target in cardSlots.GetComponentsInChildren<TargetCrisis>()){
            //target = child.GetComponent<TargetCrisis>();
            if(target.player == true){
                continue;
            }
            if(target.index == index){
                return target;
            }
        }

        // switch(index){
        //     //gets the AIslot 0 target from the children of the crisis box  
        //     case 0:
                
        //         target = cardSlots.GetChild(3).GetComponent<TargetCrisis>();
        //         return target;
        //     //gets the AIslot 1 target from the children of the crisis box    
        //     case 1:
        //         target = cardSlots.GetChild(4).GetComponent<TargetCrisis>();             
        //         return target;
        //     //gets the AIslot 2 target from the children of the crisis box
        //     case 2:
        //         target = cardSlots.GetChild(5).GetComponent<TargetCrisis>();
        //         return target;
        //     default:
        //         Debug.LogWarning("CrisisMaster: GetTargetFromIndex: Index out of range");
        //         break;
        // }
        Debug.LogWarning("CrisisMaster: GetTargetFromIndex: No target found");
        //Debug.Break();        
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
