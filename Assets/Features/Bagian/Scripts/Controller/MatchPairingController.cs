using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

public class MatchPairingController : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    public ObjectIdentifier selectedObject;
    public int totalMatchedPair;
    public int totalObjectIdentifierParied;
    public GameObject popup;
    public AudioSource audioSource;
    public AudioManager audioManager;

    private bool clicksDisabled = false;
    ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();

    }

    private void Start()
    {
        CountObjectIdentifier();
    }

    private void CountObjectIdentifier()
    {
        var countIdentifier = FindObjectsByType<ObjectIdentifier>(FindObjectsSortMode.None).Length;
        Debug.Log(countIdentifier);
        if (countIdentifier % 2 == 0)
            scoreManager.totalObject = countIdentifier / 2;
    }

    void Update()
    {
        if (clicksDisabled)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            var pointerEventData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };

            var results = new List<RaycastResult>();
            bool componentFound = false;

            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                GameObject clickedObject = result.gameObject;

                if (clickedObject.TryGetComponent<ObjectIdentifier>(out var component))
                {
                    componentFound = true;


                    if (component.objectType == ObjectType.target)
                    {
                        if (selectedObject != null)
                        {
                            if (selectedObject.objectName == component.objectName)
                            {
                                var instantiatedObject = Instantiate(selectedObject, component.transform.position, Quaternion.identity);
                                instantiatedObject.transform.SetParent(component.transform.parent);
                                instantiatedObject.transform.localScale = new Vector3(1f, 1f, 1f);
                                instantiatedObject.transform.rotation = component.transform.rotation;
                                Destroy(selectedObject.gameObject);

                                popup.transform.SetAsLastSibling();

                                var rectTransform = instantiatedObject.GetComponent<RectTransform>();
                                rectTransform.localScale = new Vector3(1f, 1f, 1f);
                                scoreManager.AddScore();
                                instantiatedObject.isMatched = true;

                                if (instantiatedObject.objectType == component.objectType)
                                {
                                    clicksDisabled = true;
                                }
                            }
                            else
                            {
                                var rectTransform = selectedObject.GetComponent<RectTransform>();
                                rectTransform.localScale = new Vector3(1f, 1f, 1f);
                                selectedObject = null;
                            }
                        }
                    }
                    else if (component.objectType == ObjectType.selectedObject && !component.isMatched)
                    {
                        if (selectedObject != null)
                        {
                            var rectTransform = selectedObject.GetComponent<RectTransform>();
                            rectTransform.localScale = new Vector3(1f, 1f, 1f);
                        }

                        selectedObject = component;
                        var newRectTransform = component.GetComponent<RectTransform>();
                        newRectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    }
                    PlayAudio(component.objectType);

                    break;
                }
            }

            if (!componentFound)
            {
                if (selectedObject != null)
                {
                    var rectTransform = selectedObject.GetComponent<RectTransform>();
                    rectTransform.localScale = new Vector3(1f, 1f, 1f);
                }
                selectedObject = null;
            }
        }
    }

    public void PlayAudio(ObjectType objectType)
    {
        if (objectType == ObjectType.target)
        {
            audioSource.clip = audioManager.audioDrop;
        }
        else
        {
            audioSource.clip = audioManager.audioDrag;
        }

        audioSource.Play();
    }
}