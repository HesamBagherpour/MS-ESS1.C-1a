using System;
using Runtime.Manager;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Collectable
{
    public class RockItem : CollectableBase
    {
        [field: SerializeField] public override string Id { get; set; }

        private int number = 0;
        private GameObject _fossilImage;
        private GameObject _fossilPickupVfx;

        public Action RockCollectabled;

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == 0)
                {
                    _fossilImage = transform.GetChild(i).gameObject;

                    continue;
                }

                if (i == 1)
                {
                    _fossilPickupVfx = transform.GetChild(i).gameObject;

                    continue;
                }

                var rock = transform.GetChild(i).gameObject.AddComponent<Rock>();
                rock.Destroy += RockDestroyed;
                number++;
            }
        }

        private void RockDestroyed()
        {
            number--;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SFX.hitRockSfx);
            if (number == 0)
            {
                Collectable();
            }
        }


        public override void Collectable()
        {
            var audio = AudioManager.Instance;
            var ui = UIManager.Instance;
            audio.PlaySFX(audio.SFX.foundFossilSfx);

             LevelManager.Instance.GetFossil();
            
             ui.ShowDialogue(Id, () =>
             {
                 ui.dialoguePopUp.Hide();
                 Destroy(_fossilImage.gameObject);
                 Destroy(_fossilPickupVfx.gameObject);
                 RockCollectabled?.Invoke();
             });
        }
    }
}