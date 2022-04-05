using UnityEngine;

namespace Utilities.Components
{
    public class URLOpener : MonoBehaviour
    {
        [SerializeField] private string _url = "";


        public void OpenURL()
        {
            Application.OpenURL(_url);
        }
    }
}
