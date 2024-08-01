using UnityEngine;

public class DetachOnEnable : MonoBehaviour
{
    void OnEnable()
    {
        transform.SetParent(null);
    }
}
/*
 * 
Using OnEnable() instead of Start() has specific advantages in this context:

Immediate Detachment: OnEnable() is called every time the GameObject is enabled, whereas Start() is called only once when the script is first initialized. If the particle system is disabled and then re-enabled, OnEnable() will be called again, ensuring the particle system detaches every time it is enabled. Start() would not handle re-enabling.

Lifecycle Events: OnEnable() is part of the Unity event lifecycle that ensures operations tied to enabling/disabling are handled correctly. Using OnEnable() for actions directly related to enabling the GameObject is a more appropriate lifecycle event than Start().

Script Execution Order: Actions in OnEnable() occur after the GameObject is enabled, but before the first frame update. This ensures that the particle system is detached immediately upon enabling, whereas Start() may introduce slight delays.
*/