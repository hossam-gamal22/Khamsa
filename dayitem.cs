using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Project.UI.Views
{
    public class DayItem : MonoBehaviour
    {
        public TextMeshProUGUI dayText;
        public TextMeshProUGUI rewardText;
        public Button claimButton;
        public Button doubleButton;
        public GameObject dimOverlay;
        public GameObject checkMark;

        private int dayNumber;

        public void Init(int index, int rewardAmount)
        {
            dayNumber = index + 1;
            rewardText.text = rewardAmount.ToString();
            dayText.text = $"اليوم {ToArabicDay(index)}";
        }

        public void ApplyState(bool isActive, bool isClaimed, bool isFuture, int coinAmount)
        {
            if (isClaimed)
            {
                dayText.text = "استلمت";
                rewardText.text = "";
                checkMark.SetActive(true);
            }
            else
            {
                dayText.text = $"اليوم {ToArabicDay(dayNumber - 1)}";
                rewardText.text = coinAmount.ToString();
                checkMark.SetActive(false);
            }

            dimOverlay.SetActive(isClaimed || isFuture);

            claimButton.interactable = isActive && !isClaimed;
            doubleButton.interactable = isClaimed;

            float scale = isActive ? 1.2f : 1.0f;
            transform.localScale = Vector3.one * scale;

            Color labelColor = isActive
                ? new Color(0.44f, 0.89f, 0.56f)
                : Color.white;

            dayText.color = labelColor;
        }

        private string ToArabicDay(int index)
        {
            string[] days = { "الأول", "الثاني", "الثالث", "الرابع", "الخامس", "السادس", "السابع", "يوميًا" };
            return days[Mathf.Clamp(index, 0, days.Length - 1)];
        }
    }
}
