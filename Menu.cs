using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//For SceneManager
using UnityEngine.SceneManagement;
//for Text
using UnityEngine.UI;
//for serializable
using System;

public class Menu : MonoBehaviour {
	int currentFoodChoice;
	public Button[] FoodButtons;
	public Slider[] Setpro; 
	public Text[] SetproText;
	public int[] OffSetFood;
	public InputField inQuanity;
	private int Budget;
	private Food FastFood = new Food(0.15f,0);
	private Food Italian = new Food(0.25f,0);
	private Food Beverage = new Food(0.15f,0);
	//private Food[] foods= new Food[3];
	//foods[1] = 
	int totalCost;
	public Text FoodText;
	public Text CQ;
	public Text Cost;
	public Text TCost;
	public Text BudgetText;
	//class for food contain quanity and cost
	[Serializable]
	public class Food
	{
		public int tempQuan;
		int price;
		int quality;
		int size;
		int lastQuanity;
		int quanity;
		float costRate;
		int cost;

		public Food(float costRate,int lastquan){
			price = 1;
			size = 1;
			lastQuanity = lastquan;
			quanity = 0;
			quality = 30;
			this.costRate = costRate;
			cost = (int)(costRate*quality);
		}
		public void AddQuan(int quan){
			quanity += quan;
		}
		private void UpdateCost(){
			cost = (int)(costRate*quality);
		}
		public void SetSize(int size){
			this.size = size;
		}
		public void SetQual(int quality){
			this.quality = quality;
			UpdateCost ();
		}
		public void SetPrice(int price){
			this.price = price;
		}
		public int GetQuan(){
			return quanity;
		}
		public int GetSize(){
			return size;
		}
		public int GetQual(){
			return quality;
		}
		public int GetCost(){
			return cost;
		}
		public int Getprice(){
			return price;
		}
		public int GetLQ(){
			return lastQuanity;
		}
	}
	void SetBudgetText(){
		BudgetText.text = "Budget: " +Budget.ToString();
	}
	private void OffInteractable(){
		for(int i =0;i<Setpro.Length;i++){
			Setpro [i].interactable = false;
		}
	}
	private void OnInteractable(){
		for(int i =0;i<Setpro.Length;i++){
			Setpro [i].interactable = true;
		}
	}
	public void Start(){
		OffInteractable ();
		currentFoodChoice = -1;
		Budget = 100000;
		CQ.text = "Old Quanity:";
		Cost.text = "Cost:";
		TCost.text = "Total Cost:";
		SetBudgetText();
		OffSetFood = new int[3];
		for (int i = 0; i < 3; i++) {
			OffSetFood [i] = 1;
		}
	}

