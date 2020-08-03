using Gemfruit.Mod.Items;
using NUnit.Framework;

namespace GemfruitTest.Mod.Items
{
    [TestFixture]
    public class ItemTest
    {
        [Test]
        public void ParseFromString()
        {
            var src =
                "Cauliflower Seeds/40/-300/Seeds -74/Cauliflower Seeds/Plant these in the spring. Takes 12 days to produce a large cauliflower.";
            var res = Item.ParseFromString(src);
            
            Assert.IsFalse(res.IsError());

            var i = res.Unwrap();
            
            Assert.AreEqual("Cauliflower Seeds", i.Name);
            
            Assert.AreEqual(40, i.Price);
            
            Assert.AreEqual(-300, i.Edibility);
            
            Assert.AreEqual("Seeds", i.Type);
            
            Assert.AreEqual(-74, i.Category);
            
            Assert.AreEqual("Cauliflower Seeds", i.DisplayName);
            
            Assert.AreEqual("Plant these in the spring. Takes 12 days to produce a large cauliflower.", i.Description);
        }
    }
}