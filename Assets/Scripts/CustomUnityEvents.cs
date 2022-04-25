using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class IntEvent : UnityEvent<int>{}


public class FloatEvent : UnityEvent<float>{}

public class ManaEvent : UnityEvent<float, string>{}

public class StringEvent: UnityEvent<string> {}

public class PlayerHappinessEvent: UnityEvent<Faction.PlayerHappinessEnum> {}

public class AIHappinessEvent: UnityEvent<Faction.AIHappinessEnum> {}