	public void Back(){
		//SceneManager.LoadScene ();
	}
	public void Next(){
		//SceneManager.LoadScene ();
	}
	public void Buy(){
		int inQ = -1;
		Int32.TryParse(inQuanity.text, out inQ);
		if (inQ >0) {
			switch (currentFoodChoice) {
			case 0:
				FastFood.AddQuan (inQ);
				SetproText [3].text = FastFood.GetQuan ().ToString ();	
				Budget -= (FastFood.GetCost () * inQ);
				OffSetFood [0] = 0;
				SetBudgetText ();
				break;
			case 1:
				Italian.AddQuan (inQ);
				SetproText [3].text = Italian.GetQuan().ToString();
				Budget -= (Italian.GetCost()*inQ);
				OffSetFood [1] = 0;
				SetBudgetText ();
				break;
			case 2:
				Beverage.AddQuan (inQ);
				SetproText [3].text = Beverage.GetQuan().ToString();
				Budget -= (Beverage.GetCost()*inQ);
				OffSetFood [2] = 0;
				SetBudgetText ();
				break;
			}
			OffInteractable ();
		}
	}
	private void FoodUpdate(Food food){
		Setpro [0].value = food.Getprice ();
		Setpro [1].value = food.GetSize ();
		Setpro [2].value = food.GetQual ();
		for (int i = 0; i< SetproText.Length; i++) {
			SetproTextMethod (i, food);
		}
		CQ.text = "Old Quanity: " +food.GetLQ().ToString();
	}
	private void SetproTextMethod(int index,Food food){
		switch(index){
		case 0:
			SetproText [index].text = food.Getprice().ToString ();
			break;
		case 1:
			SetproText [index].text = food.GetSize().ToString ();
			break;
		case 2:
			SetproText [index].text = food.GetQual().ToString ();
			break;
		case 3:
			SetproText [index].text = food.GetQuan ().ToString ();
			break;
		}
	}
	public void TotalCostUpdate(){
		int inQ = -1;
		Int32.TryParse(inQuanity.text, out inQ);
		if (inQ > 0) {
			switch (currentFoodChoice) {
			case 0:
				FastFood.tempQuan = inQ;
				TCost.text = "Total Cost: \t\t" + (FastFood.GetCost ()*inQ).ToString();
				break;
			case 1:
				Italian.tempQuan = inQ;
				TCost.text = "Total Cost: \t\t" + (Italian.GetCost ()*inQ).ToString();
				break;
			case 2:
				Beverage.tempQuan = inQ;
				TCost.text = "Total Cost: \t\t" + (Beverage.GetCost ()*inQ).ToString();
				break;
			}
		}
	}
	public void SetFastFood(){
		currentFoodChoice = 0;
		FoodText.text = "Fast Food";
		FoodUpdate (FastFood);
		SetQuality ();
		TotalCostUpdate ();
		if (OffSetFood [0] == 1) {
			OnInteractable ();
		}
	}
	public void SetItalian(){
		currentFoodChoice = 1;
		FoodText.text = "Italian";
		FoodUpdate (Italian);
		SetQuality ();
		TotalCostUpdate ();
		if (OffSetFood [1] == 1) {
			OnInteractable ();
		}
	}
	public void SetBeverage(){
		currentFoodChoice = 2;
		FoodText.text = "Beverage";
		FoodUpdate (Beverage);
		SetQuality ();
		TotalCostUpdate ();
		if (OffSetFood [2] == 1) {
			OnInteractable ();
		}
	}
	public void SetPrice(){
		int price = (int)Setpro [0].value;
		switch (currentFoodChoice) {
		case 0:
			FastFood.SetPrice(price);
			SetproTextMethod (0, FastFood);
			break;
		case 1:
			Italian.SetPrice(price);
			SetproTextMethod (0, Italian);
			break;
		case 2:
			Beverage.SetPrice(price);
			SetproTextMethod (0, Beverage);
			break;
		}
	} 
	public void SetSize(){
		int size = (int)Setpro [1].value;
		switch (currentFoodChoice) {
		case 0:
			FastFood.SetSize(size);
			SetproTextMethod (1, FastFood);
			break;
		case 1:
			Italian.SetSize(size);
			SetproTextMethod (1, Italian);
			break;
		case 2:
			Beverage.SetSize(size);
			SetproTextMethod (1, Beverage);
			break;
		}
	}
	public void SetQuality(){
		int quality = (int)Setpro [2].value;
		switch (currentFoodChoice) {
		case 0:
			FastFood.SetQual((int)quality);
			SetproTextMethod (2, FastFood);
			Cost.text = "Cost: " +(FastFood.GetCost()).ToString();
			break;
		case 1:
			Italian.SetQual((int)quality);
			SetproTextMethod (2, Italian);
			Cost.text = "Cost: " +(Italian.GetCost()).ToString();
			break;
		case 2:
			Beverage.SetQual((int)quality);
			SetproTextMethod (2, Beverage);
			Cost.text = "Cost: " +(Beverage.GetCost()).ToString();
			break;
		}
		TotalCostUpdate();
	}



}
