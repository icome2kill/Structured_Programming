using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Structured_Programming.Models.Business
{
    public class ItemsManagement
    {
        private static DataEntities db = new DataEntities();
        public static List<Item> ItemSearch(SearchOptionsModel model)
        {
            string str = model.Query ?? "";
            // Flag to see if none of the search options ticked, default to search for Item name.
            bool flag = false;

            // List of items to display on result page
            List<Item> itemsToDisplay = new List<Item>();

            if (!str.Equals(""))
            {
                // All items is queried first to minimize request to database
                var allItems = db.Items.ToList();

                string[] splited = str.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var s in splited)
                {
                    if (model.InclName == true)
                    {
                        flag = true;
                        itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => row.Name.ToLower().Contains(s.ToLower()))).ToList();
                    }
                    if (model.InclDescription == true)
                    {
                        flag = true;
                        itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => row.Description.ToLower().Contains(s.ToLower()))).ToList();
                    }
                    if (model.InclTrade == true)
                    {
                        flag = true;
                        itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => (row.Trade != null) ? row.Trade.ToLower().Contains(s.ToLower()) : false)).ToList();
                    }
                    if (model.InclUser == true)
                    {
                        flag = true;
                        itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => row.UserProfile.UserName.ToLower().Contains(s.ToLower()))).ToList();
                    }
                    if (model.TypeId != 0)
                    {
                        itemsToDisplay = itemsToDisplay.Where(row => row.TypeId == model.TypeId).ToList();
                    }
                    if (flag == false)
                    {
                        flag = true;
                        model.InclName = true;
                        itemsToDisplay = itemsToDisplay.Union(allItems.Where(row => row.Name.ToLower().Contains(s.ToLower()))).ToList();
                    }
                }
            }
            return itemsToDisplay;
        }
    }
}