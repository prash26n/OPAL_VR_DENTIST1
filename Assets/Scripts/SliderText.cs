 using UnityEngine;
 using UnityEngine.UI;
 
 public class SliderText : MonoBehaviour 
 {
     string sliderTextString = "0";
     public Text sliderText; 
 
     public void textUpdate (float textUpdateNumber)
     {
         sliderTextString = textUpdateNumber.ToString();
         sliderText.text = sliderTextString;
     }
 }