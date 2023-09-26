using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Extra
{
    [Serializable]
    public class CharacterManagerUI
    {
        public List<Sprite> playerImage;
        public Image playerImg;

        public Button nextBtn;
        public Button previousBtn;

        private int count;
        private int number;


        public void Initialize()
        {
            count = playerImage.Count - 1;

            nextBtn.onClick.RemoveAllListeners();
            nextBtn.onClick.AddListener(Next);

            previousBtn.onClick.RemoveAllListeners();
            previousBtn.onClick.AddListener(Previous);

            number = PlayerPrefs.GetInt("character");
            SetImage(number);
        }

        private void Next()
        {
            number++;
            if (number > count)
            {
                number = 0;
            }

            SetImage(number);
        }

        private void Previous()
        {
            number--;
            if (number < 0)
            {
                number = count;
            }

            SetImage(number);
        }

        private void SetImage(int value)
        {
            playerImg.sprite = playerImage[value];
            PlayerPrefs.SetInt("character", value);
        }
    }
}