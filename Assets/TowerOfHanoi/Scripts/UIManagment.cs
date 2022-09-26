using UnityEngine;
using UnityEngine.UI;

namespace TowerHanoi
{
    /// <summary>
    /// Let a user use UI for managing the work process
    /// </summary>
    public class UIManagment : MonoBehaviour
    {
        [SerializeField] Slider sliderCountCircles;
        [SerializeField] Text textCountCircles;

        [SerializeField] Slider sliderSpeed;
        [SerializeField] Text textSpeed;

        [SerializeField] Button buttonPlay_Stop;

        [SerializeField] Image imageButton;
        [SerializeField] Sprite spritePlay;
        [SerializeField] Sprite spriteStop;
        [SerializeField] Text textCurrentStep;

        [SerializeField] UISetStockRoles managerStockDestination;
        [SerializeField] DisksManager manager;
        [SerializeField] GameObject panelSetting;


        const string stepWord = "Step";

        void Start()
        {
            //init values
            textCurrentStep.enabled = false;
            imageButton.sprite = spritePlay;
            sliderCountCircles.value = manager.CountDisks;
            OnChangeCountCircles(manager.CountDisks);
            OnChangeSpeed(1);

            //add event handlers
            buttonPlay_Stop.onClick.AddListener(OnClickPlayStop);
            sliderCountCircles.onValueChanged.AddListener(OnChangeCountCircles);
            sliderSpeed.onValueChanged.AddListener(OnChangeSpeed);
            manager.OnCompleteMoveDisk += UpdateUIStep;
        }

        //event handlers
        void OnChangeCountCircles(float value)
        {
            manager.CountDisks = (int)value;
            textCountCircles.text = manager.CountDisks.ToString();
        }
        void OnChangeSpeed(float value)
        {
            float roundedValue = (float)System.Math.Round(value, 1, System.MidpointRounding.AwayFromZero);
            Time.timeScale = roundedValue;
            textSpeed.text = roundedValue.ToString();
        }
        void OnClickPlayStop()
        {
            if (manager.isWorking)
                ToWaitingState();
            else
                ToWorkState();
        }
        void OnCompleteWorkOfManager()
        {
            ToWaitingState();
        }
        void UpdateUIStep()
        {
            textCurrentStep.text = string.Format("{0} {1}/{2}", stepWord, manager.CurrentStepOfWork, manager.CountStepsOfWork);
        }

        //other
        void ToWaitingState()
        {
            textCurrentStep.enabled = false;
            managerStockDestination.enabled = true;
            manager.StopAndClear();
            imageButton.sprite = spritePlay;
            panelSetting.SetActive(true);
        }
        void ToWorkState()
        {
            managerStockDestination.enabled = false;
            manager.StartWork(OnCompleteWorkOfManager);
            imageButton.sprite = spriteStop;
            panelSetting.SetActive(false);
            textCurrentStep.enabled = true;
            UpdateUIStep();
        }

    }
}