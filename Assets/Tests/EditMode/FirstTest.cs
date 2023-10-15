using NUnit.Framework;

namespace Tests.EditMode
{
    public class FirstTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void FirstTestSimplePasses()
        {
            Assert.That("Hello", Is.EqualTo("Hello"));
        }
    }
}
