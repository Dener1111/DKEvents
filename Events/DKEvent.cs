using UnityEngine;

namespace DKEvents
{
    public struct Empty { }

    [CreateAssetMenu(fileName = "DKEvent", menuName = "ScriptableObjects/DKEvents/Basic", order = 1)]
    public class DKEvent : DKEventBase<Empty> { }
}
