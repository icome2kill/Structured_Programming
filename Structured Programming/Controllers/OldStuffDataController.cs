using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Structured_Programming.Models;
using Structured_Programming.Models.Business;
using System.Net;
using System.Web.Mvc;

namespace Structured_Programming.Controllers
{
    public class OldStuffDataController : ApiController
    {
        DataEntities db = new DataEntities();
        public List<SerializableItem> GetItems(int typeId = 0)
        {
            List<Item> items = db.Items.ToList();
            List<SerializableItem> serializableItems = new List<SerializableItem>();
            if (typeId != 0)
            {
                items = items.Where(row => row.TypeId == typeId).ToList();
            }
            foreach (var item in items)
            {
                serializableItems.Add(new SerializableItem(item));
            }
            return serializableItems;
        }
        [System.Web.Http.HttpGet]
        public List<SerializableItem> SearchItems(string Query, bool InclName, bool InclDescription, bool InclUser, bool InclTrade, int TypeId)
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
            List<SerializableItem> serializableItems = new List<SerializableItem>();
            foreach (var item in ItemsManagement.ItemSearch(model))
            {
                serializableItems.Add(new SerializableItem(item));
            }
            return serializableItems;
        }
        public SerializableItem GetItemDetails(int id)
        {
            var item = db.Items.Find(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new SerializableItem(item);
        }
        public class SerializableItem
        {
            public int id;
            public string name;
            public string tradeWith;
            public Nullable<decimal> price;
            public string owner;
            public string type;

            public SerializableItem(Item item)
            {
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
