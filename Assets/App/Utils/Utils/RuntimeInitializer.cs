using Core.Common;
using UnityEngine;

namespace Common.Utils
{
    public class RuntimeInitializer : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            GameObject controller = GameObject.Find("Controllers");
            controller.AddComponent<Bootstraper>();
        }
    }
}
