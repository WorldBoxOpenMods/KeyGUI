using System.Linq;
using KeyGeneralPurposeLibrary.Assets;
using UnityEngine;
using UnityEngine.UI;

namespace KeyGUI.Framework.Powers {
  public abstract class KeyGuiPower : KeyGuiComponent {
    public readonly GodPower Power;
    public readonly PowerButton Button;
    protected KeyGuiPower(GodPower power) {
      Power = power;
      Power.click_special_action = ClickWithPower;
      Power.select_button_action = PowerButtonPress;
      Sprite buttonParentSprite = KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite");
      Sprite buttonSprite = KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite");
      GameObject buttonParent = null;
      {
        GameObject[] objectsOfTypeAll = Resources.FindObjectsOfTypeAll<GameObject>();
        GameObject inspectButtonObject = objectsOfTypeAll.FirstOrDefault(t => t.gameObject.gameObject.name == "inspect");
        if (inspectButtonObject != null) {
          inspectButtonObject.SetActive(false);
          buttonParent = Object.Instantiate(inspectButtonObject);
          inspectButtonObject.SetActive(true);
        }
      }
      if (buttonParent != null) {
        buttonParent.SetActive(false);
        buttonParent.transform.Find("Icon").GetComponent<Image>().sprite = buttonParentSprite;
        PowerButton powerButton = buttonParent.GetComponent<PowerButton>();
        powerButton.open_window_id = string.Empty;
        powerButton.type = PowerButtonType.Active;
        powerButton.godPower = Power;
        powerButton.icon.sprite = buttonSprite;
        Button = powerButton;
      } else {
        Button = null;
      }
      AssetManager.powers.add(Power);
    }

    protected abstract bool ClickWithPower(WorldTile pTile, string pPowerID);
    protected abstract bool PowerButtonPress(string pPower);
  }
}
