using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameModel : ScriptableObject
{
    [System.Serializable]
    public struct attributes
    {
        public string name;
        public string initialMessage;
        public string[] rules;
    }

    public attributes attribute;
}
