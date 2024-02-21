using System.Collections;
using System.Collections.Generic;
using CrazyGames;
using UnityEngine;


public class BanerController : MonoBehaviour
{
    [SerializeField] private CrazyBanner crazyBanner;

    private bool isBannerVisible = false;

    private void Start()
    {
        StartCoroutine(BannerController());
    }

    private IEnumerator BannerController()
    {
        while (true)
        {
            yield return new WaitForSeconds(90f);
            crazyBanner.gameObject.SetActive(true);
            if (isBannerVisible)
            {
                crazyBanner.MarkForRefresh();
            }
            else
            {
                isBannerVisible = true;
                crazyBanner.MarkVisible(true);
            }

            CrazyAds.Instance.updateBannersDisplay();
        }
    }
}