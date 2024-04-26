using TMPro;
using UnityEngine;

namespace ModularWeapons.UI
{
    public class AmmoUIElement : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _magazine;
        [SerializeField] TextMeshProUGUI _reserve;

        public void UpdateUI(int magazine)
        {
            _magazine.text = magazine.ToString();
        }

        public void UpdateUI(int magazine, int reserve)
        {
            _magazine.text = magazine.ToString();
            _reserve.text = reserve.ToString();
        }
    }
}
