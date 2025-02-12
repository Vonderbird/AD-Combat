using ADC.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sisus.Init;
using Sisus.Init.Internal;

namespace ADC
{
    public class CheckInitargsOnInstantiateFromScene : MonoBehaviour
    {
        [SerializeField] private GameObject perfabObject;
        private GameObject sampleObject;
        // Start is called before the first frame update
        void Start()
        {
            sampleObject = FindObjectOfType<ThunderkinWarWagon>(includeInactive:true).gameObject;
        }

        GameObject result;
        
        public void OnInstantiateClicked()
        {
            var ini1 = perfabObject.GetComponents<Initializer>();
            perfabObject.SetActive(false);
            result = Instantiate(perfabObject);
            var ini2 = result.GetComponents<Initializer>();
            Debug.Log($"ini1: {ini1.Length}, ini2: {ini2.Length}");
            StartCoroutine(CheckWithDelay(result, ini1));
        }

        IEnumerator CheckWithDelay(GameObject output, Initializer[] components)
        {
            yield return null;
            foreach (var comp in components)
            {
                output.AddComponent(comp.GetType());
            }
            var ini3 = result.GetComponents<Initializer>();
            yield return null;
            var ini4 = result.GetComponents<Initializer>();
            Debug.Log($"ini3: {ini3.Length}, ini4: {ini4.Length}");
        }

        public void OnInstantiateOnResult()
        {
            var ini1 = result.GetComponents<Initializer>();
            result = Instantiate(result);
            var ini2 = result.GetComponents<Initializer>();
            Debug.Log($"ini1: {ini1.Length}, ini2: {ini2.Length}");
        }
    }
}
