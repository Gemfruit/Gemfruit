using Gemfruit.Mod.API;
using NUnit.Framework;

namespace GemfruitTest.Mod.API
{
    [TestFixture]
    public class ResourceKeyTest
    {
        [Test]
        public void EqualityTest()
        {
            var a = new ResourceKey("abc", "foo");
            var b = new ResourceKey("abc", "bar");
            var c = new ResourceKey("def", "foo");
            var d = new ResourceKey("def", "bar");
            var aN = new ResourceKey("abc", "foo");
            
            Assert.AreEqual(a, a);
            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(a, d);
            Assert.AreEqual(a, aN);
        }

        [Test]
        public void StringRepresentationTest()
        {
            var a = new ResourceKey("abc", "foo");
            Assert.AreEqual("abc:foo", a.ToString());
        }
    }
}