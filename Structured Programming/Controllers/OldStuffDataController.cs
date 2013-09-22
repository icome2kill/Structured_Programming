using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Structured_Programming.Models;
using Structured_Programming.Models.Business;

namespace Structured_Programming.Controllers
{
    public class OldStuffDataController : ApiController
    {
        DataEntities db = new DataEntities();
        public List<ItemToSerialize> GetItems(int typeId = 0)
        {
            List<Item> items = db.Items.ToList();
            List<ItemToSerialize> serializableItems = new List<ItemToSerialize>();
            if (typeId != 0)
            {
                items = items.Where(row => row.TypeId == typeId).ToList();
            }
            foreach (var item in items)
            {
                serializableItems.Add(new ItemToSerialize(item));
            }
            return serializableItems;
        }
        [System.Web.Http.HttpGet]
        public List<ItemToSerialize> SearchItems(string Query, bool InclName, bool InclDescription, bool InclUser, bool InclTrade, int TypeId)
        {
            SearchOptionsModel model = new SearchOptionsModel()
            {
                Query = Query, 
                InclName = InclName,
                InclDescription = InclDescription,
                InclUser = InclUser,
                InclTrade = InclTrade,
                TypeId = TypeId
            };
            List<ItemToSerialize> serializableItems = new List<ItemToSerialize>();
            foreach (var item in ItemsManagement.ItemSearch(model))
            {
                serializableItems.Add(new ItemToSerialize(item));
            }
            return serializableItems;
        }
        public ItemToSerialize GetItemDetails(int id)
        {
            var item = new ItemToSerialize(db.Items.Find(id));
            return item;
        }
        public class ItemToSerialize
        {
            public int id;
            public string name;
            public string tradeWith;
            public Nullable<decimal> price;
            public string owner;
            public string type;

            public ItemToSerialize(Item item) {
                id = item.ItemId;
                name = item.Name;
                tradeWith = item.Trade;
                price = item.Price;
                owner = item.UserProfile.UserName;
                type = item.Type.Name;
            }
        }
    }
}
