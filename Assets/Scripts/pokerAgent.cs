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

public class pokerAgent : Agent
{
    // Start is called before the first frame update
    private Character _agent;
    public Character agent
    {
        get { return _agent; }
        private set { _agent = value; }
    }
    public static int agentID = 0;
    GameObject agentBody;
    Hand hand;
    public static Dictionary<int, string> discardBinaries = new Dictionary<int, string>();
    //Need to look at documentation for what this does
    [Observable(numStackedObservations: 3)]
    PokerEnums.PokerEnums.HandResults currentHand
    {
        get { return currentHand; }
        set 
        {
            if (currentHand != value)
            {
                //This means that the current hand changed
                //If they got a better hand then they will gain a reward
                //If they got a worse hand then they get punished
                AddReward(((float)value - (float)currentHand) * 0.2f);
            }
            currentHand = value;
        }
    }


    public new void Awake()
    {
        base.Awake();
        discardBinaryOperations.PopulateDictionary(discardBinaries);

    }
    void Start()
    {
        agent = new Character(agentBody, ("AI" + agentID));
        agentID++;
        hand = agent.GetHand();
        Academy.Instance.AutomaticSteppingEnabled = false;
    }
    public override void OnEpisodeBegin()
    {

    }
    public void AdvanceStep()
    {
        currentHand = hand.HandResult;
        Academy.Instance.EnvironmentStep();
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        int discardKey = actionBuffers.DiscreteActions[0];
        string discardValue = discardBinaries[discardKey];
        List<Card> cardsToDiscard = new List<Card>();
        //This is kinda scuff but unfortunately because the discardValue does not change correctly calculating offset is kinda a headache
        //because although normally we can just i--; discardValue doesn't change so we just end up discarding every card
        //It's not that difficult but I don't feel like testing, and this is still O(2n) which is O(n) so who cares
        for(int i = 0; i < 5; i++)
        {
            if(discardValue.Substring(i, i+1).Equals("1"))
            {
                cardsToDiscard.Add(hand.GetCard(i));
            }
        }
        
        foreach(Card card in cardsToDiscard)
        {
            agent.DiscardCard(card); 
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(hand.HasAce());
    }

    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        actionMask.SetActionEnabled(0, 15, false);
        actionMask.SetActionEnabled(0, 23, false);
        actionMask.SetActionEnabled(0, 27, false);
        actionMask.SetActionEnabled(0, 29, false);
        if(!hand.HasAce())
        {
            actionMask.SetActionEnabled(0, 30, false);
        }
    }
}
