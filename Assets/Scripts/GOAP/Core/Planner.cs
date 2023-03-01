using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Planner
{
    /// <summary>
    /// this returns a Queue of possible plan for a npc to execute
    /// </summary>
    /// <param name="actions">all the actions this NPC can execute</param>
    /// <param name="goal">the specific goal the NPC is trying to achieve</param>
    /// <param name="NpcState">the current state of NPC</param>
    /// <returns></returns>
    public Queue<Action> Plan(List<Action> actions, Dictionary<string, float> goal, Dictionary<string, float> NpcState)
    {
        List<Action> useableActions = new List<Action>();
        foreach(Action action in actions)
        {
            if(isActionAchievable(action))
            {
                useableActions.Add(action);
            }
           
        }
        // Now we create the first node in the graph
        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0.0f, NpcState, null);
        bool success = buildGraph(start, leaves, useableActions, goal);
        if(!success)
        {
            Debug.Log("No plan");
            return null;
        }
        Node cheapest = null;
        foreach(Node node in leaves)
        {
            if(cheapest == null)
            {
                cheapest = node;
            }
            else if(node.Cost < cheapest.Cost)
            {
                cheapest = node;
            }
        }
        List<Action> result = new List<Action>();
        Node n = cheapest;
        while (n != null)
        {
            if(n.Action != null)
            {
                result.Insert(0,n.Action);
            }
            n = n.Parent;
        }
        Queue<Action> plan = new Queue<Action>();
        foreach (Action action in result)
            plan.Enqueue(action);
        return plan;
    }
    /// <summary>
    /// this method will attempt to create a plan for NPC to execute
    /// </summary>
    /// <param name="start">the sarting node</param>
    /// <param name="leaves">all the leaves if there are any</param>
    /// <param name="useableActions">all the actions that this perticular NPC can use</param>
    /// <param name="goal">the goal that this NPC wants to achieve</param>
    /// <returns></returns>
    /// <exception cref="System.NotImplementedException"></exception>
    private bool buildGraph(Node start, List<Node> leaves, List<Action> useableActions, Dictionary<string, float> goal)
    {
        return false;
    }

    /// <summary>
    /// This method returns if a certain action is achievable when appropriate conditions are fiven
    /// </summary>
    /// <param name="action">The action you want to evaluate</param>
    /// <returns></returns>
    bool isActionAchievable(Action action)
    {
        return true;
    }
}


public class Node
{
    /// <summary>
    /// the patent node of the connected node, Goal will have null as parent
    /// </summary>
    public Node Parent;
    /// <summary>
    /// cost accumulated as we progress to find a plan
    /// </summary>
    public float Cost;
    /// <summary>
    /// The state of world / NPC by the time Action assigned to this node is achieved
    /// </summary>
    public Dictionary<string, float> State;
    /// <summary>
    /// The action this node represents
    /// </summary>
    public Action Action;
    /// <summary>
    /// Creating a node for the tree
    /// </summary>
    /// <param name="parent">the parent node of this node</param>
    /// <param name="cost">the cost to persorm action in this node</param>
    /// <param name="allStates">all the states noc and world holds</param>
    /// <param name="action">the action that this node can execute</param>
    public Node(Node parent, float cost, Dictionary<string, float> allStates, Action action) 
    {
        this.Parent = parent;
        this.Cost = cost;
        this.State = allStates;
        this.Action = action;
    }
}



