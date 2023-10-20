using Interpreter.Eval;
using NUnit.Framework;

namespace Tests.EditMode.Interpreter
{
    public class ValueTest
    {
        [Test]
        public void TestTypeCheck()
        {
            var n = Value.WrapNumber(3.14);
            Assert.True(n.IsNumber);
            Assert.False(n.IsString);

            var s = Value.WrapString("oi");
            Assert.False(s.IsNumber);
            Assert.True(s.IsString);
        }

        [Test]
        public void TestCast()
        {
            var n = Value.WrapNumber(3.14);
            Assert.AreEqual(n.AsNumber, 3.14);
            var ex1 = Assert.Throws<EvalException>(() => { _ = n.AsString; });
            Assert.AreEqual(ex1.Message, "Expected type String, but got Number");

            var s = Value.WrapString("oi");
            Assert.AreEqual(s.AsString, "oi");
            var ex2 = Assert.Throws<EvalException>(() => { _ = s.AsNumber; });
            Assert.AreEqual(ex2.Message, "Expected type Number, but got String");
        }
    }
}