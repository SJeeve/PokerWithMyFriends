using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors.Reflection;
using JetBrains.Annotations;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using characterClass;
using PokerEnums;
using cardClass;
using binaryDiscard;

public class pokerAgent : Agent, Character
{
    // Start is called before the first frame update

    public Hand hand = new Hand();
    public string AgentName;

    public static int agentID = 0;
    GameObject agentBody;
    List<int[]> AllAgentHands = new List<int[]>();
    private int place = 0;
    private bool HasDiscarded = false;
    private bool ReadyForNewRound = true;
    private static Dictionary<int, string> discardBinaries = new Dictionary<int, string>();
    //Accidentally made a stack overflow error whoops
    PokerEnums.PokerEnums.HandResults _currentHand = PokerEnums.PokerEnums.HandResults.None;
    PokerEnums.PokerEnums.HandResults currentHand
    {
        get { return _currentHand; }
        set 
        {
            //Little check to make sure we never accidentally add a reward for a hand still in the process of discarding and drawing

            if (hand.GetLength() == 5)
            {
                Debug.Log($"Checking incoming value\n{value}");
                Debug.Log($"Current value\n{_currentHand}");
                if (_currentHand != PokerEnums.PokerEnums.HandResults.None && value != _currentHand)
                {
                    Debug.Log("IF STATEMENT FOR CURRENTHAND");
                    //This means that the current hand changed
                    //If they got a better hand then they will gain a reward
                    //If they got a worse hand then they get punished
                    Debug.Log(hand.ToString());
                    if (!ReadyForNewRound) 
                    {
                        Debug.Log("Change in reward: " + (((float)value - (float)_currentHand)) * 0.5f);
                        AddReward(((float)value - (float)_currentHand) * 0.2f);
                    }
                }
                _currentHand = value;
            }

        }
    }

    float handScore = 0;


    public new void Awake()
    {
        base.Awake();
        discardBinaryOperations.PopulateDictionary(discardBinaries);
    }
    public bool GetReadyForNewRound()
    {
        return ReadyForNewRound;
    }
    public void setReadyForNewRound(bool value)
    {
        ReadyForNewRound = value;
    }
    void Start()
    {
        Academy.Instance.AutomaticSteppingEnabled = false;
        Debug.Log("Initialized Hand");
        hand = new Hand();
        AgentName = "Agent " + agentID;
        agentID++;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 12;
        Debug.Log("Option pressed is " + (discreteActionsOut[0]));
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("OnEpisode Begin Called");
        for (int i = 0; i < 3; i++)
            AllAgentHands.Add(new int[]{ -1,-1,-1,-1,-1});
    }
    public void AdvanceStep()
    {
        RateHand();
        Debug.Log("AdvanceStep called");
        Debug.Log("Current hand: " + hand.ToString());
        Debug.Log("Current rating: " + currentHand);
        Academy.Instance.EnvironmentStep();
    }
    public void RateHand()
    {
        Debug.Log("RateHand function in pokeragent");
        hand.RateHand();
        currentHand = hand.HandResult;
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (HasDiscarded)
            return;
        hand.SortHand();
        Debug.Log("ActionReceived");
        int discardKey = actionBuffers.DiscreteActions[0];
        string discardValue = discardBinaries[discardKey];
        DiscardFromBinary(discardValue);
        HasDiscarded = true;
    }
    public void DiscardFromBinary(string discardValue)
    {
        List<Card> cardsToDiscard = new List<Card>();
        Debug.Log(discardValue);
        Debug.Log(hand.ToString());
        //This is kinda scuff but unfortunately because the discardValue does not change correctly calculating offset is kinda a headache
        //because although normally we can just i--; discardValue doesn't change so we just end up discarding every card
        //It's not that difficult but I don't feel like testing, and this is still O(2n) which is O(n) so who cares
        for (int i = 0; i < 5; i++)
        {
            if (discardValue[i].Equals('1'))
            {
                Debug.Log($"Hand length = {hand.GetLength()}\ni = {i}");
                Debug.Log($"Name of agent: {AgentName}");
                cardsToDiscard.Add(hand.GetCard(i));
            }
        }
        foreach (Card card in cardsToDiscard)
        {
            DiscardCard(card);
        }
        Debug.Log("Hand after discard");
        Debug.Log(hand.ToString());
    }
    public void AddCard(Card card)
    {
        hand.AddCard(card);
    }
    public void DiscardCard(Card card)
    {
        hand.DiscardCard(card);
    }
    public void DiscardCard(int i)
    {
        hand.DiscardCard(i);
    }
    public PokerEnums.PokerEnums.HandResults HandResult()
    {
        return hand.HandResult;
    }
    public override string ToString()
    {
        string output = $"{AgentName} has a \n";
        for (int i = 0; i < hand.GetLength(); i++)
        {
            output += hand.GetCard(i);
            output += "\n";
        }
        output += "Result of hand: " + HandResult().ToString();
        return output;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        currentHand = hand.HandResult;
        sensor.AddObservation(hand.HasAce());
        //sensor.AddObservation(place);
        int[] handIndexes = hand.GetHandIndexes();

        for(int i = 0; i < 5; i++)
        {
            sensor.AddObservation(handIndexes[i]);
        }
        /*Commented out code was for allowing the ai to learn with multiple other ai at the same time
        however I decided it would be easier to just have one train at a time. However in the future 
        when I implement betting it will be important to allow for multiple to train at a time so they can learn from each other*/
        /*for(int i = 0; i < AllAgentHands.Count;i++)
        {
            for(int j = 0; j < AllAgentHands[i].Length;j++)
            {
                sensor.AddObservation(AllAgentHands[i][j]);
            }
        }*/
        sensor.AddObservation(handScore);
        AddReward(handScore);
        //I could do a bit of math for this but I feel like a switch is a little easier on my brain rn
        /*switch (place) {
            //Once place has been determined we add the corresponding reward and end the episode
            case 0: AddReward(0); break;
            case 1: AddReward(1f); goto default;
            case 2: AddReward(-1f); goto default;
            case 3: AddReward(-1.5f); goto default;
            case 4: AddReward(-2f); goto default;
            default:
                EndEpisode();
                break;
        }*/

    }
    public void SetAllAgentHands(List<int[]> array)
    {
        AllAgentHands.Clear();
        AllAgentHands = array;
    }
    public void RewardAndEndEpisode()
    {
        Debug.Log("Ending Episode");
        EndEpisode();
        hand.DiscardAll();
        ReadyForNewRound = true;
        currentHand = PokerEnums.PokerEnums.HandResults.None;
        HasDiscarded = false;
        handScore = 0;
        AllAgentHands.Clear();

    }
    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        actionMask.SetActionEnabled(0, 15, false);
        actionMask.SetActionEnabled(0, 23, false);
        actionMask.SetActionEnabled(0, 27, false);
        actionMask.SetActionEnabled(0, 29, false);
        actionMask.SetActionEnabled(0, 30, hand.HasAce());
    }
}
