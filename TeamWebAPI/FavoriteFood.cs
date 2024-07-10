using System;

namespace TeamWebAPI
{
    public class FavoriteFood
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string Cuisine { get; set; }
        public bool IsVegetarian { get; set; }
        public DateTime LastEaten { get; set; }
    }
}
