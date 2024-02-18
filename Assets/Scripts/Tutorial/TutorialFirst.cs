using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EventBusSystem;

[Serializable]
public class TutorialFirst : TutorialInitializeClass, IJimSignal
{
    public override void OnUpdate()
    {
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        tutorialParameters.TutorialExit.fadeImage.DOFade(0, 1f);
        tutorialParameters.Joystic.gameObject.SetActive(false);
        tutorialParameters.JimCamera.gameObject.SetActive(true);

        tutorialParameters.CharacterAnimator.SetBool("IsTutorial", true);
        tutorialParameters.CharacterAnimator.SetBool("Ishide", true);
        EventBus.Subscribe(this);
    }

    protected override void OnComplete()
    {
        EventBus.Unsubscribe(this);
        tutorialParameters.Joystic.gameObject.SetActive(true);
        tutorialParameters.JimCamera.gameObject.SetActive(false);
        tutorialParameters.CharacterAnimator.SetBool("Ishide", false);
    }

    public void Finish()
    {
        Complete();
    }
}

[Serializable]
public class TutorialFirstMenu : TutorialInitializeClass
{
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (SaveData.Instance.BuyWeapon.Count != 0)
        {
            Complete();
        }
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        SaveData.Instance.Wallet.AddMoney(250);
        tutorialParameters.StarGameButton.gameObject.SetActive(false);
        tutorialParameters.ShopMenuFinger.gameObject.SetActive(true);
        tutorialParameters.CloseShop.gameObject.SetActive(false);
        tutorialParameters.CloseUpgrade.gameObject.SetActive(false);
        tutorialParameters.ShopBuyFinger.gameObject.SetActive(true);
    }
}

[Serializable]
public class TutorialSecondMenu : TutorialInitializeClass
{
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (SaveData.Instance.FreeWeapon == WeaponType.PP)
        {
            Complete();
        }
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        tutorialParameters.StarGameButton.gameObject.SetActive(false);
        tutorialParameters.ShopMenuFinger.gameObject.SetActive(false);
        tutorialParameters.CloseShop.gameObject.SetActive(false);
        tutorialParameters.CloseUpgrade.gameObject.SetActive(false);
        tutorialParameters.ShopBuyFinger.gameObject.SetActive(false);
        tutorialParameters.SelectWeaponFinger.gameObject.SetActive(true);
    }
}

[Serializable]
public class TutorialThirdMenu : TutorialInitializeClass
{
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (SaveData.Instance.IsTutorialOpen)
        {
            Complete();
        }
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        tutorialParameters.SelectWeaponFinger.gameObject.SetActive(false);
        tutorialParameters.UpgradeSelectFinger.gameObject.SetActive(true);
    }
}

[Serializable]
public class TutorialFourthMenu : TutorialInitializeClass
{
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (SaveData.Instance.AttachList.Where(t => t.WeaponType == WeaponType.PP).ToList()[0].BoughtTypes.Count != 0)
        {
            Complete();
        }
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        tutorialParameters.UpgradeSelectFinger.gameObject.SetActive(false);
        tutorialParameters.UpgradeFinger.gameObject.SetActive(true);
    }
}

[Serializable]
public class TutorialFixMenu : TutorialInitializeClass
{
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (SaveData.Instance.AttachList.Where(t => t.WeaponType == WeaponType.PP).ToList()[0].AttachTypes.Count != 0)
        {
            Complete();
        }
    }
}

[Serializable]
public class TutorialFiveMenu : TutorialInitializeClass
{
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (SaveData.Instance.IsGameStarted)
        {
            Complete();
        }
    }

    protected override void OnBegin()
    {
        base.OnBegin();
        tutorialParameters.UpgradeFinger.gameObject.SetActive(false);
        tutorialParameters.StarGameButton.gameObject.SetActive(true);
        tutorialParameters.CloseShop.gameObject.SetActive(true);
        tutorialParameters.CloseUpgrade.gameObject.SetActive(true);
        tutorialParameters.StartGameFinger.gameObject.SetActive(true);
        tutorialParameters.ShopCloseFinger.gameObject.SetActive(true);
        tutorialParameters.UpgradeCloseFinger.gameObject.SetActive(true);
    }

    protected override void OnComplete()
    {
        tutorialParameters.StartGameFinger.gameObject.SetActive(false);
        tutorialParameters.ShopCloseFinger.gameObject.SetActive(false);
        tutorialParameters.UpgradeCloseFinger.gameObject.SetActive(false);
        SaveData.Instance.IsTutorialMenuCompleted = true;
        SaveData.Instance.Save();
    }
}