using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayScript : MonoBehaviour
{
    [SerializeField] private Health health = null;
    [SerializeField] private GameObject healthBarParent = null;
    [SerializeField] private Image healthBarImage = null;

    private void Awake() {
        health.ClientOnHealthUpdated+=HandleHealthUpdated;
    }

    private void OnDestroy() {
        health.ClientOnHealthUpdated-=HandleHealthUpdated;
    }
    private void HandleHealthUpdated(int current, int max){
        healthBarImage.fillAmount=(float)current/max;
    }
    private void OnMouseEnter() {
        healthBarParent.SetActive(true);
    }
    private void OnMouseExit() {
        healthBarParent.SetActive(false);
    }

}
