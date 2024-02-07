using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webAPI_demo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsAPIController : ControllerBase
    {
        private List<string> Fruits = new List<string>()
        {
            "Watermelon",
            "Strawberry",
            "Pineapple",
            "Papaya",
            "Orange",
            "Mango",
            "Kiwi",
            "Blueberry",
            "Banana",
            "Apple",
        };

        [HttpGet]
        public List<string> FruitsList()
        {
            return Fruits;
        }

        [HttpGet("{index}")]
        public string getFruitById(int index)
        {
            return Fruits.ElementAt(index);   
        }

    }
}
