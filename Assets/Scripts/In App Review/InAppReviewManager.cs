using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InAppReviewManager : MonoBehaviour
{

    private void Awake()
    {
        RatePopup.launchInAppReviewEvent += LaunchInAppReview;

    }

    private void OnDestroy()
    {
        RatePopup.launchInAppReviewEvent += LaunchInAppReview;
    }

    private void LaunchInAppReview()
    {
        StartCoroutine(LaunchingInAppReview());
    }

    private IEnumerator LaunchingInAppReview()
    {
        return null;
        
    }
}
