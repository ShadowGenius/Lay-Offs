using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerMovement movement;

    public List<Action> playerActions = new List<Action>();
}
