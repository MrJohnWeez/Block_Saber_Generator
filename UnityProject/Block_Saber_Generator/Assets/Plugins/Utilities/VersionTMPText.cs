using UnityEngine;
using TMPro;

namespace Utilities
{
    [RequireComponent(typeof(TMP_Text))]
    public class VersionTMPText : MonoBehaviour
    {
        private TMP_Text textObject = null;


        public TMP_Text TextObject { get => textObject; set => textObject = value; }


        void Start()
        {
            textObject = GetComponent<TMP_Text>();
            textObject.text = "v" + Application.version;
        }
    }
}