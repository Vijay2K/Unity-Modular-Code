using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SavingSystem
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}
