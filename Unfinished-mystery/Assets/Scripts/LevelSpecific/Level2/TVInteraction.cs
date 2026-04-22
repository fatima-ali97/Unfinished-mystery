using System.Collections;
using UnityEngine;

public class TVInteraction : MonoBehaviour
{
    public GameObject tvStatic;
    public GameObject tvMessage;
    public float staticDuration = 1.5f;

    private Coroutine tvRoutine;

    private void Awake()
    {
        if (tvStatic != null)
            tvStatic.SetActive(false);

        if (tvMessage != null)
            tvMessage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED TV");

            if (tvRoutine != null)
                StopCoroutine(tvRoutine);

            tvRoutine = StartCoroutine(ShowTVSequence());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER EXITED TV");

            if (tvRoutine != null)
                StopCoroutine(tvRoutine);

            if (tvStatic != null)
                tvStatic.SetActive(false);

            if (tvMessage != null)
                tvMessage.SetActive(false);
        }
    }

    private IEnumerator ShowTVSequence()
    {
        if (tvStatic != null)
            tvStatic.SetActive(true);

        if (tvMessage != null)
            tvMessage.SetActive(false);

        yield return new WaitForSeconds(staticDuration);

        if (tvStatic != null)
            tvStatic.SetActive(false);

        if (tvMessage != null)
            tvMessage.SetActive(true);
    }
}