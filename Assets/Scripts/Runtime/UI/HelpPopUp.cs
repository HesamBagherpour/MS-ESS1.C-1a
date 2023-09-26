namespace Runtime.UI
{
    public class HelpPopUp : BasePage
    {
        public override void Awake()
        {
            base.Awake();

            backBtn.onClick.AddListener(() => Hide());
        }
    }
}