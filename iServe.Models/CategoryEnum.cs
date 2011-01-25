using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iServe.Models.dotNailsCommon;

namespace iServe.Models {
	public enum CategoryEnum {
		Travel = 1,
		BabysittingChildcare = 2,
		HandymanRepair = 3,
		MealsFood = 4,
		Clothes = 5,
		Furniture = 6,
		Errands = 7,
		ComputerTech = 8,
		LawnYardwork = 9,
		CarRepairMaintenance = 10,
		Other = 11
	}

	[Serializable()]
	public class CategoryEnumList {
		public static readonly DataItem Travel = new DataItem("Travel", "1");
		public static readonly DataItem BabysittingChildcare = new DataItem("Babysitting/Childcare", "2");
		public static readonly DataItem HandymanRepair = new DataItem("Handyman/Repair", "3");
		public static readonly DataItem MealsFood = new DataItem("Meals/Food", "4");
		public static readonly DataItem Clothes = new DataItem("Clothes", "5");
		public static readonly DataItem Furniture = new DataItem("Furniture", "6");
		public static readonly DataItem Errands = new DataItem("Errands", "7");
		public static readonly DataItem ComputerTech = new DataItem("Computer/Tech", "8");
		public static readonly DataItem LawnYardwork = new DataItem("Lawn/Yardwork", "9");
		public static readonly DataItem CarRepairMaintenance = new DataItem("Car repair/Maintenance", "10");
		public static readonly DataItem Other = new DataItem("Other", "11");
		public static readonly DataItem[] DataSource = new DataItem[11] { Travel, BabysittingChildcare, HandymanRepair, MealsFood, Clothes, Furniture, Errands, ComputerTech, LawnYardwork, CarRepairMaintenance, Other };
	}
}
